using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class has all the spells that is known to the player
/// </summary>
public class SpellBook : MonoBehaviour
{
    [SerializeField]
    private Spell[] spells;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Spell CastSpell(int index)
    {
        return spells[index];
    }

}
