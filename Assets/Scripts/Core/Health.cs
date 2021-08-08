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

        Cleanup _cleanup;
        
        void Awake()
        {
            _healthBar = GetComponentInChildren<MMHealthBar>();
            _cleanup = GetComponent<Cleanup>();
            
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
            currentHP = Mathf.Max(currentHP - damageAmount, 0);
            UpdateHealthBar();
            onHit.Invoke();

            if (currentHP == 0 && !gameObject.CompareTag("Player")) { Die(); }
        }

        void Die()
        {
            PickupManager.Instance.SpawnBloodPickup(transform.position);
            // TODO: Maybe make this callable if exists rather then a hard variable set?
            _cleanup.CleanUp();
        }

        void DeathCleanup() => Destroy(gameObject);

        void UpdateHealthBar() => _healthBar.UpdateBar(currentHP, 0f, maxHP, true);
    }
}