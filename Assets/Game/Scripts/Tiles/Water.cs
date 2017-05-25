using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Tile {

    public int waterFlow = 0;
    int flowSpeed;
    Vector3 startPos;
    Vector3 endPos;

    protected override void Start()
    {
        base.Start();
    }

    protected override void SpawnObstacles()
    {
        base.SpawnObstacles();

        flowSpeed = Random.Range(2, 5);
        flowSpeed *= waterFlow;

        float time = 60 / Mathf.Abs(flowSpeed);
        int delay = Random.Range(3, 6);
        int numOfLogs = (int)time / delay;

        startPos = new Vector3(-29 * waterFlow, 0, 0);
        endPos = new Vector3(29 * waterFlow, 0, 0);
        for (int i = 0; i < numOfLogs; i++)
        {
            int randlog = Random.Range(0, LevelManager.WATER_OBSTACLE_PREFABS.Length);
            GameObject temp = Instantiate(LevelManager.WATER_OBSTACLE_PREFABS[randlog], transform.parent);
            temp.transform.position = new Vector3(-29 * waterFlow, temp.transform.position.y, temp.transform.position.z);
            temp.GetComponent<Log>().Initialize(flowSpeed, delay * i, startPos, endPos);
        }


    }
}
