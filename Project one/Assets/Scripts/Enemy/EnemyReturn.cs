using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturn : MonoBehaviour
{
    private EnemyObjectPool objectPool;

    void Start()
    {
        objectPool = FindObjectOfType<EnemyObjectPool>();
    }

    private void OnDisable()
    {
        if(objectPool != null)
        {
            objectPool.ReturnZombie(this.gameObject);
        } 
    }
}
