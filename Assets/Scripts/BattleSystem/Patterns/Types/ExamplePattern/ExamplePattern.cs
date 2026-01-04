using System.Net;
using UnityEngine;

public class ExamplePattern : PatternHolder
{
    [Header ("Pattern Info")]
    public Projectile baseAttackProjectile;
    public Transform[] bulletLocations;
    int projectilesSpawned;

    float timeSpawns;
    public float spawnIntervals;
    public int loops;
    private int currentloops;


    public override void StartPattern()
    {
        patternOver = false;
    }

    private void Update()
    {
        if (patternOver == false)
        {
            HandleSpawnTime();
        }
    }

    void HandleSpawnTime()
    {
        timeSpawns += Time.deltaTime;
        if(timeSpawns > spawnIntervals)
        {
            timeSpawns = 0;
            SpawnProjectile();
        }
    }
    void SpawnProjectile()
    {
        if(projectilesSpawned < bulletLocations.Length)
        {
            Instantiate(baseAttackProjectile, bulletLocations[projectilesSpawned]);
            projectilesSpawned++;
        }
        else
        {
           CheckSpawns();
        }
      
    }

    void CheckSpawns()
    {
        currentloops++;

        if (loops < currentloops)
        {
            patternOver = true;
            EndPattern();
           
        }
        else
        {
            projectilesSpawned = 0;
        }
    }

    




}
