using AI.Actions;
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

        void Awake()
        {
            _mover = GetComponent<MoveAndStrafe>();
            _p = gameObject.AddComponent<Perception>();
        }

        void FixedUpdate()
        {
            if (_p.distanceToPlayer > combatDistanceToKeep)
            {
                _mover.LookAtDirection(_p.offsetToPlayer);
                _mover.MoveInDirection(_p.offsetToPlayer);
            }
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