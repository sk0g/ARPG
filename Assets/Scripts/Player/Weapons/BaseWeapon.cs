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
            set => weaponCollider = value;
        }

        protected string Name => gameObject.name;

        // every weapon needs a name 
        [SerializeField] string description;

        public string Description
        {
            get => description;
            private set => description = value;
        }

        // how far a weapon can reach, excludes arm length and leg? length
        [SerializeField] float range;

        protected float Range
        {
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
            if (WeaponCollider == null) { FetchCollider(); }

            if (WeaponAnimations == null) { FetchAnimations(); }

            print($"{Name} ready to clap cheeks!");
        }

        public void FetchAnimations()
        {
            Debug.LogWarning(Name + "'s Animations wasn't pre-set, attempting to fetch it");
        }

        public void FetchCollider()
        {
            Debug.LogWarning(Name + "'s Collider wasn't pre-set, attempting to fetch it");
            WeaponCollider = GetComponent<Collider>();
        }

        public abstract void Damage();
        public abstract void Hit();
        public abstract void LightAttack1();
        public abstract void HeavyAttack1();
    }
}