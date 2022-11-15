using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDisplay : MonoBehaviour
{
    public MinimapObject minimapObjectType;
    public GameObject minimapComponent;
    private SpriteRenderer spriteRend;

    
    void Awake()
    {
        minimapComponent = transform.Find("MinimapSymbol").gameObject;
        spriteRend = minimapComponent.GetComponent<SpriteRenderer>();
        spriteRend.sprite = minimapObjectType.iconSprite;
        spriteRend.color = minimapObjectType.color;
    }


}
