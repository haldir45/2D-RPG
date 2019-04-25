using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The GameManager's player's reference
    /// </summary>
    [SerializeField]
    private Player player;

    private NPC currentTarget;

    /// <summary>
    /// Clickable layer
    /// </summary>
    private const int clickableLayerMask = 1 << 9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }

    /// <summary>
    /// Sets player's target to mouse's position when player clicks in the world
    /// </summary>
    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, clickableLayerMask);
            
            if(hit.collider != null)
            {
                if (currentTarget != null)
                    currentTarget.DeSelect();

                currentTarget = hit.collider.GetComponent<NPC>();

                player.Target = currentTarget.Select();
            }
            else
            {
                if(currentTarget != null)
                {
                    currentTarget.DeSelect();
                }

                currentTarget = null;
                player.Target = null;
            }
        }
     
    }
}
