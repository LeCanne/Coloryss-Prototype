using System.Collections;
using System.Net;
using UnityEngine;

public class ExamplePattern : PatternHolder
{
    [Header ("Pattern Info")]
    public Projectile baseAttackProjectile;
    public ProjectileBehavior followBehavior;
    public ProjectileBehavior rotateToPlayer;
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
            Projectile projectile = Instantiate(baseAttackProjectile, bulletLocations[projectilesSpawned]);
            projectile.AddBehavior(followBehavior);
            projectile.AddBehavior(rotateToPlayer);
            projectile.speed = 12f;
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
            EndPattern(1f);
           
        }
        else
        {
            projectilesSpawned = 0;
        }
    }

    

    




}
