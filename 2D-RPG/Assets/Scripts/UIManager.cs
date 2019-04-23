using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// The action buttons in the action bar
    /// </summary>
    [SerializeField]
    private Button[] actionButtons;

    /// <summary>
    /// The keycodes of the action buttons
    /// </summary>
    private KeyCode action1, action2, action3;

    // Start is called before the first frame update
    void Start()
    {
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
}
