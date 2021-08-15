using TheKiwiCoder;
using UnityEngine;

public class Attack1 : ActionNode
{
    protected override void OnStart()
    {
        context.animator.SetTrigger("Attack");
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
