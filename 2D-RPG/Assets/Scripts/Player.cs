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
    /// The player's cast exit points
    /// </summary>
    [SerializeField]
    private Transform[] exitPoints;

    /// <summary>
    /// The exitIndex tracks of which exit point to use.
    /// Initial value facing down.
    /// </summary>
    private int exitIndex = 2;

    /// <summary>
    /// The player's blocks.
    /// The blocks are used for line of sight purposes.
    /// </summary>
    [SerializeField]
    private Block[] blocks;

    /// <summary>
    /// Block layerMask
    /// </summary>
    private const int blockLayerMask = 1 << 8;

    /// <summary>
    /// The player's target
    /// </summary>
    public Transform Target { get; set; }

    /// <summary>
    /// The player's Spellbook Reference
    /// </summary>
    [SerializeField]
    private SpellBook spellBook;

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
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }

    }

    private IEnumerator Attack(int spellIndex)
    {

            Spell newSpell = spellBook.CastSpell(spellIndex);

            isAttacking = true;

            animator.SetBool("attack", isAttacking);

            yield return new WaitForSeconds(newSpell.CastTime);

            SpellScript s = Instantiate(newSpell.SpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Target = Target;

            StopAttack();
        

    }

    public void CastSpell(int spellIndex)
    {
        Block();
        if (Target != null && !isAttacking && !IsMoving && InLineOfSight())
            attackRoutine = StartCoroutine(Attack(spellIndex));

    }

    /// <summary>
    /// Shoots a line to target's direction
    /// </summary>
    /// <returns></returns>
    private bool InLineOfSight()
    {
        Vector2 targetDirection = (Target.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, Target.transform.position), blockLayerMask);

        if(hit.collider == null)
        {
            return true;
        }

        return false;
    }

    ///<summary>
    ///Activates the correct set of blocks with index <param name="exitIndex"></param>
    /// </summary>
    private void Block()
    {
        foreach (Block b in blocks)
            b.Deactivate();
      
        blocks[exitIndex].Activate(); 


    }
}
