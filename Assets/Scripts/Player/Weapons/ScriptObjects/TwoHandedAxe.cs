using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Player.Weapons
{
    public class TwoHandedAxe : BaseWeapon
    {
        TwoHandedAxe(float range, string name) : base(range, name) {}
        
        public override void Hit() {}

        public override void Damage(){}

        public override void LightAttack1()
        {
            MonoBehaviour.print($"collided with {Name}");
        }

        public override void HeavyAttack1()
        {
            MonoBehaviour.print($"collided with {Name}");
        }
    }
}