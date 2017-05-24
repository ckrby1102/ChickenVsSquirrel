using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : Tile {

    private void Start()
    {
        print(SPAWN_POINTS[0]);
        SpawnObstacles();
    }
    private void SpawnObstacles() {
        int randNum = Random.Range(0, 5); //Number of possible obstacles

        for (int i = 0; i < randNum; i++)
        {
            int randSpawn = Random.Range(0, SPAWN_POINTS.Length);
            if (!usedSpawnPoints[randSpawn])
            {
                int randObs = Random.Range(0, LevelManager.OBSTACLE_PREFABS.Length);
                GameObject temp = Instantiate(LevelManager.OBSTACLE_PREFABS[randObs], transform);
                temp.transform.localPosition = SPAWN_POINTS[randSpawn];
                usedSpawnPoints[randSpawn] = true;
            }
            else i -= 1;
        }
    }
}
