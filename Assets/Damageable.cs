///Damageable////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    //public GameManagerScript gameManager;

    private bool isDead;
    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            ///IF health below 0 character is dead
            if(_health <= 0 && !isDead)
            {
                isDead = true;
                //gameManager.gameOver();
                IsAlive = false;
            }
        }
    }
    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    public bool IsHit
    {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {
            animator.SetBool(AnimationStrings.isHit, value);
        }
    }

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set" +value);
        }
    }

 
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInvincible) 
        {
            if(timeSinceHit > invincibilityTime)
            {
                ///Remove Invincibility
                isInvincible = false;
                timeSinceHit = 0;

            }
            timeSinceHit += Time.deltaTime;
        }
     //  Hit(10);
    }
    public bool Hit(int damage , Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            //animator.SetTrigger(AnimationStrings.hitTrigger);
            // LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            return true;
        }
        return false;
    }

    
}
