using Interfaces;
using UnityEngine;

namespace Player.Weapons
{
    [RequireComponent(typeof(Collider))]
    public abstract class BaseWeapon : MonoBehaviour, IAttack
    {
        // TODO: actually have this access the collider automatically
        [SerializeField] private Collider weaponCollider;
        public Collider WeaponCollider   { 
            get => weaponCollider;
            private set => weaponCollider = value;
        }
        
        protected string Name => gameObject.name;

        // every weapon needs a name 
        [SerializeField] private string description;
        public string Description
        {
            get => description;
            private set => description = value;
        }

        // how far a weapon can reach, excludes arm length and leg? length
        [SerializeField] private float range;
        protected float Range   { 
            get => range;
            private set => range = value;
        }

        protected BaseWeapon(float r, string d)
        {
            Range = r;
            Description = d;
        }

        public virtual void Awake()
        {
            print($"{Name} ready to clap cheeks!");
        }

        public abstract void Damage();
        public abstract void Hit();
        public abstract void LightAttack1();
        public abstract void HeavyAttack1();
    }
}