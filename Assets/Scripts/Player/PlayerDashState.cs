using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerGroundState
{
    private float startTime;
    private PlayerGroundData playerGroundData;

    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        startTime = Time.time;
        playerGroundData = stateMachine.player.data.groundedData;

        SetAnimationBool(stateMachine.player.animationData.dashParameterHash, true);
        /*
        alreadyApplyForce = false;

        SetAnimationBool(stateMachine.player.animationData.dashParameterHash, true);

        TryApplyForce();
        */
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("Exit Dash");

        SetAnimationBool(stateMachine.player.animationData.dashParameterHash, false);
    }

    public override void FixedUpdateState()
    {
        if (Time.time < startTime + playerGroundData.dashTime)
        {
            stateMachine.player.controller.Move(stateMachine.player.transform.forward * playerGroundData.dashSpeed * Time.fixedDeltaTime);
        }
    }

    public override void UpdateState()
    {
        /*
        if (Time.time < startTime + playerGroundData.dashTime)
        {
            stateMachine.player.controller.Move(stateMachine.player.transform.forward * playerGroundData.dashSpeed * Time.deltaTime);
        }
        */
        
        //ForceMove();
        
        float normalizedTime = GetNormalizedTime(stateMachine.player.animator, "Dash");

        if (normalizedTime >= 1f)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
        
    }


    /*
    private void TryApplyForce()
    {
        Debug.Log("Dash!");
        if (alreadyApplyForce) return;

        alreadyApplyForce = true;

        stateMachine.player.forceReceiver.Reset();

        // 전진 대쉬
        stateMachine.player.forceReceiver.AddForce(stateMachine.player.transform.forward * stateMachine.player.data.groundedData.dashForce);
    }
    */
}
