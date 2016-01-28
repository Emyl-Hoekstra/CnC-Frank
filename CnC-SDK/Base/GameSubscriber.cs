using CnC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnC.Base
{
    public class GameSubscriber
    {
        public IGameObject GameObject { get; set; }
        public IFireObject FireObject { get; set; }

        public Unit Unit { get; set; }
        public Game Game { get; set; }
    }
}
