
using CnC.Base;
namespace CnC.Model.GamePlay
{
    public class GameMapPlayer : FireObject, IFireObject
    {
        public GameMapPlayer()
        {
            this.Path = this.GetType().Name;
        }
        public string GameId { get; set; }
        public Map GameMap { get; set; }
    }
}
