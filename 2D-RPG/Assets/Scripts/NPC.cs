using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);

public delegate void CharacterRemoved();

public class NPC : Character
{
    /// <summary>
    /// This event triggers when health is changed
    /// </summary>
    public event HealthChanged healthChanged;

    /// <summary>
    /// This event triggers when character is dead
    /// </summary>
    public event CharacterRemoved characterRemoved;

    [SerializeField]
    private Sprite portrait;

    public Sprite Portrait {
        get {
            return portrait;
        }
    }

    public virtual void DeSelect()
    {
        healthChanged -= UIManager.Instance.UpdateTargetFrame;

        characterRemoved -= UIManager.Instance.HideTargetFrame;
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if(healthChanged != null)
        {
            healthChanged(health);
        }
    }

    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }

        Destroy(gameObject);
    }
}
