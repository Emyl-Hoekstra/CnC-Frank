using CnC.Base;
using System.Collections.Generic;

namespace CnC.Model.GamePlay
{
    public class PlayerGame : FireObject, IFireObject
    {
        /// <summary>
        /// PlayerGame contains all the data for one player in one game
        /// </summary>
        /// <param name="userId"></param>
        public PlayerGame(Game game = null, string userId = null, string userNickName = "")
        {
            this.UserId = userId;
            this.UserNickName = userNickName;
            this.Path = this.GetType().Name;// +"/" + this.UserId;
            this.Map = new Map();
            this.Units = new List<Unit>();
            this.CommandFeedbacks = new List<CommandFeedback>();
            if (game != null)
            {
                this.GameId = game.Id;
                this.GameName = game.Name;
                this.Map.Description = game.Map.Description;
                this.Map.HorizontalTilesCount = game.Map.HorizontalTilesCount;
                this.Map.VerticalTilesCount = game.Map.VerticalTilesCount;
            }
        }

        public string GameId { get; set; }
        public string GameName { get; set; }
        public string UserNickName { get; set; }
        public Map Map { get; set; }
        public List<Unit> Units { get; set; }
        public List<CommandFeedback> CommandFeedbacks { get; set; }

        public MapTile Spawn { get; set; }

    }
}
