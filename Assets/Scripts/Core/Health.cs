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
        }

        void UpdateHealthBar() => _healthBar.UpdateBar(currentHP, 0f, maxHP, true);
    }
}