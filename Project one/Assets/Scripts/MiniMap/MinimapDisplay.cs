using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapDisplay : MonoBehaviour
{
    [SerializeField]
    private MinimapObject minimapObjectType; // ScriptalbleObject that contains a color and sprite/icon

    private GameObject minimapComponent; // Player and Enemies all have this child component.
    private SpriteRenderer spriteRend;

    
    void Awake()
    {
        // Made sure container is called "MinimapSymbol". 
        minimapComponent = transform.Find("MinimapSymbol").gameObject;
        
        // Get th Renderer so we can change color and sprite. To arrow and either Green or Red color.
        spriteRend = minimapComponent.GetComponent<SpriteRenderer>();
        
        // Assign the values to SpriteRenderer from the scriptable objects.
        spriteRend.sprite = minimapObjectType.iconSprite;
        spriteRend.color = minimapObjectType.color;
    }


}
