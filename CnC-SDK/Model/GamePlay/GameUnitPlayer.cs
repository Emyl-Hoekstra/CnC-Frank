using CnC.Base;
using System.Collections.Generic;

namespace CnC.Model.GamePlay
{
    public class GameUnitPlayer : FireObject, IFireObject
    {
        public GameUnitPlayer()
        {
            this.Path = this.GetType().Name;
        }

        public Unit Unit { get; set; }
        public string GameId { get; set; }



    }
}
