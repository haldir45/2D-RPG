using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance {
        get {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    /// <summary>
    /// The action buttons in the action bar
    /// </summary>
    [SerializeField]
    private Button[] actionButtons;

    /// <summary>
    /// The keycodes of the action buttons
    /// </summary>
    private KeyCode action1, action2, action3;

    /// <summary>
    /// The target's frame
    /// </summary>
    [SerializeField]
    private GameObject targetFrame;

    ///<summary>
    /// The target's portrait frame
    /// </summary>
    [SerializeField]
    private Image portraitFrame;

    /// <summary>
    /// The target's health stat
    /// </summary>
    private Stat healthStat;

    // Start is called before the first frame update
    void Start()
    {

        healthStat = targetFrame.GetComponentInChildren<Stat>();


        action1 = KeyCode.Alpha1;
        action2 = KeyCode.Alpha2;
        action3 = KeyCode.Alpha3;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(action1))
        {
            ActionButtonOnClick(0);
        }

        if (Input.GetKeyDown(action2))
        {
            ActionButtonOnClick(1);

        }

        if (Input.GetKeyDown(action3))
        {
            ActionButtonOnClick(2);

        }
    }

    /// <summary>
    /// Triggers the action button's onClick event
    /// </summary>
    /// <param name="buttonIndex"></param>
    private void ActionButtonOnClick(int buttonIndex)
    {
        actionButtons[buttonIndex].onClick.Invoke();
    }

    /// <summary>
    /// Shows the target NPC's frame
    /// </summary>
    /// <param name="target"></param>
    public void ShowTargetFrame(NPC target)
    {
  
        targetFrame.SetActive(true);

        portraitFrame.sprite = target.Portrait;

        healthStat.Initialize(target.Health.MyCurrentValue, target.Health.MyMaxValue);

        target.healthChanged += UpdateTargetFrame;

        target.characterRemoved += HideTargetFrame;

    }

    /// <summary>
    /// Hides the target NPC's frame
    /// </summary>
    /// <param name="target"></param>
    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
    }

    ///<summary>
    ///Updates the target NPC's frame
    ///</summary>
    public void UpdateTargetFrame(float health)
    {
        healthStat.MyCurrentValue = health;
    }
 
}
