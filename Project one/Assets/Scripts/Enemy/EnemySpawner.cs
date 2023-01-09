using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 30f;
    [SerializeField]
    private float offset = 1f;
    private float timeSinceSpawn;
    private EnemyObjectPool objectPool;

    void Start()
    {
        objectPool = FindObjectOfType<EnemyObjectPool>();
        
        // mellem 29 og 31 sekunder tager det før spawnersne spawner nye zombier så de ikke går i takt
        timeToSpawn = Random.Range(timeToSpawn - offset, timeToSpawn + offset);
        // Make them spawn from the beginning of the game
        timeSinceSpawn = timeToSpawn;
    }

    void Update()
    {
        // Keeps track of timesincespawn
        timeSinceSpawn += Time.deltaTime;

        if(timeSinceSpawn >= timeToSpawn)
        {
            Vector3 currentPos = this.transform.position;

            // Spawn zone is 10 x 20 so zombies dont chase in formations
            float randomX = Random.Range(-5.0f, 5.0f);
            float randomZ = Random.Range(-10.0f, 10.0f);

            Vector3 newPos = new Vector3(currentPos.x + randomX ,currentPos.y, currentPos.z + randomZ);

            GameObject newZombie = objectPool.GetZombie();

            // places the zombie on the NavMesh
            newZombie.GetComponent<NavMeshAgent>().Warp(newPos);
            timeSinceSpawn = 0f;

        }
    }
}
