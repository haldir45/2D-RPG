using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class has all the spells that is known to the player
/// </summary>
public class SpellBook : MonoBehaviour
{
    /// <summary>
    /// Casting bar's background's canvas group
    /// </summary>
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image fill;

    [SerializeField]
    private Image spellIcon;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private Text spellCastingTime;

    [SerializeField]
    private Spell[] spells;

    private Coroutine castRoutine,fadeBarRoutine;


    public Spell CastSpell(int index)
    {
        canvasGroup.alpha = 1;
        fill.fillAmount = 0;

        fill.color = spells[index].BarColor;
        spellIcon.sprite = spells[index].Icon;
        spellName.text = spells[index].Name;

        castRoutine = StartCoroutine(Progress(index));

        fadeBarRoutine = StartCoroutine(FadeBar());

        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / spells[index].CastTime;

        float progress = 0.0f;

        while(progress <= 1.0)
        {
            fill.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;


            spellCastingTime.text = (spells[index].CastTime - timePassed).ToString("F2");

            if(spells[index].CastTime - timePassed < 0)
            {
                spellCastingTime.text = "0.0";
            }

            yield return null;
        }

        StopCasting();

    }

    private IEnumerator FadeBar()
    { 
        //The 0.5 value represents how fast the fade out will be
        float rate = 1.0f / 0.50f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }

    }


    public void StopCasting()
    {
        if(castRoutine != null)
        {
            StopCoroutine(castRoutine);
            castRoutine = null;
        }

        if(fadeBarRoutine != null)
        {
            StopCoroutine(fadeBarRoutine);
            canvasGroup.alpha = 0;
            fadeBarRoutine = null;
        }
    }

}
