using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 5f;
    private float timeSinceSpawn;
    private EnemyObjectPool objectPool;

    void Start()
    {
        objectPool = FindObjectOfType<EnemyObjectPool>();
    }

    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if(timeSinceSpawn >= timeToSpawn)
        {
            GameObject newZombie = objectPool.GetZombie();
            newZombie.transform.position = this.transform.position;
            timeSinceSpawn = 0f;
        }
    }
}
