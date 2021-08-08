using Interfaces;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour, IPusher, IDirectionalMover
    {
        [SerializeField] float movementSpeed = 5f;
        [SerializeField] MMFeedbacks stepFeedback;

        CharacterController _characterController;
        float _currentMovementMagnitude;

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void MoveInDirection(Vector3 currentMovement)
        {
            LookAtDirection(currentMovement);
            _currentMovementMagnitude = currentMovement.magnitude;

            _characterController.Move(
                movementSpeed * Time.fixedDeltaTime * currentMovement);
        }

        public void LookAtDirection(Vector3 offset) =>
            transform.LookAt(transform.position + offset);

        public void PushForward(float distance) => _characterController.Move(
            transform.forward * distance);

        void Step()
        {
            if (stepFeedback == null || _currentMovementMagnitude < .5f) { return; }

            stepFeedback.PlayFeedbacks(transform.position, _currentMovementMagnitude);
        }

        public void FootR()
        {
            Step();
        }

        public void FootL()
        {
            Step();
        }
    }
}