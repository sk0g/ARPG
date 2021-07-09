using Interfaces;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour, IPusher, IDirectionalMover
    {
        [SerializeField] float movementSpeed = 5f;

        CharacterController _characterController;

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void MoveInDirection(Vector3 currentMovement)
        {
            LookAtDirection(currentMovement);

            _characterController.Move(
                movementSpeed * Time.fixedDeltaTime * currentMovement);
        }

        public void LookAtDirection(Vector3 offset) =>
            transform.LookAt(transform.position + offset);

        public void PushForward(float distance) => _characterController.Move(
            transform.forward * distance);
    }
}