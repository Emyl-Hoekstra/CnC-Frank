using CnC.Base;
using FirebaseSharp.Portable;
using FirebaseSharp.Portable.Interfaces;
using System;

namespace CnC.Model
{
    public class Player : FireObject, IFireObject
    {
        public Player()
        {
            this.Path = this.GetType().Name;
            this.UserId = this.Id;
            this.uid = this.Id;
            this.Rank = 0; //2Do protect, only game master can change this
            //Check if player has been cached

        }

        public string Name { get; set; }
        public string NickName { get; set; }
        public int Rank { get; set; }
        public string Token { get; set; }

        public override string ToString()
        {
            return this.UserId;
        }
    }
}
