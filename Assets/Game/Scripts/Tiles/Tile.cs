using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public bool isStartingArea = false;
    public bool isPlayerSafeZone = false;

    protected static Vector3[] SPAWN_POINTS;
    protected Dictionary<int, bool> usedSpawnPoints;

    private void Awake()
    {
        InitializeSpawnPoints();
        usedSpawnPoints = new Dictionary<int, bool>();
        for (int i = 0; i < SPAWN_POINTS.Length; i++)
        {
            usedSpawnPoints[i] = false;
        }
    }

    protected virtual void Start()
    {
        CheckSpawnObstacles();
    }

    private void InitializeSpawnPoints() {
        if (SPAWN_POINTS == null)
        {   SPAWN_POINTS = new Vector3[60];

            float spacing = 1.0f / 60.0f;
            SPAWN_POINTS[0] = new Vector3(-0.5f, 0, 0);
            for (int i = 1; i < SPAWN_POINTS.Length; i++)
            {
                SPAWN_POINTS[i] = new Vector3(SPAWN_POINTS[i-1].x + spacing, 0, 0);
            }
        }
    }

    protected void CheckSpawnObstacles() {
        //if (isStartingArea)
        //{
        //    SpawnStartingObstacles();
        //}
        //else
        //{
        //    SpawnObstacles();
        //}
        SpawnObstacles();
    }

    protected virtual void SpawnStartingObstacles() {

    }
    protected virtual void SpawnObstacles() {

    }
}
