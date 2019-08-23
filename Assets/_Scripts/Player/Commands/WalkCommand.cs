using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCommand : Command
{
    private readonly float _moveVel;
    private static readonly int VelX = Animator.StringToHash("VelX");

    public WalkCommand(PlayerInputHandler playerInputHandler, float moveVel) : base(playerInputHandler)
    {
        this._moveVel = moveVel;
    }

    public override bool IsValid()
    {
        return true;
    }

    public override void Act()
    {
        playerInputHandler.m_rigidbody.velocity = new Vector2(playerInputHandler.transform.localScale.x * _moveVel, playerInputHandler.m_rigidbody.velocity.y);
    }

    public override void DoBeforeEntering()
    {
        if ((playerInputHandler.transform.localScale.x == 1 && playerInputHandler.moveX < 0) ||
            (playerInputHandler.transform.localScale.x == -1 && playerInputHandler.moveX > 0))
        {
            playerInputHandler.Flip();
        }
        
        if(!playerInputHandler.isJumping)
            playerInputHandler.m_animator.SetInteger(VelX,1);
    }
}
