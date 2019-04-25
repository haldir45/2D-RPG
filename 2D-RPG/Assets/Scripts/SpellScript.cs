using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellScript : MonoBehaviour
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
    public Transform Target { get; private set; }

    /// <summary>
    /// Spell's damage
    /// </summary>
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Transform target,int damage)
    {
        this.Target = target;
        this.damage = damage;
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
            speed = 0;
            collision.GetComponentInParent<Enemy>().TakeDamage(this.damage);
            GetComponent<Animator>().SetTrigger("Impact");
            rigidbody.velocity = Vector2.zero;
            Target = null;
        }
    }
}
