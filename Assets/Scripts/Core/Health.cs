using Interfaces;
using MoreMountains.Tools;
using UnityEngine;

namespace Core
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxHP = 100f;
        [SerializeField] float currentHP;

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
        }

        void UpdateHealthBar() => _healthBar.UpdateBar(currentHP, 0f, maxHP, true);
    }
}