using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIValueModifierFeedback : MonoBehaviour
{
    public TextMeshProUGUI[] modifTexts;
    public float feedbackDuration;

    public IEnumerator IndicateValueChange(int modificationValue, int modifType)
    {
        switch (modifType)
        {
            case 0:
                modifTexts[0].gameObject.SetActive(true);
                modifTexts[0].text = modificationValue.ToString();
                break;

            case 1:
                modifTexts[1].gameObject.SetActive(true);
                modifTexts[1].text = modificationValue.ToString();
                break;

            case 2:
                modifTexts[2].gameObject.SetActive(true);
                modifTexts[2].text = modificationValue.ToString();
                break;
        }

        yield return new WaitForSeconds(feedbackDuration);

        modifTexts[modifType].gameObject.SetActive(false);
    }
}
