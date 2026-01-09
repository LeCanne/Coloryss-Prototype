using System.Collections;
using System.Net;
using UnityEngine;

public class ExamplePattern : PatternHolder
{
    [Header ("Pattern Info")]
    public Projectile baseAttackProjectile;
    public BehaviorDataContainer directProjectile;
    public BehaviorDataContainer rotateToPlayer;
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

            //AddDirectBehavior to direct projectile towards player.
            projectile.AddBehavior(directProjectile);
            DirectBehaviorData direct = new DirectBehaviorData
            {
                direction = PatternHandler.Instance.cursorPosition.position - projectile.transform.position
            };
            projectile.SetBehaviorData(directProjectile.behaviors[0], direct);

            //AddRotateBehavior towards player
            projectile.AddBehavior(rotateToPlayer);
            RotateTowardsBehaviorData rotation = new RotateTowardsBehaviorData
            {
                directionToRotate = PatternHandler.Instance.cursorPosition.position - projectile.transform.position
            };
            projectile.SetBehaviorData(rotateToPlayer.behaviors[0], rotation);


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
