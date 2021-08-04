using System;
using Core;
using TheKiwiCoder;
using UnityEngine;

namespace AI.Actions.Test
{
    public class RecoverFromHitState : ActionNode
    {
        Health _health;
        float lastHitAt = -1f;

        public override void Awake()
        {
        }
        
        protected override void OnStart()
        {
            _health = context.gameObject.GetComponentInParent<Health>();
            _health.onHit.AddListener(ReactToHit);
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