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


        timeToSpawn = Random.Range(timeToSpawn - offset, timeToSpawn + offset);
        // Make them spawn from the beginning of the game
        timeSinceSpawn = timeToSpawn;
    }

    void Update()
    {
                timeSinceSpawn += Time.deltaTime;
        if(timeSinceSpawn >= timeToSpawn)
        {

            Vector3 currentPos = this.transform.position;

            // Spawn zone is 10 x 20
            float randomX = Random.Range(-5.0f, 5.0f);
            float randomZ = Random.Range(-10.0f, 10.0f);

            Vector3 newPos = new Vector3(currentPos.x + randomX ,currentPos.y, currentPos.z + randomZ);

            GameObject newZombie = objectPool.GetZombie();
            newZombie.GetComponent<NavMeshAgent>().Warp(newPos);
            //newZombie.transform.position = newPos;
            timeSinceSpawn = 0f;



        }
    }
}
