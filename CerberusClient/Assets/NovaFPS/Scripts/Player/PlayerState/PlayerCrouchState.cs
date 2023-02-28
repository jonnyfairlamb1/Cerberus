using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NovaFPS;

public class PlayerCrouchState : PlayerBaseState {

    public PlayerCrouchState(PlayerStates currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
        _ctx.GetComponent<PlayerMovement>().events.OnCrouch.Invoke();
        _ctx.GetComponent<PlayerMovement>().StartCrouch();
    }

    public override void UpdateState() {
        CheckSwitchState();
        _ctx.GetComponent<PlayerMovement>().Look();
    }

    public override void FixedUpdateState() {
        HandleMovement();
    }

    public override void ExitState() {
    }

    public override void CheckSwitchState() {
        if (_ctx.GetComponent<PlayerStats>().health <= 0) SwitchState(_factory.Die());

        if (_ctx.GetComponent<PlayerMovement>().CanJump && InputManager.jumping && _ctx.GetComponent<PlayerMovement>().grounded && _ctx.GetComponent<PlayerMovement>().canJumpWhileCrouching && _ctx.GetComponent<PlayerMovement>().ReadyToJump) SwitchState(_factory.Jump());

        CheckUnCrouch();
    }

    public override void InitializeSubState() {
    }

    private bool canUnCrouch = false;

    private void HandleMovement() {
        _ctx.GetComponent<PlayerMovement>().Movement(_ctx.GetComponent<PlayerStats>().controllable);
        _ctx.GetComponent<PlayerMovement>().FootSteps();

        //if(_ctx.GetComponent<PlayerMovement>().grounded) _ctx.GetComponent<Rigidbody>().AddForce(Vector3.forward * Time.deltaTime);
    }

    private void CheckUnCrouch() {
        RaycastHit hitt;
        if (!InputManager.crouching) // Prevent from uncrouching when there´s a roof and we can get hit with it
        {
            if (Physics.Raycast(_ctx.transform.position, _ctx.transform.up, out hitt, 5.5f, _ctx.GetComponent<PlayerMovement>().weapon.hitLayer)) {
                canUnCrouch = false;
            } else
                canUnCrouch = true;
        }
        if (canUnCrouch) {
            _ctx.GetComponent<PlayerMovement>().events.OnStopCrouch.Invoke(); // Invoke your own method on the moment you are standing up NOT WHILE YOU ARE NOT CROUCHING
            _ctx.GetComponent<PlayerMovement>().StopCrouch();
            if (_ctx.transform.localScale == _ctx.GetComponent<PlayerMovement>().PlayerScale)
                SwitchState(_factory.Default());
        }
    }
}