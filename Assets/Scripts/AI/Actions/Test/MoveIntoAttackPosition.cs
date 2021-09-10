using TheKiwiCoder;
using UnityEngine;

namespace AI.Actions.Test
{
public class MoveIntoAttackPosition : ActionNode
{
    static readonly int Speed = Animator.StringToHash("Speed");
    [SerializeField] float attackRange;

    protected override void OnStart()
    {
        context.animator.SetFloat(Speed, .5f);
    }

    protected override void OnStop()
    {
        context.animator.SetFloat(Speed, .0f);
    }

    protected override State OnUpdate()
    {
        var distanceToPlayer = context.mover.DistanceToPlayer();

        if (distanceToPlayer >= attackRange + 5) { return State.Failure; }

        var facingPlayer = context.mover.RoughlyFacingPlayer(.8f);

        var withinAttackRange = distanceToPlayer <= attackRange;

        switch (facingPlayer, withinAttackRange)
        {
        case (true, true):
            return State.Success;
        case (false, _):
            context.mover.LookAtDirection(context.mover.OffsetToPlayer());
            break;
        case (_, false):
            context.mover.WalkInDirection(context.mover.OffsetToPlayer());
            break;
        }

        return State.Running;
    }
}
}