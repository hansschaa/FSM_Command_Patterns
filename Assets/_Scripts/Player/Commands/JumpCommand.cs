using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : Command
{
    private readonly float _jumpForce;
    private static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");

    public JumpCommand(PlayerInputHandler playerInputHandler, float jumpForce) : base(playerInputHandler)
    {
        this._jumpForce = jumpForce;
    }

    public override bool IsValid()
    {
        if (playerInputHandler.isGrounded)
            return true;
        
        return false;
    }

    public override void Act()
    {
        playerInputHandler.m_rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        //playerInputHandler.m_Animator.SetBool(playerInputHandler.groundedBool, false);
        playerInputHandler.m_animator.SetTrigger(JumpTrigger);
        
    }

    public override void DoBeforeEntering()
    {
        playerInputHandler.isJumping = true;
    }
}
