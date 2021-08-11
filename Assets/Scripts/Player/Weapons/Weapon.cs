using UnityEngine;

namespace Player.Weapons
{
    public class Weapon : BaseWeapon
    {
        Weapon(float range, string name) : base(range, name) {}

        public override void Awake()
        {
            base.Awake();
        }

        public override void Hit() {}

        public override void Damage(){}

        public override void LightAttack1()
        {
            print($"collided with {Name}");
            print($"collided with {Description}");
            print($"collided with {Range.ToString()}");
        }

        public override void HeavyAttack1()
        {
           print($"collided with {Name}");
           print($"collided with {Description}");
        }
    }
}