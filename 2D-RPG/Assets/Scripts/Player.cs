using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    /// <summary>
    /// The player's health and mana stat
    /// </summary>
    [SerializeField]
    private Stat health,mana;

    /// <summary>
    /// The player's initial Health,Mana and maximum Heald,Mana
    /// </summary>
    [SerializeField]
    private float initHealth, maxHealth, initMana, maxMana;

    /// <summary>
    /// The player's spells
    /// </summary>
    [SerializeField]
    private GameObject[] spellPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        health.Initialize(initHealth, maxHealth);
        mana.Initialize(initMana, maxMana);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        base.Update();
    }

    private void GetInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isAttacking && !IsMoving)
                attackRoutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
      
            isAttacking = true;

            animator.SetBool("attack", isAttacking);

            yield return new WaitForSeconds(1.5f);

            CastSpell();    

            StopAttack();
        

    }

    private void CastSpell()
    {
        Instantiate(spellPrefab[0], transform.position, Quaternion.identity);
    }
}
