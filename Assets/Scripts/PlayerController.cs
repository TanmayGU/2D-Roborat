using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections) /*typeof(Damageable)*/)]
public class PlayerController : MonoBehaviour
{

    public float walkspeed = 5f;
    public float runspeed = 8f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    public Text LEVELCOMPLETETEXT;
    //Damageable damageable;
    public GameManagerScript gameManager;


    public float CurrentMoveSpeed { get 
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (IsRunning)
                    {
                        return runspeed;
                    }
                    else
                    {
                        return walkspeed;
                    }

                }
                else
                {
                    //idle state
                    return 0;
                }
            }
            else
            {
                ///movement locked
                return 0;
            }
             
        }
         
        }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving { get 
        { 
        return _isMoving;
        } private set 
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }


    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning { get 
        {
            return _isRunning;
        }
        set 
        { 
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight { get 
        {
            return _isFacingRight;  
        } private set 
        {
           if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight= value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }


    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    public bool LockVelocity { 
        get 
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        //set
        //{
        //    animator.SetBool(AnimationStrings.lockVelocity, value);
        //}
    }

    Rigidbody2D rb;
    Animator animator;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        //damageable = GetComponent<Damageable>();
        
    }



    private void FixedUpdate()
    {
        if(!LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if(IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            setFacingDirection(moveInput);
            Debug.Log("Moving");
        }
        else
        {
            PlayerInput input = GetComponent<PlayerInput>(); input.actions.Disable();
        }
        
    }

    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
   
        {
            ///face right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            ///face left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    private bool _isPaused = false;
    public bool IsPaused
    {
        get
        {
            return _isPaused;
        }
        set
        {
            _isPaused = value;
            //animator.SetBool(AnimationStrings.isAlive, value);
            //Debug.Log("IsAlive set" + value);
        }
    }

    //public void OnPause(InputAction.CallbackContext context)
    //{
    //    if (context.started && IsPaused == false)
    //    {
    //        IsPaused = true;
    //        gameManager.Pause();
    //        //Time.timeScale = 0f;
    //    }
    //    else if (context.started && IsPaused == true)
    //    {
    //        IsPaused = false;
    //        gameManager.Resume();
    //        //Time.timeScale = 1;
    //        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //        //SceneManager.LoadScene("MainLevel");
    //    }
    //}

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.started && !touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, -jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && Time.timeScale == 1)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);

        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        //LockVelocity = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Win")
        //{
        //    if (gameObject.name(KnightEnemy (8)) isDestroyed)
        //}
        {
            LEVELCOMPLETETEXT.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
 
}

