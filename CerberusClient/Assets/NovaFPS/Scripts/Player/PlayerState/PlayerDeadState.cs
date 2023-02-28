using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{

    public PlayerDeadState(PlayerStates currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
        Debug.Log("Dead");
        _ctx.GetComponent<PlayerStats>().LoseCrontol(); 
    }

    public override void UpdateState() {
        CheckSwitchState();
    }

    public override void FixedUpdateState() { }

    public override void ExitState() { }

    public override void CheckSwitchState() {
        /*if (InputManager.attacking != 0)
            SwitchState(_factory.Attack());*/

    }

    public override void InitializeSubState() { }

}
