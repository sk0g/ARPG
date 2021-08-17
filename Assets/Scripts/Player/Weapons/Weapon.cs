using System.Linq;
using Actions;
using UnityEngine;

namespace Player.Weapons
{
    public class Weapon : BaseWeapon
    {
        Weapon(float range, string name) : base(range, name)
        {
        }

        public override void Hit()
        {
        }

        public override void Damage()
        {
        }

        void OnTriggerEnter(Collider other)
        {
            FindObjectsOfType<Attack>()?.First(a => a.IsAttacking).PassOnTriggerEnter(other);
        }

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