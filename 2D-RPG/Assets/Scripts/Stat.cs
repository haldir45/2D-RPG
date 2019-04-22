using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private Text statValue;

    private float currentFill;
    public float MyMaxValue { get; set; }


    private float currentValue;

    public float MyCurrentValue {
        get => currentValue;

        set {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if(value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;
            statValue.text = currentValue + "/" + MyMaxValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
   
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        content.fillAmount = currentFill;

        ///Decrease slowly the stat
        ///if(currentFill != content.fillAmount)
        ///{
        ///content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * 0.5f);
        ///}
        
    }

    public void Initialize(float currentValue,float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
