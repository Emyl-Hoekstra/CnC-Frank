using CnC.Base;
using System.Collections.Generic;

namespace CnC.Model.GamePlay
{
    public class PlayerMapTile : FireObject, IFireObject
    {

        public PlayerMapTile(string gameId, string userId, MapTile mapTile)
        {
            this.UserId = userId;
            this.Path = this.GetType().Name;// +"/" + this.UserId;
            this.GameId = gameId;
            this.Tile = mapTile;
        }

        public string GameId { get; set; }

        public MapTile Tile { get; set; }


    }
}
