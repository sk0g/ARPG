#region

using Interfaces;
using UnityEngine;

#endregion

namespace Player.Weapons
{
[RequireComponent(typeof(Collider))]
public abstract class BaseWeapon : MonoBehaviour, IAttack
{
    [SerializeField] AnimatorOverrideController weaponAnimations;

    public AnimatorOverrideController WeaponAnimations
    {
        get => weaponAnimations;
        set => weaponAnimations = value;
    }

    [SerializeField] Collider weaponCollider;

    public Collider WeaponCollider
    {
        get => weaponCollider;
        private set => weaponCollider = value;
    }

    protected BaseWeapon(float r, string d)
    {
    }

    public virtual void Awake()
    {
        if (WeaponCollider == null) { FetchCollider(); }

        if (WeaponAnimations == null) { FetchAnimations(); }
    }

    public void FetchAnimations()
    {
        print($"{name}'s Animations wasn't pre-set, attempting to fetch it");
    }

    public void FetchCollider()
    {
        print($"{name}'s Collider wasn't pre-set, attempting to fetch it");
        WeaponCollider = GetComponent<Collider>();
    }

    public abstract void Damage();
    public abstract void Hit();
    public abstract void LightAttack1();
    public abstract void HeavyAttack1();
}
}