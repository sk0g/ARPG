using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Player.Weapons;
using UnityEngine;

namespace Actions
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] float cooldownTime = .3f;
        [SerializeField] float damageAmount = 10f;
        [SerializeField] string animationTriggerName = "Attack1";
        [SerializeField] Weapon currentWeapon;
        
        bool _isAttacking;
        bool _canAttackAgain = true;

        public bool CanAttackAgain() => !_isAttacking && _canAttackAgain;

        public bool IsAttacking() => _isAttacking;

        List<GameObject> _objectsDamagedThisAttack = new List<GameObject>();

        public IEnumerator StartAttack()
        {
            gameObject.SendMessage("SetTrigger", animationTriggerName);
            gameObject.BroadcastMessage("StartingAttack");
            _isAttacking = true;

            yield return null;
        }

        public void StartAttackSwing()
        {
            currentWeapon.WeaponCollider.isTrigger = true;
            _objectsDamagedThisAttack.Clear();
        }

        public void EndAttackSwing() => currentWeapon.WeaponCollider.isTrigger = false;

        public IEnumerator EndAttack()
        {
            _isAttacking = false;
            gameObject.BroadcastMessage("EndingAttack");

            yield return new WaitForSeconds(cooldownTime);

            _canAttackAgain = true;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == gameObject) { return; }

            var damageable = other.GetComponent<IDamageable>();
            if (other.name != "Axe_M" && other.name != "Plane") print($"collided with {other.name}");

            if (damageable == null || _objectsDamagedThisAttack.Contains(other.gameObject)) { return; }

            damageable.TakeDamage(damageAmount);
            _objectsDamagedThisAttack.Add(other.gameObject);
        }
    }
}