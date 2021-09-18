using Core;
using UnityEngine;

namespace AI.Actions
{
    public class BT_Movement : MonoBehaviour
    {
        [SerializeField] float runSpeed = 3.5f;
        [SerializeField] float walkSpeed = 2f;

        [Tooltip("Degrees per second the character can turn")] [SerializeField]
        float rotationSpeed = 150f;

        CharacterController _cc;
        GameObject _player;
        Collider _playerCollider;
        DirectionModifier _directionalModifier;
        
        void Awake()
        {
            _cc = GetComponent<CharacterController>();

            _player = GameObject.FindWithTag("Player");
            _playerCollider = _player.GetComponent<Collider>();
            _directionalModifier = GetComponent<DirectionModifier>();
        }

        public void FootL() { } // Animation Event Sink for Attack1

        public void FootR() { } // Animation Event Sink for Attack1

        public void RunInDirection(Vector3 movementDirection)
        {
            _cc.SimpleMove(movementDirection.normalized * runSpeed);
        }
           

        public void WalkInDirection(Vector3 movementDirection) =>
            _cc.SimpleMove(movementDirection.normalized * walkSpeed);

        public bool RoughlyFacingPlayer(float threshold) =>
            Vector3.Dot((_playerCollider.ClosestPoint(transform.position) - transform.position).normalized,
                        transform.forward)
            >= threshold;

        public float DistanceToPlayer() =>
            Vector3.Distance(transform.position, _playerCollider.ClosestPoint(transform.position));

        public Vector3 OffsetToPlayer()
        {
            var offset = _player.transform.position - transform.position;
            offset.y = 0;
            return offset;
        }

        public void LookAtDirection(Vector3 direction) =>
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.fixedDeltaTime * rotationSpeed);
        
        public void LookAndRunInDirection(Vector3 direction)
        {
            Vector3 adjustedDirection = _directionalModifier.ClosestOffsetToDirection(direction);
            LookAtDirection(adjustedDirection);
            RunInDirection(adjustedDirection);
        }
    }   
}