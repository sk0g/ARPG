using Actions;
using Core.Managers;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    [RequireComponent(typeof(Dash), typeof(IDirectionalMover))]
    public class PlayerInputHandler : MonoBehaviour
    {
        PlayerInputActions _inputActions;
        IDirectionalMover _directionalMover;
        Dash _dasher;
        Attack _attack1;
        GameManager _gameManager;

        void Awake()
        {
            _directionalMover = GetComponent<IDirectionalMover>();
            _dasher = GetComponent<Dash>();
            _attack1 = GetComponent<Attack>();
            _gameManager = GameObject.FindWithTag("GameController")?.GetComponent<GameManager>();
        }

        Vector3 _currentMovement;
        bool _shouldDash;
        bool _shouldAttack;

        #region Input Reader

        public void ReadMovement(InputAction.CallbackContext ctx)
        {
            var movementVec2 = ctx.ReadValue<Vector2>();
            _currentMovement = Quaternion.Euler(0, 45, 0) * new Vector3(movementVec2.x, 0, movementVec2.y);
        }

        public void ReadDash(InputAction.CallbackContext ctx)
        {
            if (ctx.started) { _shouldDash = true; }

            if (ctx.canceled && _shouldDash) { _shouldDash = false; }
        }

        public void ReadAttack1(InputAction.CallbackContext ctx)
        {
            if (ctx.started) { _shouldAttack = true; }

            if (ctx.canceled && _shouldAttack) { _shouldAttack = false; }
        }

        public void ReadPause(InputAction.CallbackContext ctx)
        {
            if (ctx.started) { _gameManager.TogglePause(); }
        }

        #endregion

        void FixedUpdate()
        {
            if (_gameManager.IsPaused) { return; }

            if (_shouldDash && CanDash) { DoDash(); }
            else if (_shouldAttack && CanAttack) { DoAttack(); }
            else if (CanMove) { DoMove(); }
        }

        bool CanDash => !(_dasher.isDashing || _attack1.IsAttacking);

        bool CanAttack => !_dasher.isDashing && _attack1.CanAttack;

        // due to how the animator's speed value is set in PlayerAnimationController.UpdateAnimatorSpeedValue(),
        // it is important to move even if the movement input is Vector3.zero
        bool CanMove => !(_dasher.isDashing || _attack1.IsAttacking);

        void DoDash()
        {
            if (_currentMovement != Vector3.zero) { _directionalMover.LookAtDirection(_currentMovement); }

            StartCoroutine(_dasher.StartDash());
            _shouldDash = false;
        }

        void DoAttack()
        {
            if (_currentMovement != Vector3.zero) { _directionalMover.LookAtDirection(_currentMovement); }

            _attack1.StartAttack();
            _shouldAttack = false;
        }

        void DoMove() => _directionalMover.MoveInDirection(_currentMovement);
    }
}