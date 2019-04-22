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
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        //JUST FOR TESTING
        target = GameObject.Find("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;

        rigidbody.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
