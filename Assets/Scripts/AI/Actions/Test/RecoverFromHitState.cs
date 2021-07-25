using Core;
using TheKiwiCoder;
using UnityEngine;

namespace AI.Actions.Test
{
    public class RecoverFromHitState : ActionNode
    {
        Health _health;

        void Awake()
        {
            _health = context.gameObject.GetComponent<Health>();
            _health.onHit.AddListener(ReactToHit);
        }

        float lastHitAt = -1f;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        void ReactToHit()
        {
            context.animator.SetTrigger("Hit");
            lastHitAt = Time.time;
        }

        protected override State OnUpdate()
        {
            if (Time.time >= (lastHitAt + 1)) { return State.Failure; }

            return State.Running;
        }
    }
}