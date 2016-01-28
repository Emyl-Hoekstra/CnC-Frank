
using CnC.Base;
namespace CnC.Model.GamePlay
{
    public class GP_MapTile : FireObject, IFireObject
    {
        public GP_MapTile()
        {
            this.Path = this.GetType().Name;
        }

        public string GameId { get; set; }
        public MapTile MapTile { get; set; }
    }
}
