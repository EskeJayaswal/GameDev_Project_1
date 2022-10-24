using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonEffect : MonoBehaviour
{



    public void OnPointerEnter()
    {
        transform.localScale = new Vector2(1.5f, 1.5f);
    }

    public void OnPointerExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }
}
