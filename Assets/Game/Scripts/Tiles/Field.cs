using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Tile {
    int randNum;

    protected override void Start()
    {
        base.Start();
        //SpawnObstacles();
    }

    protected override void SpawnObstacles()
    {
        base.SpawnObstacles();

        for (int i = 0; i < SPAWN_POINTS.Length; i++)
        {
            if (i < 14 || i > 49)
            {
                int randObs = Random.Range(0, LevelManager.FIELD_OBSTACLE_PREFABS.Length);
                GameObject temp = Instantiate(LevelManager.FIELD_OBSTACLE_PREFABS[randObs], transform.parent);
                temp.transform.parent = transform;
                temp.transform.localPosition = SPAWN_POINTS[i];
                usedSpawnPoints[i] = true;
            }
        }

        randNum = Random.Range(0, LevelManager.MAX_NUM_OF_OBSTACLES); //Number of possible obstacles
        for (int i = 0; i < randNum; i++)
        {
            int randSpawn = Random.Range(14, 48);
            if (!usedSpawnPoints[randSpawn])
            {
                if (isPlayerSafeZone && randSpawn > 25 && randSpawn < 35)
                {
                    randNum -= 1;
                }
                else
                {
                    int randObs = Random.Range(0, LevelManager.FIELD_OBSTACLE_PREFABS.Length);
                    GameObject temp = Instantiate(LevelManager.FIELD_OBSTACLE_PREFABS[randObs], transform.parent);
                    temp.transform.parent = transform;
                    temp.transform.localPosition = SPAWN_POINTS[randSpawn];
                    usedSpawnPoints[randSpawn] = true;
                    randNum -= 1;
                }
            }
        }
    }

    //protected override void SpawnObstacles()
    //{
    //    base.SpawnObstacles();
    //
    //    int randNum = Random.Range(0, 5); //Number of possible obstacles
    //
    //    for (int i = 0; i < randNum; i++)
    //    {
    //        int randSpawn = Random.Range(0, SPAWN_POINTS.Length);
    //        if (!usedSpawnPoints[randSpawn])
    //        {
    //            int randObs = Random.Range(0, LevelManager.OBSTACLE_PREFABS.Length);
    //            GameObject temp = Instantiate(LevelManager.OBSTACLE_PREFABS[randObs], transform.parent);
    //            temp.transform.parent = transform;
    //            temp.transform.localPosition = SPAWN_POINTS[randSpawn];
    //            usedSpawnPoints[randSpawn] = true;
    //        }
    //        else i -= 1;
    //    }
    //}
}
