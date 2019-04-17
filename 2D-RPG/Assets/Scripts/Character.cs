using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    /// <summary>
    /// The Player's direction
    /// </summary>
    protected Vector2 direction;

    /// <summary>
    /// The Player's movement speed
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// The Player's Animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The Player's Rigibody
    /// </summary>
    private Rigidbody2D rigibody;

    public bool IsMoving {
        get {
            return direction.x != 0 || direction.y !=  0;
        }

    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
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

            //Sets the animation parameter so that he faces the right direction
            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
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

}
