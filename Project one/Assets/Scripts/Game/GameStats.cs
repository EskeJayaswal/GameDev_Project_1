using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{


    // Singleton
    public static GameStats instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
