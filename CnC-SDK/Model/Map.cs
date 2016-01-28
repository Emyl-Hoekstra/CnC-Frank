using CnC.Base;
using System;
using System.Collections.Generic;

namespace CnC.Model
{
    public class Map : FireObject, IFireObject
    {
        public Map()
        {
            this.Path = this.GetType().Name;
            this.MapTiles = new List<MapTile>();
        }

        public List<MapTile> MapTiles { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void LoadMap(string mapFile)
        {
            Array[] mapTiles = CnC.Helpers.CSVParser.getValues(mapFile, 1, 1);
            this.MapTiles = new List<MapTile>();

            int x = 0;
            int y = 0;
            foreach (var col in mapTiles)
            {
                foreach (var row in col)
                {
                    MapTile mapTile = new MapTile();
                    mapTile.CreatedAt = DateTime.Now;
                    mapTile.UpdatedAt = DateTime.Now;
                    mapTile.TerrainType = Convert.ToInt32(row);
                    mapTile.Xpos = y;
                    mapTile.Ypos = x;
                    mapTile.IsAccessible = (mapTile.TerrainType < 10 || mapTile.TerrainType == 100);
                    mapTile.IsSpawningPoint = (mapTile.TerrainType == 100);
                    this.MapTiles.Add(mapTile);
                    y++;
                }
                y = 0;
                x++;

            }
            this.VerticalTilesCount = y;
            this.HorizontalTilesCount = x;
        }

        public int HorizontalTilesCount { get; set; }
        public int VerticalTilesCount { get; set; }

    }
}
