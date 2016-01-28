using CnC.Base;
using CnC.Controls;
using CnC.Model;
using CnC.Model.GamePlay;
using FirebaseSharp.Portable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Windows.Forms;



namespace CnC_Player
{
    public partial class PlayerDashboard : Form, IFireDelegate
    {

        private const string FBPATHPRIVATE = "https://codeandconquer.firebaseio.com/private/";
        private const string FBPATHPUBLIC = "https://codeandconquer.firebaseio.com/public/";
        private const string SETTINGSPATH = @"C:\Users\Emyl\Documents\Visual Studio 2015\Projects\CnCSDK\CnC-Player\bin\Debug\Settings";
        private Player me;
        private PictureBox[,] mapTilePictures;

        public FirebaseApp FAppPublic { get; set; }
        public FirebaseApp FAppPrivate { get; set; }

        public FireList flGames { get; set; }
        public FireList flCommands { get; set; }
        public FireList flPlayers { get; set; }
        public FireList flCommandFeedback { get; set; }
        public FireList flPlayerGames { get; set; } 

        public FireList flPlayerUnits { get; set; }
        public FireList flPlayerMapTiles { get; set; }

        public PlayerDashboard()
        {
            InitializeComponent();
        }

        private void PlayerDashboard_Load(object sender, EventArgs e)
        {

        }

        private void InitListeners()
        {
            //These lists automatically get updated when data in database changes, also see region #IFireDelegate

            //Players (should be one)
            this.flPlayers = new FireList(this.FAppPrivate, typeof(Player), this, new FireListBox(lbxPlayers), userId: me.Id);

            //Games
            this.flGames = new FireList(this.FAppPublic, typeof(Game), this, new FireListBox(lbxGames));

            //Commands
            this.flCommands = new FireList(this.FAppPrivate, typeof(GameCommand), this, new FireListBox(lbxCommands), userId: me.Id);

            //PlayerGames (should be only one for now)
            this.flPlayerGames = new FireList(this.FAppPrivate, typeof(PlayerGame), this, new FireListBox(lbxMyGames), userId: me.Id);

            //Feedback based from server
            this.flCommandFeedback = new FireList(this.FAppPrivate, typeof(CommandFeedback), this, new FireListBox(lbxCommandFeedback), userId: me.Id);


            //All visible maptiles
            this.flPlayerMapTiles = new FireList(this.FAppPrivate, typeof(PlayerMapTile), this, userId: me.Id);

        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {

            me = CnC.Helpers.Cache.LoadObject<Player>(SETTINGSPATH); //Load from JSon 
            if (me != null)
            {
                this.txtId.Text = me.UserId; // As received from game master (via email)
                this.txtName.Text = me.Name; // Whatever you like
                this.txtNickName.Text = me.NickName; //Whater you like
                this.txtAuthToken.Text = me.Token; // As received from game master (via email)
                this.txtKey.Text = me.Key; // unique fb key
                this.FAppPublic = new FirebaseApp(new Uri(FBPATHPUBLIC + this.txtGameTestEnvironment.Text + "/"), me.Token); // used for connecting with Firebase
                this.FAppPrivate = new FirebaseApp(new Uri(FBPATHPRIVATE + this.txtGameTestEnvironment.Text + "/"), me.Token); // used for connecting with Firebase
            }
            InitPlayer();
            InitListeners();
        }

        private void InitPlayer()
        {

            if (me == null)
            {
                LoadPlayerFromUI();
            }

            me.Save(this.FAppPrivate); //save in firebase db

            this.txtKey.Text = me.Key; //returned unique key from Firebase

            CnC.Helpers.Cache.SaveObject<Player>(SETTINGSPATH, me); //safe as JSon file for next time
        }

        private void LoadPlayerFromUI()
        {
            //create
            me = new Player()
            {
                Id = this.txtId.Text,
                UserId = this.txtId.Text,
                Name = this.txtName.Text,
                NickName = this.txtNickName.Text,
                Token = this.txtAuthToken.Text,
                Key = this.txtKey.Text
            };
            this.txtId.Text = me.UserId;
            this.txtGameTestEnvironment.Text = Guid.NewGuid().ToString();


            this.FAppPrivate = new FirebaseApp(new Uri(FBPATHPRIVATE + this.txtGameTestEnvironment.Text + "/"), me.Token); //used to store data in and retrieve from firebase, path 1
            this.FAppPublic = new FirebaseApp(new Uri(FBPATHPUBLIC + this.txtGameTestEnvironment.Text + "/"), me.Token); //used to store data in and retrieve from firebase, path 2
        }


        private void updateMap()
        {
            PlayerGame myGame = this.flPlayerGames.TypedItems<PlayerGame>().Where(c => c.UserId == me.Id).FirstOrDefault();
            if (mapTilePictures == null && myGame != null)
            {
                //first time init of array
                double size = Math.Sqrt(myGame.Map.MapTiles.Count); //maps are always square
                mapTilePictures = new PictureBox[(int)size, (int)size]; //init array (2 dimensions)
            }

            //PlayerMapTile
            foreach (PlayerMapTile tile in this.flPlayerMapTiles.TypedItems<PlayerMapTile>().ToList())
            {
                RedrawMapTile(tile.Tile);
            }

            // List<PlayerUnit> playerUnits = this.flPlayerUnits.TypedItems<PlayerUnit>().Where(c => c.UserId == myGame.UserId).ToList();
            List<PlayerUnit> playerUnits = this.flPlayerUnits.TypedItems<PlayerUnit>().ToList();

            foreach (PlayerUnit unit in playerUnits)
            {
                PictureBox newBox = mapTilePictures[unit.Unit.Position.Xpos, unit.Unit.Position.Ypos];
                if (newBox != null)
                {
                    if (unit.Unit.UserId != myGame.UserId)
                    {
                        newBox.BackColor = Color.Red;
                    }
                    else
                    {
                        newBox.BackColor = Color.Yellow;

                    }
                    if (unit.Unit.Heading == enHeading.South) newBox.ImageLocation = SETTINGSPATH + "000.png";
                    else if (unit.Unit.Heading == enHeading.SouthWest) newBox.ImageLocation = SETTINGSPATH + "045.png";
                    else if (unit.Unit.Heading == enHeading.West) newBox.ImageLocation = SETTINGSPATH + "090.png";
                    else if (unit.Unit.Heading == enHeading.NorthWest) newBox.ImageLocation = SETTINGSPATH + "135.png";
                    else if (unit.Unit.Heading == enHeading.North) newBox.ImageLocation = SETTINGSPATH + "180.png";
                    else if (unit.Unit.Heading == enHeading.NorthEast) newBox.ImageLocation = SETTINGSPATH + "225.png";
                    else if (unit.Unit.Heading == enHeading.East) newBox.ImageLocation = SETTINGSPATH + "270.png";
                    else if (unit.Unit.Heading == enHeading.SouthEast) newBox.ImageLocation = SETTINGSPATH + "310.png";
                }
            }


        }

        private void RedrawMapTile(MapTile tile)
        {
            PictureBox newBox;
            int tileSize = 20;
            if (mapTilePictures[tile.Xpos, tile.Ypos] == null)
            {
                newBox = new PictureBox();
                mapTilePictures[tile.Xpos, tile.Ypos] = newBox;
                newBox.Left = tileSize * tile.Xpos;
                newBox.Top = tileSize * tile.Ypos;
                newBox.Width = tileSize;
                newBox.Height = tileSize;
                AddPictureBox(newBox);
            }
            else
            {
                newBox = mapTilePictures[tile.Xpos, tile.Ypos];
            }


            if (tile.TerrainType == -1)
            {
                //unknown
                newBox.BackColor = Color.Black;
            }
            else
            {
                switch (tile.TerrainType)
                {
                    case 30:
                        //border
                        newBox.BackColor = System.Drawing.ColorTranslator.FromHtml("#402121");
                        break;
                    case 0:
                        //grass
                        newBox.BackColor = System.Drawing.ColorTranslator.FromHtml("#0df05d");
                        break;
                    case 1:
                        //sand
                        newBox.BackColor = System.Drawing.ColorTranslator.FromHtml("#e1d11a");
                        break;
                    case 100:
                        //spawn
                        newBox.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                        break;
                    case 20:
                        //rocks
                        newBox.BackColor = System.Drawing.ColorTranslator.FromHtml("#714148");
                        break;
                    default:
                        break;
                }

                if (tile.IsAccessible)
                {
                    if (tile.IsSpawningPoint)
                    {
                        //green
                        newBox.BackColor = Color.Orange;
                    }
                    newBox.ImageLocation = null;

                }
            }
        }

        #region IFireDelegate

        public void object_added(IFireObject fireObject)
        {
            if (fireObject.Path == "PlayerMapTile")
            {
                //redraw game
                // this.redrawMap();
                PlayerMapTile gameMapTile = (PlayerMapTile)fireObject;
                PlayerGame playerGame = this.flPlayerGames.TypedItems<PlayerGame>().Where(c => c.UserId == me.Id).FirstOrDefault();
                if (playerGame != null)
                {
                    MapTile mapTile = playerGame.Map.MapTiles.Where(c => c.Xpos == gameMapTile.Tile.Xpos && c.Ypos == gameMapTile.Tile.Ypos).FirstOrDefault();
                    if (mapTile != null)
                    {
                        mapTile = gameMapTile.Tile;
                        RedrawMapTile(mapTile);
                    }
                }



            }
            else if (fireObject.Path == "PlayerUnit")
            {
                updateMap();
            }
            else if (fireObject.Path == "CommandFeedback")
            {
                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.Volume = 100;  // 0...100
                synthesizer.Rate = -2;     // -10...10

                // Asynchronous
                CommandFeedback feedback = (CommandFeedback)fireObject;
                synthesizer.SpeakAsync(feedback.Feedback);
            }
            else if (fireObject.Path == "PlayerGame")
            {
                //All visible player units (both own and enemy)
                PlayerGame pg = (PlayerGame)fireObject;
                this.flPlayerUnits = new FireList(this.FAppPrivate, typeof(PlayerUnit), this, new FireListBox(lbxPlayerUnits), userId: pg.GameId, userIDKey: "GameId");
            }

            Debug.Print("Object added : " + fireObject.Revision);
        }

        public void object_deleted(IFireObject fireObject)
        {
            Debug.Print("Object deleted : " + fireObject.Revision);
            if (fireObject.Path == "GameCommand")
            {
                updateMap();
            }
        }

        public void object_updated(IFireObject fireObject)
        {
            Debug.Print("Object updated : " + fireObject.Revision);
        }

        #endregion

        #region threading precausion
        //required to deal with different threads
        delegate void AddPictureBoxCallback(PictureBox text);

        private void AddPictureBox(PictureBox text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.txtForward.InvokeRequired)
            {
                AddPictureBoxCallback d = new AddPictureBoxCallback(AddPictureBox);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.pnlMap.Controls.Add(text);
            }
        }
        #endregion

        #region UI Buttons and other UI events
        private void btnJoin_Click(object sender, EventArgs e)
        {

            if (this.lbxGames.SelectedIndex > -1)
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.lbxGames.SelectedItem.ToString(), enCommand.JoinGame);
                myCommand.Save(this.FAppPrivate);
            }
        }


        private void btnLeaveGame_Click(object sender, EventArgs e)
        {
            if (this.lbxGames.SelectedIndex > -1)
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.lbxGames.SelectedItem.ToString(), enCommand.LeaveGame);
                myCommand.Save(this.FAppPrivate);
            }
        }

        private void lbxGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxGames.SelectedIndex > -1)
            {
                Game selectedGame = this.flGames.TypedItems<Game>().Where(g => g.Id == lbxGames.SelectedItem.ToString()).First();
                this.lbxUnits.Items.Clear();
                this.lbxPlayersInGame.Items.Clear();
                foreach (Unit unit in selectedGame.AvailableUnits)
                {
                    this.lbxUnits.Items.Add(unit.Id);
                }
                foreach (PlayerGame player in selectedGame.PlayersInGame)
                {
                    this.lbxPlayersInGame.Items.Add(player);
                }
            }
        }

        private void lbxCommandFeedback_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxPlayers.SelectedIndex > -1)
            {
                Player _me = (Player)lbxPlayers.SelectedItem;
                this.txtName.Text = _me.Name;
                this.txtNickName.Text = _me.NickName;
                _me.Token = me.Token;
                CnC.Helpers.Cache.SaveObject<Player>(SETTINGSPATH, _me);

            }
        }

        private void btnAddVehicle_Click(object sender, EventArgs e)
        {
            if (this.lbxUnits.SelectedIndex > -1)
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.lbxUnits.SelectedItem.ToString(), enCommand.AddUnit);
                myCommand.Save(this.FAppPrivate);
            }
        }

        private void lbxMyGames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    updateMap();
        //}

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (this.txtPlayerUnitSelected.Text != "")
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.txtPlayerUnitSelected.Text, enCommand.MoveForward, distance: 1);
                myCommand.Save(this.FAppPrivate);
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (this.txtPlayerUnitSelected.Text != "")
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.txtPlayerUnitSelected.Text, enCommand.MoveBackward, distance: 1);
                myCommand.Save(this.FAppPrivate);
            }
        }

        private void btnRotateVehicleRight_Click(object sender, EventArgs e)
        {
            if (this.txtPlayerUnitSelected.Text != "")
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.txtPlayerUnitSelected.Text, enCommand.RotateCCW, rotation: 45);
                myCommand.Save(this.FAppPrivate);
            }
        }

        private void btnRotateVehicleLeft_Click(object sender, EventArgs e)
        {
            if (this.txtPlayerUnitSelected.Text != "")
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.txtPlayerUnitSelected.Text, enCommand.RotateCW, rotation: 45);
                myCommand.Save(this.FAppPrivate);
            }

        }

        private void btnFire_Click(object sender, EventArgs e)
        {
            if (this.txtPlayerUnitSelected.Text != "")
            {
                GameCommand myCommand = new GameCommand(this.me.Id, this.txtPlayerUnitSelected.Text, enCommand.FireFixedGun, distance: 1);
                myCommand.Save(this.FAppPrivate);
            }
        }
        #endregion

        private void lbxPlayerUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxPlayerUnits.SelectedIndex > -1)
            {
                this.txtPlayerUnitSelected.Text = lbxPlayerUnits.SelectedItem.ToString();
            }
        }



    }
}

