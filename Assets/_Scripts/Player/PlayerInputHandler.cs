using System;
using UnityEngine;
using UnityEngineInternal.Input;

public class PlayerInputHandler : MonoBehaviour
{

    public bool isGrounded;
    public Transform groundCheckPosition;
    public bool isJumping;
    public bool isFalling;
    
    public LayerMask whatIsGround;
    [HideInInspector] public Rigidbody2D m_rigidbody;
    [HideInInspector] public Animator m_animator;
    public float moveVel;
    public float jumpForce;
    private CommandProcessor _commandProcessor;
    private Command _walkCommand, _jumpCommand;
    [HideInInspector] public float moveX;
    private static readonly int VelX = Animator.StringToHash("VelX");

    private void Awake()
    {
        _commandProcessor = new CommandProcessor();
        _walkCommand = new WalkCommand(this, moveVel);
        _jumpCommand = new JumpCommand(this, jumpForce);

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = transform.GetChild(0).GetComponent<Animator>();

        isJumping = false;
        isFalling = false;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f, whatIsGround);
        moveX = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump"))
        {
            print("Jump");
            _commandProcessor.Execute(_jumpCommand);
        }

        if (moveX >= .9f || moveX <= -.9f)
            _commandProcessor.Execute(_walkCommand);
        

        else if (moveX < .9f && moveX > -.9f && !isJumping)
        {
            m_animator.SetInteger(VelX, 0);
            m_rigidbody.velocity = new Vector2(0, m_rigidbody.velocity.y);
        }

        if (isFalling && isGrounded)
        {
            isJumping = false;
            isFalling = false;
            m_animator.Play("Player_Idle");
        }

        if (isJumping && m_rigidbody.velocity.y < 0 && !isFalling)
            isFalling = true;
    }

    public void Flip()
    {
        var localScale = transform.localScale;
        transform.localScale = new Vector3(localScale.x * -1 , localScale.y, localScale.z);
    }
}
