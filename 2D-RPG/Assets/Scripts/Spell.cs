using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    /// <summary>
    /// The spell's rigibody
    /// </summary>
    private Rigidbody2D rigidbody;

    /// <summary>
    /// The spell's speed
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// The spell's target
    /// </summary>
    public Transform Target { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Target != null)
        {
            Vector2 direction = Target.position - transform.position;

            rigidbody.velocity = direction.normalized * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
      
    }

    /// <summary>
    /// On collision with enemy triggers the impact animation
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyHitBox" && collision.transform == Target)
        {
            GetComponent<Animator>().SetTrigger("Impact");
            rigidbody.velocity = Vector2.zero;
            Target = null;
        }
    }
}
