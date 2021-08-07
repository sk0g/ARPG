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

        MMHealthBar _healthBar;

        void Awake()
        {
            _healthBar = GetComponentInChildren<MMHealthBar>();
            currentHP = maxHP;

            // finally, draw the health bar
            UpdateHealthBar();
        }

        public void TakeDamage(float damageAmount)
        {
            currentHP = Mathf.Max(currentHP - damageAmount, 0);
            UpdateHealthBar();
            onHit.Invoke();

            if (currentHP == 0 && !gameObject.CompareTag("Player")) { Die(); }
        }

        public void Heal(float healAmount)
        {
            currentHP = Mathf.Min(maxHP, currentHP + healAmount);
            UpdateHealthBar();
        }

        void Die()
        {
            PickupManager.Instance.SpawnBloodPickup(transform.position);
            // TODO: ragdoll, disable physical interactions with player, instead of the below
            Invoke(nameof(DeathCleanup), .2f);
        }

        void DeathCleanup() => Destroy(gameObject);

        public bool CanHeal => currentHP < maxHP;

        void UpdateHealthBar() => _healthBar.UpdateBar(currentHP, 0f, maxHP, true);
    }
}