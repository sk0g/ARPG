using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;

namespace Player.Weapons
{
public class Weapon : BaseWeapon
{
    Weapon(float range, string name) : base(range, name) { }

    List<Attack> _attacks;

    public override void Awake()
    {
        base.Awake();
        _attacks = GetComponentsInParent<Attack>().ToList();
    }

    public override void Hit() { }

    public override void Damage() { }

    void OnTriggerEnter(Collider other)
    {
        _attacks?.FirstOrDefault(a => WeaponCollider.isTrigger && a.enabled && a.IsAttacking)
                ?.OnTriggerEnter(other);
    }

    void OnCollisionEnter(Collision other)
    {
        _attacks?.FirstOrDefault(a => WeaponCollider.isTrigger && a.enabled && a.IsAttacking)
                ?.OnCollisionEnter(other);
    }

    public override void LightAttack1()
    {
        print($"collided with {name}");
    }

    public override void HeavyAttack1()
    {
        print($"collided with {name}");
    }
}
}