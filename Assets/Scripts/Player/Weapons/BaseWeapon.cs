using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

// TODO: Add more?? events? 
public abstract class BaseWeapon : IAttack
{
    // every weapon needs a name 
    private string _name;
    protected string Name { get; set; }

    // how far a weapon can reach, excludes arm length and leg? length
    private float _range;
    protected float Range { get; set; }
    
    public BaseWeapon(float r, string n)
    {
        Range = r;
        Name = n;
    }

    public abstract void Damage();
    public abstract void Hit();
    
    public abstract void LightAttack1();

    public abstract void HeavyAttack1();

}
