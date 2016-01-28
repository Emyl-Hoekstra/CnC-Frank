using CnC.Base;

namespace CnC.Model.GamePlay
{
    public class PlayerUnit : FireObject, IFireObject
    {
        public PlayerUnit(string gameId, string userId, Unit unit)
        {
            this.UserId = userId;
            this.Path = this.GetType().Name;// +"/" + this.UserId
            this.GameId = gameId;
            this.Unit = unit;            
            this.Unit.UserId = userId;
        }

        public string GameId { get; set; }

        public Unit Unit { get; set; } //contains both own and enemy units


    }
}
