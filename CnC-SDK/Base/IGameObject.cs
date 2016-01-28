using CnC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnC.Base
{
    public interface IGameObject
    {
        void Update();
        bool Updated { get; set; }



    }
}
