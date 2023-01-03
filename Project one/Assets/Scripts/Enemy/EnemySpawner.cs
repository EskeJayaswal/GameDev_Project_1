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
            Vector3 currentPos = this.transform.position;

            float randomX = Random.Range(-5.0f, 5.0f);
            float randomZ = Random.Range(-5.0f, 5.0f);

            Vector3 newPos = new Vector3(currentPos.x + randomX ,0, currentPos.z + randomZ);

            GameObject newZombie = objectPool.GetZombie();
            newZombie.transform.position = this.transform.position;
            newZombie.transform.position = newPos;
            timeSinceSpawn = 0f;
        }
    }
}
