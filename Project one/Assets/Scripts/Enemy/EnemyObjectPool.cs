using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;
    [SerializeField]
    private Queue<GameObject> zombiePool = new Queue<GameObject>();
    [SerializeField]
    private int poolStartSize = 100;

    void Start()
    {
        for (int i = 0; i < poolStartSize; i++) 
        {
            GameObject zombie = Instantiate(zombiePrefab);
            zombiePool.Enqueue(zombie);
            zombie.SetActive(false);    
        }
    }

    // Happens in the EnemySpawner
    public GameObject GetZombie()
    {
        if (zombiePool.Count > 0)
        {
            // Dequeues after FIFO principle 
            GameObject zombie = zombiePool.Dequeue();
            zombie.SetActive(true);
            return zombie;
        }
        else
        {
            // If the Queue is empty instanciate new zombies
            GameObject zombie = Instantiate(zombiePrefab);
            return zombie;
        }
    }

    // happens in the EnemyReturn
    public void ReturnZombie(GameObject zombie)
    {
        zombiePool.Enqueue(zombie);
        zombie.SetActive(false);
    }
}

