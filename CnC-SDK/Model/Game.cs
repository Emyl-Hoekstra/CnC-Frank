
using CnC.Base;
using CnC.Model.GamePlay;
using System.Collections.Generic;

namespace CnC.Model
{
    public class Game : FireObject, IFireObject
    {
        public Game(string userId = null)
        {
            this.AvailableUnits = new List<Unit>();
            this.PlayersInGame = new List<PlayerGame>();
            this.UnitsInGame = new List<Unit>();
            this.UserId = userId;
            this.Path = this.GetType().Name;
            this.Rank = 0;
            this.Map = new Map();
        }


        public string Name { get; set; }
        public string Rules { get; set; }
        public int Rank { get; set; }

        public int MaxNumberOfPlayers { get; set; }
        public int AccessibleTerrainTypes { get; set; }
        public int InAccessibleTerrainTypes { get; set; }

        public Map Map { get; set; }

        public List<Unit> AvailableUnits { get; set; }
        public List<PlayerGame> PlayersInGame { get; set; }

        public List<Unit> UnitsInGame { get; set; }

    }

}
