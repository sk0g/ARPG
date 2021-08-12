using Core.Managers;
using Interfaces;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxHP = 100f;
        [SerializeField] float currentHP;
        public UnityEvent onHit;
        public UnityEvent<float> onDeath; // pass in the overkill amount for scaling blood and knock-back

        MMHealthBar _healthBar;

        void Awake()
        {
            _healthBar = GetComponentInChildren<MMHealthBar>();

            currentHP = maxHP;

            // finally, draw the health bar
            UpdateHealthBar();
        }

        public bool CanHeal => currentHP < maxHP;

        public void Heal(float healAmount)
        {
            currentHP = Mathf.Min(maxHP, currentHP + healAmount);
            UpdateHealthBar();
        }

        public void TakeDamage(float damageAmount)
        {
            onHit.Invoke();

            currentHP -= damageAmount; // may be less than 0, for the overkill

            if (currentHP <= 0 && !gameObject.CompareTag("Player")) { Die(); }

            currentHP = Mathf.Max(currentHP, 0); // should now be a valid value
            UpdateHealthBar();
        }

        void Die()
        {
            PickupManager.Instance.SpawnBloodPickup(transform.position);

            onDeath.Invoke(OverkillMultiplier);
        }

        // Below returns (hp => result) -20 => 3, -15 => 2.5, -5 => 1.5, 0 => 1, etc
        // Weird result when character is not dead though
        float OverkillMultiplier => (-currentHP / 10) + 1;

        void UpdateHealthBar() => _healthBar.UpdateBar(currentHP, 0f, maxHP, true);
    }
}