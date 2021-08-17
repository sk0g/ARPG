using TheKiwiCoder;
using UnityEngine;

namespace AI.Actions.Test
{
    public class MoveToPlayer : ActionNode
    {
        [Tooltip("Distance in meters the AI player will stop chasing the player in")] [SerializeField]
        float idealPlayerProximity = 4f;

        BT_Movement _mover;
        static readonly int Speed = Animator.StringToHash("Speed");

        protected override void OnStart()
        {
            context.animator.SetFloat(Speed, 1f);
        }

        protected override void OnStop()
        {
            context.animator.SetFloat(Speed, 0f);
        }

        protected override State OnUpdate()
        {
            if (context.mover.DistanceToPlayer() <= idealPlayerProximity) { return State.Success; }

            UpdatePositionAndLookDirection();

            return State.Running;
        }

        void UpdatePositionAndLookDirection()
        {
            context.mover.RunInDirection(context.mover.OffsetToPlayer());
            context.mover.LookAtDirection(context.mover.OffsetToPlayer());
        }
    }
}