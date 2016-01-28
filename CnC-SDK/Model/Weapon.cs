using CnC.Base;

namespace CnC.Model
{
    public class Weapon : FireObject, IFireObject
    {
        public Weapon()
        {
            this.Path = this.GetType().Name;
        }

        public string Name { get; set; }
        public int Class { get; set; } //defines impact when fired and hit
        public int RotatingSpeed { get; set; }
        public int FiringRange { get; set; }
        public int ArmorDamage { get; set; }
        public int ShieldDamage { get; set; }
        public enWeaponStatus Status { get; set; }

    }
}
