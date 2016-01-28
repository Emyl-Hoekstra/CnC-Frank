using CnC.Base;
using System;
using System.Collections.Generic;

namespace CnC.Model
{


    public class Unit : FireObject, IFireObject, IGameObject
    {
        public Unit()
        {
            this.Path = this.GetType().Name;
            this.Weapons = new List<Weapon>();
            this.Status = enUnitStatus.Build;
            this.Updated = false;
            this.Position = new MapTile();
            this.MoveTarget = new MapTile();
        }



        public void Update()
        {
            //check time
            if (this.Status == enUnitStatus.Building && (DateTime.Now - this.CreatedAt).Seconds > this.BuildTimeInSeconds)
            {
                this.Status = enUnitStatus.Build;
                this.Updated = true;
            }
            else if (this.Status == enUnitStatus.Moving && (DateTime.Now - this.MoveStarted).Seconds > this.MovingSpeed)
            {
                this.Status = enUnitStatus.Stopped;
                this.Updated = true;
            }
        }

        public string UnitTypeId { get; set; }
        public bool Updated { get; set; }
        public bool HasFixedGun { get; set; }
        public int FixedGunFiringRange { get; set; }
        public int FixedGunArmorDamage { get; set; }
        public int FixedGunShieldDamage { get; set; }

        public DateTime MoveStarted { get; set; }
        public MapTile MoveTarget { get; set; }

        public MapTile Position { get; set; }

        public string Name { get; set; }
        public int Class { get; set; }
        public int MovingSpeed { get; set; }
        public int MovingRange { get; set; }
        public int RotatingSpeed { get; set; }
        public enHeading Heading { get; set; }

        public int MaxNumberOf { get; set; }
        public int BuildTimeInSeconds { get; set; }
        public int Price { get; set; }
        public int Armor { get; set; }
        public int RadarRange { get; set; }
        public int Shields { get; set; }

        public enUnitStatus Status { get; set; }
        public List<Weapon> Weapons { get; set; }



    }
}
