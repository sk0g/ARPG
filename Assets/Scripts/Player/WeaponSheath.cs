using System.Collections.Generic;
using Player;
using Player.Weapons;
using UnityEngine;

public class WeaponSheath : MonoBehaviour
{
    [SerializeField] List<Weapon> Weapons;
    int _weaponIndex = 0;

    void Awake()
    {
        _weaponIndex = 0;
        SetWeaponAnimations();
    }

    void SetWeaponAnimations()
    {
        // not bare-fist!
        if (CurrentWeapon() != null && WeaponAnimations() != null)
        {
            // TODO: Define a player component's interface similar to BTree's context
            GetComponent<PlayerAnimationController>().SetAnimations(WeaponAnimations());
        }
    }

    Weapon CurrentWeapon()
    {
        return Weapons[_weaponIndex];
    }

    public bool WeaponIsTrigger()
    {
        return CurrentWeapon()?.WeaponCollider.isTrigger ?? false;
    }

    void NextWeapon() => _weaponIndex += 1 % Weapons.Count;
    void PreviousWeapon() => _weaponIndex -= 1 % Weapons.Count;

    AnimatorOverrideController WeaponAnimations()
    {
        return CurrentWeapon().WeaponAnimations;
    }

    public void SetColliderState(bool b) => CurrentWeapon().SetColliderState(b);
}