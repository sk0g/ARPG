using TheKiwiCoder;
using UnityEngine;

namespace AI.Actions.Test
{
    public class MoveToPlayer : ActionNode
    {
        [SerializeField] float runSpeed = 4f;

        [Tooltip("Distance in meters the AI player will stop chasing the player in")] [SerializeField]
        float idealPlayerProximity = 4f;

        [Tooltip("Degrees per second the character can turn")] [SerializeField]
        float rotationSpeed = 150f;

        CharacterController _cc;
        GameObject _player;

        protected override void OnStart()
        {
            _player = GameObject.FindWithTag("Player");
            _cc = context.gameObject.GetComponent<CharacterController>();

            context.gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
        }

        protected override void OnStop()
        {
            context.gameObject.GetComponent<Animator>().SetFloat("Speed", 0f);
        }

        protected override State OnUpdate()
        {
            if (DistanceToPlayer() <= idealPlayerProximity) { return State.Success; }

            UpdatePositionAndLookDirection();

            return State.Running;
        }

        void UpdatePositionAndLookDirection()
        {
            MoveInDirection(OffsetToPlayer());
            LookAtDirection(OffsetToPlayer());
        }

        void MoveInDirection(Vector3 movementDirection) =>
            _cc.SimpleMove(movementDirection.normalized * runSpeed);

        float DistanceToPlayer() => Vector3.Distance(context.transform.position, _player.transform.position);

        Vector3 OffsetToPlayer() => _player.transform.position - context.transform.position;

        void LookAtDirection(Vector3 direction) =>
            context.transform.rotation = Quaternion.RotateTowards(
                from: context.transform.rotation,
                to: Quaternion.LookRotation(direction),
                maxDegreesDelta: Time.fixedDeltaTime * rotationSpeed);
    }
}