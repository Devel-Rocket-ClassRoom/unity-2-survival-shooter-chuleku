using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public float spawnTime;
    public float maxSpawnTime;
    public float currentSpawnTime;
    public float totalSpawnTime;
    public GameObject[] ZombieData;
    public float maxDistance = 33f;
    private void Start()
    {
        spawnTime = 5f;
        maxSpawnTime = 0.5f;
        currentSpawnTime = 0f;
        totalSpawnTime = 0f;
    }
    private void Update()
    {
        currentSpawnTime += Time.deltaTime;
        totalSpawnTime += Time.deltaTime;
        if (currentSpawnTime > spawnTime)
        {
            CreateZombie();
            currentSpawnTime = 0f;
        }
        if(totalSpawnTime > 10f)
        {
            spawnTime -= 0.1f;
        }
        if(spawnTime<=maxSpawnTime)
        {
            spawnTime = maxSpawnTime;
        }
    }


    private void CreateZombie()
    {
        var randomzombie = Random.Range(0,ZombieData.Length);
        var randomPos = Random.insideUnitSphere * maxDistance;
        if (!NavMesh.SamplePosition(randomPos, out NavMeshHit hit, maxDistance, NavMesh.AllAreas))
        {
            return;
        }
        randomPos = hit.position;
        randomPos.y = 0.5f;
        var zombie = Instantiate(ZombieData[randomzombie], randomPos,Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {

            var newZombie = zombie.GetComponent<Zombie>();
            if (newZombie != null)
            {
                newZombie.SetTarget(player.transform);
            }
        }
    }
}
