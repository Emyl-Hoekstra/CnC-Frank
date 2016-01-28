using CnC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnC.Model.GamePlay
{
    public class PlayerGames : FireObject, IFireObject
    {
                /// <summary>
        /// PlayerGame contains all the data for one player in one game
        /// </summary>
        /// <param name="userId"></param>
        public PlayerGames(string userId = null)
        {
            this.UserId = userId;
            this.Path = this.GetType().Name;
                           
        }
        public List<PlayerGame> Games { get; set; }
    }
}
