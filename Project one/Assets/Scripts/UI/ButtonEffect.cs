using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ButtonEffect : MonoBehaviour
{
    TextMeshProUGUI buttonText;
    public Color buttonColor;
    public Color highlightedColor;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.color = buttonColor;

    }



    public void OnPointerEnter()
    {
        transform.localScale = new Vector2(1.5f, 1.5f);
        buttonText.color = highlightedColor;

        
    }

    public void OnPointerExit()
    {
        transform.localScale = new Vector2(1f, 1f);
        buttonText.color = buttonColor;
    }
}
