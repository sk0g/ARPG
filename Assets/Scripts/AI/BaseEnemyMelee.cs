using System.Collections;
using AI.Actions;
using Core;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(MoveAndStrafe))]
    public class BaseEnemyMelee : MonoBehaviour
    {
        [SerializeField] float combatDistanceToKeep = 4f;
        [SerializeField] float attackRange = 1f;

        Perception _p;
        MoveAndStrafe _mover;
        Animator _anim;
        Health _health;

        void Awake()
        {
            _anim = GetComponent<Animator>();
            _mover = GetComponent<MoveAndStrafe>();
            _p = gameObject.AddComponent<Perception>();

            _health = GetComponent<Health>();
            _health.onHit.AddListener(ReactToHit);
        }

        void OnDisable()
        {
            _health.onHit.RemoveListener(ReactToHit);
        }

        [SerializeField] bool _canAct = true;

        void FixedUpdate()
        {
            if (!_canAct) { return; }

            if (_p.distanceToPlayer > combatDistanceToKeep) { Chase(); }
            else if (_p.distanceToPlayer > attackRange) { MoveIntoAttackRange(); }
            else { print("I suppose I could attack?"); }
        }

        void Chase()
        {
            _mover.LookAtDirection(_p.offsetToPlayer);
            _mover.MoveInDirection(_p.offsetToPlayer);
        }

        void MoveIntoAttackRange()
        {
            _mover.LookAtDirection(_p.offsetToPlayer);
            _mover.StrafeInDirection(_p.offsetToPlayer);
        }

        void ReactToHit()
        {
            _canAct = false;
            _anim.SetTrigger("Hit");

            StartCoroutine(WaitTillHitStateEnds());

            _canAct = true;
        }

        IEnumerator WaitTillHitStateEnds()
        {
            while (_anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }

            yield return new WaitForSeconds(.2f);
        }

        protected class Perception : MonoBehaviour
        {
            [SerializeField] int updatePerceptionEveryNTicks = 10;
            int _ticksTillPerceptionUpdate;

            void Awake()
            {
                _player = GameObject.FindWithTag("Player");

                UpdatePerception();
                _ticksTillPerceptionUpdate = updatePerceptionEveryNTicks;
            }

            void FixedUpdate()
            {
                if (--_ticksTillPerceptionUpdate > 0) { return; }

                UpdatePerception();
                _ticksTillPerceptionUpdate = updatePerceptionEveryNTicks;
            }

            void UpdatePerception()
            {
                UpdateLastPlayerPosition();
                UpdateDistanceToPlayer();
                UpdateOffsetToPlayer();
            }

            GameObject _player;

            public Vector3 lastPlayerPosition;

            void UpdateLastPlayerPosition() => lastPlayerPosition =
                _player.transform.position;

            public float distanceToPlayer;

            void UpdateDistanceToPlayer() => distanceToPlayer =
                Vector3.Distance(transform.position, lastPlayerPosition);

            public Vector3 offsetToPlayer;

            void UpdateOffsetToPlayer() => offsetToPlayer =
                lastPlayerPosition - transform.position;
        }
    }
}