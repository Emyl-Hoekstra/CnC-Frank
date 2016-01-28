using CnC.Base;

namespace CnC.Model
{
    public class MapTile : FireObject, IFireObject
    {
        public MapTile()
        {

        }
        public int TerrainType { get; set; }
        public bool IsAccessible { get; set; }

        public int Xpos { get; set; }
        public int Ypos { get; set; }
        public bool IsSpawningPoint { get; set; }

       //public Unit Unit { get; set; }
    }
}