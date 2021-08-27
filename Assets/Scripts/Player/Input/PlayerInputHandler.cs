using System.Linq;
using Actions;
using Core.Managers;
using Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
[RequireComponent(typeof(Dash), typeof(IDirectionalMover))]
public class PlayerInputHandler : MonoBehaviour
{
    Attack _attack1, _attack2;

    Vector3 _currentMovement;
    Dash _dasher;
    IDirectionalMover _directionalMover;
    PlayerInputActions _inputActions;
    bool _shouldAttack, _shouldAttack2;
    bool _shouldDash;

    bool CanDash => !_attack1.IsAttacking && _dasher.CanDash;

    bool CanAttack => !_dasher.IsDashing && _attack1.CanAttack;

    // due to how the animator's speed value is set in PlayerAnimationController.UpdateAnimatorSpeedValue(),
    // it is important to move even if the movement input is Vector3.zero
    bool CanMove => !(_dasher.IsDashing || _attack1.IsAttacking);

    void Awake()
    {
        _directionalMover = GetComponent<IDirectionalMover>();
        _dasher = GetComponent<Dash>();
        _attack1 = GetComponents<Attack>().First(a => a.AnimationName == "Attack1");
        _attack2 = GetComponents<Attack>().First(a => a.AnimationName == "Attack2");
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.PlayerCanAct) { return; }

        if (_shouldDash && CanDash) { DoDash(); }
        else if (_shouldAttack && CanAttack) { DoAttack(); }
        else if (_shouldAttack2 && CanAttack) { DoAttack2(); }
        else if (CanMove) { DoMove(); }
    }

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

    void DoAttack2()
    {
        if (_currentMovement != Vector3.zero) { _directionalMover.LookAtDirection(_currentMovement); }

        _attack2.StartAttack();
        _shouldAttack2 = false;
    }

    void DoMove() => _directionalMover.MoveInDirection(_currentMovement);

    #region Input Reader

    [UsedImplicitly]
    public void ReadMovement(InputAction.CallbackContext ctx)
    {
        var movementVec2 = ctx.ReadValue<Vector2>();
        _currentMovement = Quaternion.Euler(0, 45, 0) * new Vector3(movementVec2.x, 0, movementVec2.y);
    }

    [UsedImplicitly]
    public void ReadDash(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { _shouldDash = true; }

        if (ctx.canceled && _shouldDash) { _shouldDash = false; }
    }

    [UsedImplicitly]
    public void ReadAttack1(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { _shouldAttack = true; }

        if (ctx.canceled && _shouldAttack) { _shouldAttack = false; }
    }

    [UsedImplicitly]
    public void ReadAttack2(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { _shouldAttack2 = true; }

        if (ctx.canceled && _shouldAttack2) { _shouldAttack2 = false; }
    }

    [UsedImplicitly]
    public void ReadPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started) { GameManager.Instance.TogglePause(); }
    }

    #endregion
}
}