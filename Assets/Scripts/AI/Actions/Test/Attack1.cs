using System.Linq;
using Actions;
using TheKiwiCoder;
using UnityEngine;

namespace AI.Actions.Test
{
    public class Attack1 : ActionNode
    {
        Attack _attack;

        Attack Attack
        {
            get
            {
                if (_attack != null) { return _attack; }

                _attack = context.gameObject.GetComponents<Attack>()
                    .First(a => a.AnimationName == "Attack1");
                if (_attack != null) { return _attack; }

                Debug.LogError("BT attack action name must match Attack.animationTriggerName");
                return null;
            }
        }

        protected override void OnStart()
        {
            if (Attack == null) { Debug.LogError("Null attack :("); }

            if (Attack.CanAttack)
            {
                Attack.StopAllCoroutines();
                Attack.StartAttack();
            }
        }

        protected override void OnStop()
        {
            if (!Attack.CanAttack) { Attack.Interrupt(); }
        }

        protected override State OnUpdate()
        {
            if (Attack.IsAttacking) { return State.Running; }

            return State.Success;
        }
    }
}