using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is an  abstract class that all characters needs to inherit from
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    /// <summary>
    /// Character's direction
    /// </summary>
    protected Vector2 direction;

    /// <summary>
    /// Character's movement speed
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// Character's Animator
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// Character's Rigibody
    /// </summary>
    private Rigidbody2D rigibody;

    /// <summary>
    /// Character's attacking condition
    /// </summary>
    protected bool isAttacking;

    /// <summary>
    /// Character's reference AttackCoroutine
    /// </summary>
    protected Coroutine attackRoutine;

    /// <summary>
    /// Character's hitbox
    /// </summary>
    [SerializeField]
    protected Transform hitBox;

    /// <summary>
    /// Character's health
    /// </summary>
    [SerializeField]
    protected Stat health;

    public Stat Health {
        get {
            return health;
        }
    }

    /// <summary>
    /// Character's initial and max health
    /// </summary>
    [SerializeField]
    private float initHealth, maxHealth;

    public bool IsMoving {
        get {
            return direction.x != 0 || direction.y !=  0;
        }

    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        health.Initialize(initHealth, maxHealth);

        animator = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rigibody.velocity = direction.normalized * speed;

    }

    /// <summary>
    /// Handling animator's layers
    /// </summary>
    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            //Sets the animation parameter so that he faces the correct direction
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);

            StopAttack();
        }else if (isAttacking)
        {
            
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }

    /// <summary>
    /// Activates the <paramref name="layerName"/>
    /// </summary>
    /// <param name="layerName"></param>
    public void ActivateLayer(string layerName) {

        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

    /// <summary>
    /// Stops or exits attacking animation
    /// </summary>
    public virtual void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            animator.SetBool("attack", isAttacking);
        }
 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(float damage)
    {
        health.MyCurrentValue -= damage;

        if(health.MyCurrentValue <= 0)
        {
            animator.SetTrigger("die");
            
        }

    }

}
