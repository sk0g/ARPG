using TheKiwiCoder;
using UnityEngine;

public class MoveIntoAttackPosition : ActionNode
{
    [SerializeField] float attackRange;

    protected override void OnStart()
    {
        context.animator.SetFloat("Speed", .5f);
    }

    protected override void OnStop()
    {
        context.animator.SetFloat("Speed", .0f);
    }

    protected override State OnUpdate()
    {
        if (context.mover.DistanceToPlayer() <= attackRange) { return State.Success; }

        if (context.mover.DistanceToPlayer() >= attackRange + 5) { return State.Failure; }

        context.mover.WalkInDirection(context.mover.OffsetToPlayer());
        context.mover.LookAtDirection(context.mover.OffsetToPlayer());

        return State.Running;
    }
}