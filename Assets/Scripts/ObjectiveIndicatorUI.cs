using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveIndicatorUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textUI;

    public void Start()
    {
        SetVisible(false);
    }

    public void SetVisible(bool visible)
    {
        if(visible)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    public void SetText(string text)
    {
        textUI.text = text;
    }
}
