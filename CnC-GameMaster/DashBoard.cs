using CnC.Base;
using CnC.Controls;
using CnC.Helpers;
using CnC.Model;
using CnC.Model.GamePlay;
using FirebaseSharp.Portable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

namespace CnCGameMaster
{

    public partial class DashBoard : Form, IFireDelegate
    {
        //private string FBPATHPRIVATE = "https://codeandconquer.firebaseio.com/private/";
        //private string FBPATHPUBLIC = "https://codeandconquer.firebaseio.com/public/";
        //private const string SETTINGSPATH = @"C:\Users\Emyl\Documents\Visual Studio 2015\Projects\CnCSDK\CnC-GameMaster\bin\Release\Settings";
        //private const string SETTINGSPATH = @"C:\Users\Emyl\Documents\Visual Studio 2015\Projects\CnCSDK\CnC-GameMaster\bin\Debug\SettingsFrank";
        //private string SECRET = "t93Uh9akw4K6fUYSFPVuRP3DAV8Dfq07C7gOvLHt";

        private string FBPATHPRIVATE = "https://flickering-inferno-4892.firebaseio.com/private/";
        private string FBPATHPUBLIC = "https://flickering-inferno-4892.firebaseio.com/public/";
        //private const string SETTINGSPATH = @"C:\Users\Emyl\Documents\Visual Studio 2015\Projects\CnCSDK\CnC-GameMaster\bin\Release_Test\Settings";
        private const string SETTINGSPATH = @"C:\Users\Emyl\Documents\Visual Studio 2015\Projects\CnCSDK\CnC-GameMaster\bin\Debug\Settings";
        private string SECRET = "al1C4guzDk6GgDcjf8ABRq9o8vfo6mDt6SmSrE2p";


        public FirebaseApp FAppPublic { get; set; }
        public FirebaseApp FAppPrivate { get; set; }

        public List<IGameObject> lstGameObjects = new List<IGameObject>();

        private FireList fdPlayers { get; set; }
        private FireList fdGames { get; set; }
        private FireList fdCommands { get; set; }
        private FireList flPlayerGames { get; set; }
        private FireList flGP_Unit { get; set; }
        private FireList flUnit { get; set; }
        private FireList flGun { get; set; }


        private FireList flPlayerUnits { get; set; }
        private FireList flPlayerTiles { get; set; }

        public DashBoard()
        {
            InitializeComponent();
            //  Application.Idle += HandleApplicationIdle;
            Settings mySettings = Cache.LoadObject<Settings>(SETTINGSPATH); //Load from JSon 
            if (mySettings== null)
            {
                mySettings = new Settings();
                mySettings.Secret = SECRET;
               
                CnC.Helpers.Cache.SaveObject<Settings>(SETTINGSPATH, mySettings);
            }
            this.SECRET = mySettings.Secret;
        }

        #region GameLoop
        bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                Update();
                Render();
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        #endregion end GameLoop

        List<GameSubscriber> gameUpdateList = new List<GameSubscriber>();
        void Update()
        {
        //    Debug.Print("updated");
        //    Update subscribed objects
        //    for (int i = 0; i < this.gameUpdateList.Count; i++)
        //    {
        //        GameSubscriber gameSubscriber = this.gameUpdateList[i];
        //        gameSubscriber.GameObject.Update();
        //        if (gameSubscriber.GameObject.Updated)
        //        {
        //            this.gameUpdateList.Remove(gameSubscriber);
        //            gameSubscriber.FireObject.Save(this.FAppPrivate);
        //            gameSubscriber.GameObject.Updated = false;

        //            //notify all gameplayers where this tile is visible
        //            foreach (PlayerGame playergame in this.flPlayerGames.TypedItems<PlayerGame>().Where(c => c.GameId == gameSubscriber.Game.Id).ToList())
        //            {
        //                MapTile newTile = playergame.Map.MapTiles.Where(c => c.Xpos == gameSubscriber.Unit.Position.Xpos && c.Ypos == gameSubscriber.Unit.Position.Ypos && c.TerrainType > -1).FirstOrDefault();
        //                if (newTile != null)
        //                {
        //                    newTile.Unit = gameSubscriber.Unit;
        //                }

        //                // clean old
        //                MapTile previousTile = playergame.Map.MapTiles.Where(c => c.Unit.Id == gameSubscriber.Unit.Id).FirstOrDefault();
        //                if (previousTile != null)
        //                {
        //                    previousTile.Unit = null;
        //                }
        //                playergame.Save(this.FAppPrivate);
        //            }
        //        }
        //    }
        }

        void Render()
        {
            // ...
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            this.txtGameTestEnvironment.Text = Guid.NewGuid().ToString();

            this.FAppPublic = new FirebaseApp(new Uri(FBPATHPUBLIC + this.txtGameTestEnvironment.Text+"/"), SECRET);
            this.FAppPrivate = new FirebaseApp(new Uri(FBPATHPRIVATE + this.txtGameTestEnvironment.Text + "/"), SECRET);

            // Main
            this.fdPlayers = new FireList(this.FAppPrivate, typeof(Player), this, new FireListBox(lbxPlayers));
            this.fdGames = new FireList(this.FAppPublic, typeof(Game), this, new FireListBox(lbxServers), initWithCache: false);
            this.fdCommands = new FireList(this.FAppPrivate, typeof(GameCommand), this, new FireListBox(lbxCommands));

            // Communication between Players
            this.flPlayerGames = new FireList(this.FAppPrivate, typeof(PlayerGame), this, new FireListBox(lbxPlayerGames)); //contains the state of one player in one game
            this.flPlayerUnits = new FireList(this.FAppPrivate, typeof(PlayerUnit), this, new FireListBox(lbxPlayerUnits));
            this.flPlayerTiles = new FireList(this.FAppPrivate, typeof(PlayerMapTile), this);

        }

        public void object_added(IFireObject fireObject)
        {
            Debug.Print("Object added : " + fireObject.Revision);
            if (fireObject.Path == "GameCommand")
            {
                GameCommand cmdReceived = (GameCommand)this.fdCommands.Items.Where(c => c.Id == fireObject.Id).FirstOrDefault();
                try
                {
                    if (cmdReceived == null)
                    {
                        new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Command not found").Save(this.FAppPrivate);
                    }
                    else
                    {
                        Player player = this.fdPlayers.TypedItems<Player>().Where(c => c.UserId == cmdReceived.UserId).FirstOrDefault();

                        if (player == null)
                        {
                            new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Player not found").Save(this.FAppPrivate);
                        }
                        else
                        {
                            PlayerGame playerGame = this.flPlayerGames.TypedItems<PlayerGame>().Where(c => c.UserId == cmdReceived.UserId).FirstOrDefault();
                            string gameId;
                            if (playerGame != null)
                            {
                                gameId = playerGame.GameId;
                            }
                            else
                            {
                                gameId = cmdReceived.ObjectId;
                            }
                            Game game = this.fdGames.TypedItems<Game>().Where(c => c.Id == gameId).FirstOrDefault();

                            switch (cmdReceived.Command)
                            {
                                case enCommand.JoinGame:
                                    JoinGame(cmdReceived, player, playerGame, game);
                                    break;
                                case enCommand.LeaveGame:
                                    LeaveGame(cmdReceived, playerGame, game);
                                    break;
                                case enCommand.AddUnit:
                                    AddUnit(cmdReceived, playerGame, game);
                                    break;
                                case enCommand.MoveForward:
                                    MoveUnit(cmdReceived, playerGame, game);
                                    break;
                                case enCommand.MoveBackward:
                                    cmdReceived.Distance = -cmdReceived.Distance;
                                    MoveUnit(cmdReceived, playerGame, game);
                                    break;
                                case enCommand.RotateCCW:
                                    //per 45 graden
                                    RotateUnit(cmdReceived, playerGame, game);
                                    break;
                                case enCommand.RotateCW:
                                    RotateUnit(cmdReceived, playerGame, game);
                                    break;
                                case enCommand.FireFixedGun:
                                    FireFixedGun(cmdReceived, playerGame, game);
                                    break;
                                case enCommand.RotateTurretCCW:
                                    break;
                                case enCommand.RotateTurretCW:
                                    break;
                                default:
                                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Unknown command").Save(this.FAppPrivate);
                                    break;
                            }
                        }
                    }
                    cmdReceived.Delete(this.FAppPrivate);
                }
                catch (Exception e)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Error: " + e.InnerException.Message).Save(this.FAppPrivate);
                    cmdReceived.Delete(this.FAppPrivate);
                    //throw;
                }
            }
        }

        private void FireFixedGun(GameCommand cmdReceived, PlayerGame playerGame, Game game)
        {
            PlayerUnit playerUnit = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.Id == cmdReceived.ObjectId && c.Unit.UserId == playerGame.UserId).FirstOrDefault();
            if (playerUnit == null)
            {
                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "PlayerUnit not found").Save(this.FAppPrivate);
            }
            else
            {
                Unit myUnit = playerUnit.Unit;
                if (myUnit == null)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Unit not found").Save(this.FAppPrivate);
                }

                else
                {
                    if (!myUnit.HasFixedGun || myUnit.FixedGunFiringRange < cmdReceived.Distance)
                    {
                        new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Unit doesn't have a fixed gun or distance out of range").Save(this.FAppPrivate);
                    }
                    else
                    {
                        int targetYpos = -1;
                        int targetXpos = -1;
                        switch (myUnit.Heading)
                        {
                            case enHeading.North:
                                targetYpos = (int)(myUnit.Position.Ypos - cmdReceived.Distance);
                                targetXpos = (int)(myUnit.Position.Xpos);
                                break;
                            case enHeading.NorthWest:
                                targetYpos = (int)(myUnit.Position.Ypos - cmdReceived.Distance);
                                targetXpos = (int)(myUnit.Position.Xpos - cmdReceived.Distance);
                                break;
                            case enHeading.South:
                                targetYpos = (int)(myUnit.Position.Ypos + cmdReceived.Distance);
                                targetXpos = (int)(myUnit.Position.Xpos);
                                break;
                            case enHeading.SouthWest:
                                targetYpos = (int)(myUnit.Position.Ypos + cmdReceived.Distance);
                                targetXpos = (int)(myUnit.Position.Xpos - cmdReceived.Distance);
                                break;
                            case enHeading.SouthEast:
                                targetYpos = (int)(myUnit.Position.Ypos + cmdReceived.Distance);
                                targetXpos = (int)(myUnit.Position.Xpos + cmdReceived.Distance);
                                break;
                            case enHeading.NorthEast:
                                targetYpos = (int)(myUnit.Position.Ypos - cmdReceived.Distance);
                                targetXpos = (int)(myUnit.Position.Xpos + cmdReceived.Distance);
                                break;
                            case enHeading.East:
                                targetYpos = (int)(myUnit.Position.Ypos);
                                targetXpos = (int)(myUnit.Position.Xpos + cmdReceived.Distance);
                                break;
                            case enHeading.West:
                                targetYpos = (int)(myUnit.Position.Ypos);
                                targetXpos = (int)(myUnit.Position.Xpos - cmdReceived.Distance);
                                break;
                            default:
                                break;
                        }

                        if (targetXpos > -1 && targetYpos > -1)
                        {
                            PlayerUnit pu = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.Unit.Position.Xpos == targetXpos && c.Unit.Position.Ypos == targetYpos).FirstOrDefault();


                            if (pu == null)
                            {
                                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Target missed").Save(this.FAppPrivate);
                            }
                            else
                            {
                                Unit attackedUnit = pu.Unit;
                                attackedUnit.Armor -= myUnit.FixedGunArmorDamage;
                                attackedUnit.Shields -= myUnit.FixedGunShieldDamage;
                                pu.Save(this.FAppPrivate);
                                


                                if (attackedUnit.Armor < 1 || attackedUnit.Shields < 1)
                                {
                                    attackedUnit.Status = enUnitStatus.Destroyed;
                                    new CommandFeedback(cmdReceived, enCommandStatus.Accepted, attackedUnit.Name + " destroyed!").Save(this.FAppPrivate);
                                    pu.Delete(this.FAppPrivate);
                                }
                                else
                                {
                                    new CommandFeedback(cmdReceived, enCommandStatus.Accepted, attackedUnit.Name + " hit!, enemy shields: " + attackedUnit.Shields + ", enemy armor: " + attackedUnit.Armor).Save(this.FAppPrivate);
                                }
                                
                             }

                        }
                    }
                }

            }
        }

        private void RotateUnit(GameCommand cmdReceived, PlayerGame playerGame, Game game)
        {
            PlayerUnit playerUnit = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.Id == cmdReceived.ObjectId && c.Unit.UserId == playerGame.UserId).FirstOrDefault();
            if (playerUnit == null)
            {
                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "PlayerUnit not found").Save(this.FAppPrivate);
            }
            else
            {
                Unit myUnit = playerUnit.Unit;
                if (myUnit == null)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Unit not found").Save(this.FAppPrivate);
                }
                else
                {
                    switch (myUnit.Heading)
                    {
                        case enHeading.South:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.SouthEast;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading South-East").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.SouthWest;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading South-West").Save(this.FAppPrivate);
                            }
                            break;
                        case enHeading.SouthWest:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.South;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading South").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.West;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading West").Save(this.FAppPrivate);
                            }
                            break;
                        case enHeading.West:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.SouthWest;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading South-West").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.NorthWest;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading North-East").Save(this.FAppPrivate);
                            }
                            break;
                        case enHeading.NorthWest:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.West;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading West").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.North;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading North").Save(this.FAppPrivate);
                            }
                            break;
                        case enHeading.North:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.NorthWest;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading North-West").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.NorthEast;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading North-East").Save(this.FAppPrivate);
                            }
                            break;
                        case enHeading.NorthEast:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.North;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading North").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.East;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading East").Save(this.FAppPrivate);
                            }
                            break;
                        case enHeading.East:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.NorthEast;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading North-East").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.SouthEast;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading South-East").Save(this.FAppPrivate);
                            }
                            break;
                        case enHeading.SouthEast:
                            if (cmdReceived.Command == enCommand.RotateCW)
                            {
                                myUnit.Heading = enHeading.East;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading East").Save(this.FAppPrivate);
                            }
                            else
                            {
                                myUnit.Heading = enHeading.South;
                                new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Heading South").Save(this.FAppPrivate);
                            }
                            break;
                        default:
                            break;
                    }
                    playerUnit.Save(this.FAppPrivate);
                    // playerGame.Save(this.FAppPrivate);
                    //  game.UnitsInGame.Where(c => c.Id == myUnit.Id).FirstOrDefault().Heading = myUnit.Heading;
                    //Test game.Save(this.FAppPublic);

                }
            }
        }

        #region Events

        private void JoinGame(GameCommand cmdReceived, Player player, PlayerGame playerGame, Game game)
        {
            if (playerGame != null)
            {
                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "User already joined game").Save(this.FAppPrivate);
            }
            else
            {
                if (game == null)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Game not found").Save(this.FAppPrivate);
                }
                else
                {
                    if (game.PlayersInGame.Count >= game.MaxNumberOfPlayers)
                    {
                        new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Already too many players in the game").Save(this.FAppPrivate);
                    }
                    else
                    {
                        PlayerGame newPlayerGame = new PlayerGame(game, cmdReceived.UserId, player.NickName);

                        foreach (MapTile tile in game.Map.MapTiles)
                        {

                            MapTile playerTile = new MapTile();
                            playerTile.TerrainType = -1;// tile.TerrainType;
                            playerTile.UserId = newPlayerGame.UserId;
                            playerTile.Xpos = tile.Xpos;
                            playerTile.Ypos = tile.Ypos;

                            newPlayerGame.Map.MapTiles.Add(playerTile);

                            //set spawn, check if not occupied first
                            if (newPlayerGame.Spawn == null && tile.IsSpawningPoint)
                            {
                                if (game.PlayersInGame.Where(c => c.Spawn.Xpos == tile.Xpos && c.Spawn.Ypos == tile.Ypos).Count() == 0)
                                {
                                    //not used yet
                                    newPlayerGame.Spawn = playerTile;
                                    PlayerMapTile playerMapTile = new PlayerMapTile(game.Id, cmdReceived.UserId, playerTile);
                                    playerTile.TerrainType = 100;
                                    playerTile.IsSpawningPoint = true;
                                    playerTile.IsAccessible = true;
                                    playerMapTile.Save(this.FAppPrivate);
                                }
                            }
                        }


                        newPlayerGame.Save(this.FAppPrivate);
                        game.PlayersInGame.Add(newPlayerGame);
                        game.Save(this.FAppPublic);
                        new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Joined the game").Save(this.FAppPrivate);
                    }
                }
            }
        }

        private void LeaveGame(GameCommand cmdReceived, PlayerGame playerGame, Game game)
        {
            if (playerGame == null)
            {
                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "User did not join this game").Save(this.FAppPrivate);
            }
            else
            {
                if (game == null)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Could not find this game").Save(this.FAppPrivate);
                }
                else
                {

                    PlayerGame playerToRemove = game.PlayersInGame.Where(c => c.UserId == cmdReceived.UserId).FirstOrDefault();
                    if (playerToRemove == null)
                    {
                        new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Player not found in the game").Save(this.FAppPrivate);
                    }
                    else
                    {
                        game.PlayersInGame.Remove(playerToRemove);
                        game.Save(this.FAppPublic);
                        playerToRemove.Delete(this.FAppPrivate);
                        new CommandFeedback(cmdReceived, enCommandStatus.Accepted, "Left the Game").Save(this.FAppPrivate);
                    }
                }
            }
        }

        private void AddUnit(GameCommand cmdReceived, PlayerGame playerGame, Game game)
        {
            Unit newUnitType = game.AvailableUnits.Where(c => c.Id == cmdReceived.ObjectId).FirstOrDefault();

            if (newUnitType == null)
            {
                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, newUnitType.Name + " not found in the game").Save(this.FAppPrivate);
            }
            else
            {
                if (playerGame == null)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "PlayerGame not found").Save(this.FAppPrivate);
                }
                else
                {
                    if (this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.UserId == cmdReceived.UserId && c.Unit.UnitTypeId == newUnitType.UnitTypeId && c.Unit.UserId == playerGame.UserId).Count() >= newUnitType.MaxNumberOf)
                    {
                        new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Already too many " + newUnitType.Name + "s in the game").Save(this.FAppPrivate);
                    }
                    else
                    {

                        // Unit unitsOnPosition = game.UnitsInGame.Where(c => c.Position.Xpos == playerGame.Spawn.Xpos && c.Position.Ypos == playerGame.Spawn.Ypos).FirstOrDefault();
                        PlayerUnit unitsOnPosition = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.Unit.Position.Xpos == playerGame.Spawn.Xpos && c.Unit.Position.Ypos == playerGame.Spawn.Ypos).FirstOrDefault();

                        if (unitsOnPosition != null)
                        {
                            new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Could not spawn, spawn is occupied").Save(this.FAppPrivate);
                        }
                        else
                        {
                            Unit newUnit = DeepClone.CloneJson<Unit>(newUnitType);
                            newUnit.Id = Guid.NewGuid().ToString();
                            newUnit.CreatedAt = DateTime.Now;
                            newUnit.UpdatedAt = DateTime.Now;
                            newUnit.Position.Xpos = playerGame.Spawn.Xpos;
                            newUnit.Position.Ypos = playerGame.Spawn.Ypos;
                            newUnit.UserId = cmdReceived.UserId;


                            game.UnitsInGame.Add(newUnit);
                            //TEST game.Save(this.FAppPublic);

                            PlayerUnit newPlayerUnit = new PlayerUnit(game.Id, playerGame.UserId, newUnit);
                            newPlayerUnit.Save(this.FAppPrivate);

                            UpdateRadarRange(playerGame, game, newUnit); //update map for player

                            //playerGame.Units.Add(newUnit);
                            //playerGame.Save(this.FAppPrivate);

                            //subscribe unit to game ticker
                            this.gameUpdateList.Add(new GameSubscriber() { FireObject = playerGame, GameObject = newUnit, Game = game, Unit = newUnit });

                            new CommandFeedback(cmdReceived, enCommandStatus.Accepted, newUnitType.Name + " added to the game").Save(this.FAppPrivate);
                        }
                    }
                }
            }
        }

        private void MoveUnit(GameCommand cmdReceived, PlayerGame playerGame, Game game)
        {
            if (playerGame == null)
            {
                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "User did not join game").Save(this.FAppPrivate);
            }
            else
            {
                if (game == null)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Game not found").Save(this.FAppPrivate);
                }
                else
                {
                    PlayerGame myPlayerGame = this.flPlayerGames.TypedItems<PlayerGame>().Where(c => c.UserId == cmdReceived.UserId).FirstOrDefault();
                    if (myPlayerGame == null)
                    {
                        new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Player not found in game").Save(this.FAppPrivate);
                    }
                    else
                    {
                        // Unit myUnit = myPlayerGame.Units.Where(c => c.Id == cmdReceived.ObjectId).FirstOrDefault();
                        PlayerUnit playerUnit = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.Id == cmdReceived.ObjectId && c.Unit.UserId == playerGame.UserId).FirstOrDefault();
                        if (playerUnit == null)
                        {
                            new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Unit not found in game").Save(this.FAppPrivate);
                        }
                        else
                        {
                            Unit myUnit = playerUnit.Unit;

                            //can the unit move?
                            if (cmdReceived.Distance > myUnit.MovingRange)
                            {
                                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, playerUnit.Unit.Name + " can not move that distance in one move").Save(this.FAppPrivate);
                            }
                            else
                            {

                                int targetYpos = -1;
                                int targetXpos = -1;
                                switch (playerUnit.Unit.Heading)
                                {
                                    case enHeading.North:
                                        targetYpos = (int)(myUnit.Position.Ypos - cmdReceived.Distance);
                                        targetXpos = (int)(myUnit.Position.Xpos);
                                        break;
                                    case enHeading.NorthWest:
                                        targetYpos = (int)(myUnit.Position.Ypos - cmdReceived.Distance);
                                        targetXpos = (int)(myUnit.Position.Xpos - cmdReceived.Distance);
                                        break;
                                    case enHeading.South:
                                        targetYpos = (int)(myUnit.Position.Ypos + cmdReceived.Distance);
                                        targetXpos = (int)(myUnit.Position.Xpos);
                                        break;
                                    case enHeading.SouthWest:
                                        targetYpos = (int)(myUnit.Position.Ypos + cmdReceived.Distance);
                                        targetXpos = (int)(myUnit.Position.Xpos - cmdReceived.Distance);
                                        break;
                                    case enHeading.SouthEast:
                                        targetYpos = (int)(myUnit.Position.Ypos + cmdReceived.Distance);
                                        targetXpos = (int)(myUnit.Position.Xpos + cmdReceived.Distance);
                                        break;
                                    case enHeading.NorthEast:
                                        targetYpos = (int)(myUnit.Position.Ypos - cmdReceived.Distance);
                                        targetXpos = (int)(myUnit.Position.Xpos + cmdReceived.Distance);
                                        break;
                                    case enHeading.East:
                                        targetYpos = (int)(myUnit.Position.Ypos);
                                        targetXpos = (int)(myUnit.Position.Xpos + cmdReceived.Distance);
                                        break;
                                    case enHeading.West:
                                        targetYpos = (int)(myUnit.Position.Ypos);
                                        targetXpos = (int)(myUnit.Position.Xpos - cmdReceived.Distance);
                                        break;
                                    default:
                                        break;
                                }

                                if (targetXpos > -1 && targetYpos > -1)
                                {
                                    moveUnit(cmdReceived, playerGame, game, playerUnit, targetYpos, targetXpos);
                                }

                            }
                        }
                    }
                }
            }
        }

        private void moveUnit(GameCommand cmdReceived, PlayerGame playerGame, Game game, PlayerUnit myUnit, int targetYpos, int targetXpos)
        {
            MapTile targetMapTile = game.Map.MapTiles.Where(c => c.Xpos == targetXpos && c.Ypos == targetYpos).FirstOrDefault();
            if (targetMapTile == null)
            {
                new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Map position not found (out of map bounds)").Save(this.FAppPrivate);
            }
            else
            {
                if (!targetMapTile.IsAccessible)
                {
                    new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Map position type is inaccessible").Save(this.FAppPrivate);
                }
                else
                {
                    PlayerUnit unitsOnPosition = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.Unit.Position.Xpos == targetXpos && c.Unit.Position.Ypos == targetYpos).FirstOrDefault();
                    if (unitsOnPosition != null)
                    {
                        new CommandFeedback(cmdReceived, enCommandStatus.Rejected, "Map position is occupied").Save(this.FAppPrivate);
                    }
                    else
                    {
                        //yes can move there


                        myUnit.Unit.Position.Ypos = targetYpos;
                        myUnit.Unit.Position.Xpos = targetXpos;
                        UpdateRadarRange(playerGame, game, myUnit.Unit);
                        myUnit.Save(this.FAppPrivate);


                        //List<PlayerMapTile> pmtOthers = this.flPlayerTiles.TypedItems<PlayerMapTile>().Where(c => c.Tile.Xpos == targetXpos && c.Tile.Ypos == targetYpos && c.UserId != playerGame.UserId && c.GameId == game.Id).ToList();
                        //foreach (PlayerMapTile pmt in pmtOthers)
                        //{
                        //    PlayerUnit pu = CnC.Helpers.DeepClone.CloneJson<PlayerUnit>(myUnit);
                        //    pu.Key = null;
                        //    pu.UserId = pmt.UserId;
                        //    pu.Unit.UserId = playerGame.UserId;
                        //    pu.Save(this.FAppPrivate);
                        //}

                        //    PlayerUnit pu = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.Unit.Id == myUnit.Unit.Id && c.UserId == pmt.UserId).FirstOrDefault();
                        //    if(pu == null)
                        //    {
                        //        pu = CnC.Helpers.DeepClone.CloneJson<PlayerUnit>(myUnit);
                        //        pu.Unit.UserId = pmt.UserId;
                        //        pu.UserId = playerGame.UserId;

                        //    }
                        //    else
                        //    {
                        //        pu.Unit.Position.Xpos = targetXpos;
                        //        pu.Unit.Position.Ypos = targetYpos;
                        //    }
                        //    pu.Save(this.FAppPrivate);



                        new CommandFeedback(cmdReceived, enCommandStatus.Accepted, myUnit.Unit.Name + " moved").Save(this.FAppPrivate);
                    }
                }
            }
        }

        private void UpdateRadarRange(PlayerGame playerGame, Game game, Unit newUnit)
        {
            //update tiles in radar range for user
            List<MapTile> radarRange = playerGame.Map.MapTiles.Where(c => c.Xpos >= newUnit.Position.Xpos - newUnit.RadarRange && c.Xpos <= newUnit.Position.Xpos + newUnit.RadarRange && c.Ypos >= newUnit.Position.Ypos - newUnit.RadarRange && c.Ypos <= newUnit.Position.Ypos + newUnit.RadarRange).ToList();
            foreach (MapTile mt in radarRange)
            {
                MapTile motherTile = game.Map.MapTiles.Where(c => c.Xpos == mt.Xpos && c.Ypos == mt.Ypos).First();
                mt.TerrainType = motherTile.TerrainType;
                mt.IsAccessible = motherTile.IsAccessible;
                mt.IsSpawningPoint = motherTile.IsSpawningPoint;
                mt.UserId = playerGame.UserId;
                PlayerMapTile pmt = this.flPlayerTiles.TypedItems<PlayerMapTile>().Where(c => c.Tile.Xpos == mt.Xpos && c.Tile.Ypos == mt.Ypos && c.UserId == playerGame.UserId).FirstOrDefault();
                if (pmt == null)
                {
                    PlayerMapTile playerMapTile = new PlayerMapTile(game.Id, playerGame.UserId, mt);
                    playerMapTile.Save(this.FAppPrivate);
                }
            }

        }

        public void object_deleted(IFireObject fireObject)
        {
            Debug.Print("Object deleted : " + fireObject.Revision);
        }

        public void object_updated(IFireObject fireObject)
        {
            Debug.Print("Object updated : " + fireObject.Revision);
        }

        private void btnEditPlayer_Click(object sender, EventArgs e)
        {

            Player player = (Player)this.fdPlayers.Items.Where(c => c.Id == lbxPlayers.SelectedItem.ToString()).First();
            player.Name = "King Frank 3";
            player.Save(this.FAppPrivate);
        }

        private void btnDeletePlayer_Click(object sender, EventArgs e)
        {
            Player player = (Player)this.fdPlayers.Items.Where(c => c.Id == lbxPlayers.SelectedItem.ToString()).First();
            player.Delete(this.FAppPrivate);

        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            var frank = new Player()
            {
                Name = "Frank Kroondijk",
                NickName = "FCOle"
            };
            frank.Save(this.FAppPrivate);
        }

        private void btnAddGame_Click(object sender, EventArgs e)
        {

            var game = new Game()
            {
                Name = "Super Game",
                Rules = "There are no rules",
                MaxNumberOfPlayers = 2,
                AccessibleTerrainTypes = 2,
                InAccessibleTerrainTypes = 2,

            };

            game.Map.LoadMap("Maps\\Map02.csv");
            game.Map.Name = "Demo map";
            game.Map.Description = "My first demo map created in google spreadsheets";

            var gunSmallArms = new Weapon()
            {
                Class = 0,
                FiringRange = 2,
                Name = "small arms",
                RotatingSpeed = 90,
                ShieldDamage = 1,
                ArmorDamage = 2

            };

            var unitScout = new Unit()
            {
                Name = "Scout",
                Class = 0,
                Heading = enHeading.North,
                MovingSpeed = 3,
                MovingRange = 1,
                RotatingSpeed = 90,
                BuildTimeInSeconds = 10,
                MaxNumberOf = 1,
                Armor = 5,
                RadarRange = 1,
                Shields = 5,
                HasFixedGun = true,
                FixedGunArmorDamage = 1,
                FixedGunFiringRange = 1,
                FixedGunShieldDamage = 1,
                UnitTypeId = Guid.NewGuid().ToString()
            };
            //   unitScout.Weapons.Add(gunSmallArms);

            game.AvailableUnits.Add(unitScout);


            var gunTurret = new Weapon()
            {
                Class = 1,
                FiringRange = 2,
                Name = "heavy arms",
                RotatingSpeed = 45,
                ShieldDamage = 10,
                ArmorDamage = 20

            };

            var heavyUnit = new Unit()
            {
                Name = "Heavy",
                Class = 1,
                Heading = enHeading.NorthEast,
                MovingSpeed = 2,
                MovingRange = 1,
                RotatingSpeed = 45,
                BuildTimeInSeconds = 10,
                MaxNumberOf = 1,
                Armor = 100,
                RadarRange = 1,
                Shields = 200,
                HasFixedGun = true,
                FixedGunArmorDamage = 10,
                FixedGunFiringRange = 10,
                FixedGunShieldDamage = 10,
                UnitTypeId = Guid.NewGuid().ToString()
            };

            // heavyUnit.Weapons.Add(gunTurret);
            game.AvailableUnits.Add(heavyUnit);

            game.Save(this.FAppPublic);
        }

        private void btnEditGame_Click(object sender, EventArgs e)
        {
            Game game = (Game)this.fdGames.Items.Where(c => c.Id == lbxServers.SelectedItem.ToString()).First();
            game.Name = "Mega Game";
            game.Save(this.FAppPublic);
        }

        private void btnDeleteGame_Click(object sender, EventArgs e)
        {
            Game game = (Game)this.fdGames.Items.Where(c => c.Id == lbxServers.SelectedItem.ToString()).First();
            game.Delete(this.FAppPublic);
        }

        private void btnCacheGames_Click(object sender, EventArgs e)
        {
            this.fdGames.SaveToCache();
        }

        private void btnCreateToken_Click(object sender, EventArgs e)
        {

            var tokenGenerator = new Firebase.TokenGenerator(SECRET);
            var authPayload = new Dictionary<string, object>()
                {
                  { "uid", this.txtUserId.Text }, 
                  { "UserId", this.txtUserId.Text },
                  { "Role", "user" }
                };

            string token = tokenGenerator.CreateToken(authPayload, new Firebase.TokenOptions(expires: new DateTime(2016, 5, 1), debug: true));
            this.txtAuthToken.Text = token;


            FirebaseApp App = new FirebaseApp(new Uri(FBPATHPRIVATE), token);

        }

        private void lbxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtUserId.Text = lbxPlayers.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FAppPrivate.Child("/").Remove();
            this.FAppPublic.Child("/").Remove();
        }

        #endregion

    }
}


#region comment
////Does user exist?
//Player thisPlayer = null;
//if (this.fdPlayers.Items.Where(c => c.Id == thisCommand.UserId).Count() > 0)
//{
//    thisPlayer = (Player)this.fdPlayers.Items.Where(c => c.Id == thisCommand.UserId).First();
//}
//else
//{
//    //user does not exist
//    Debug.Print("User does not exist");
//    new CommandFeedback(thisCommand, enCommandStatus.Rejected, "Player does not exist").Save(this.FAppPrivate);
//    return;
//}

//Game selectedGame = null;
//var bla = this.flPlayerGames.TypedItems<PlayerGames>();

//if (this.flPlayerGames.TypedItems<PlayerGames>().Where(c => c.PlayerGames == thisCommand.UserId).Count() > 0)
//{
//    selectedGame = (Game)this.fdGames.Items.Where(c => c.Id == thisCommand.ObjectId).First();
//}

//if (selectedGame != null) //c.GameId == thisCommand.GameId
//{
//    //Already in a game, exit other one first or if its trash, delete it
//    Debug.Print("Already in a game, exit other one first");
//    new CommandFeedback(thisCommand, enCommandStatus.Rejected, "Player already joined (another) game").Save(this.FAppPrivate);
//}
//else
//{
//    if (this.fdGames.Items.Where(c => c.Id == thisCommand.ObjectId).Count() > 0)
//    {
//        selectedGame = (Game)this.fdGames.Items.Where(c => c.Id == thisCommand.ObjectId).First();
//        if (selectedGame.Rank == thisPlayer.Rank)
//        {
//            PlayerGame playerGame = new PlayerGame(thisPlayer.Id);
//            playerGame.GameId = selectedGame.Id;
//            playerGame.GameName = selectedGame.Name;
//            playerGame.UserNickName = thisPlayer.NickName;
//            playerGame.Save(this.FAppPrivate);
//            selectedGame.PlayersInGame.Add(playerGame);
//            selectedGame.Save(this.FAppPublic);                                    
//        }
//        else
//        {
//            //invalid rank
//            Debug.Print("Invalid rank");
//            new CommandFeedback(thisCommand, enCommandStatus.Rejected, "Player has invalid rank").Save(this.FAppPrivate);
//        }
//    }
//    else
//    {
//        //invalid game
//        Debug.Print("Invalid game");
//        new CommandFeedback(thisCommand, enCommandStatus.Rejected, "Invalid game selected").Save(this.FAppPrivate);
//    }
//}


//user in game
//if (this.flPlayerGames.TypedItems<PlayerGames>().Where(c => c.UserId == thisCommand.UserId && c.GameId == thisCommand.ObjectId).Count() > 0)
//{
//    this.flPlayerGames.TypedItems<PlayerGames>().Where(c => c.UserId == thisCommand.UserId && c.GameId == thisCommand.ObjectId).First().Delete(this.FAppPrivate);
//    selectedGame.PlayersInGame.Remove(selectedGame.PlayersInGame.Where(p => p.UserId == thisCommand.UserId).First());
//    selectedGame.Save(this.FAppPublic);
//}
//else
//{
//    new CommandFeedback(thisCommand, enCommandStatus.Rejected, "Player did not join this game").Save(this.FAppPrivate);
//}
//user in game?
//if (selectedGame.PlayersInGame.Where(p => p.UserId == thisCommand.UserId).Count() > 0)
//{
//    bool bAllowedToAdd = false;
//    Unit gameUnit = null;
//    List<GameUnitPlayer> unitsInGame = this.flGP_Unit.TypedItems<GameUnitPlayer>().Where(c => c.GameId == selectedGame.Id && c.UserId == thisCommand.UserId).OrderByDescending(c => c.CreatedAt).ToList();

//    int numberOfUnitsInGame = unitsInGame.Count();// this.flGP_Unit.TypedItems<GP_Unit>().Where(c => c.GameId == selectedGame.Id && c.UserId == thisCommand.UserId).Count();

//    int gameUnitCount = this.flUnit.TypedItems<Unit>().Where(c => c.Id == thisCommand.ObjectId).Count();
//    if (numberOfUnitsInGame > 0 && gameUnitCount == 1)
//    {
//        gameUnit = this.flUnit.TypedItems<Unit>().Where(c => c.Id == thisCommand.ObjectId).First();
//        //check if the number of this type of unit is allowed
//        if (numberOfUnitsInGame < gameUnit.MaxNumberOf)
//        {
//            if (numberOfUnitsInGame > 0)
//            {
//                //check when the last (not very accurate but good enough for now)
//                if (unitsInGame.First().CreatedAt.AddSeconds(gameUnit.BuildTime) > DateTime.Now)
//                {
//                    bAllowedToAdd = true;
//                }
//                else
//                {
//                    new CommandFeedback(thisCommand, enCommandStatus.Rejected, "This type of unit needs more time to build").Save(this.FAppPrivate);
//                }

//            }
//            else
//            {
//                //safe to add
//                bAllowedToAdd = true;
//            }

//        }
//        else
//        {
//            new CommandFeedback(thisCommand, enCommandStatus.Rejected, "Already to many of this type of units in the game").Save(this.FAppPrivate);
//        }
//    }
//    if (bAllowedToAdd)
//    {
//        GameUnitPlayer gpUnit = new GameUnitPlayer();
//        gpUnit.CreatedAt = DateTime.Now;
//        gpUnit.GameId = selectedGame.Id;
//        gpUnit.UserId = thisCommand.UserId;
//        gpUnit.UpdatedAt = DateTime.Now;
//        gpUnit.Unit = gameUnit;
//        gpUnit.Save(this.FAppPrivate);
//    }
//}
//else
//{
//    new CommandFeedback(thisCommand, enCommandStatus.Rejected, "Player did not join this game").Save(this.FAppPrivate);
//}

#endregion
