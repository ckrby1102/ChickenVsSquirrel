using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    protected static Vector3[] SPAWN_POINTS;
    protected Dictionary<int, bool> usedSpawnPoints;

    float startVal = -0.0418f;
    float newVal = 0;

    private void Awake()
    {
        InitializeSpawnPoints();
        usedSpawnPoints = new Dictionary<int, bool>();
        for (int i = 0; i < SPAWN_POINTS.Length; i++)
        {
            usedSpawnPoints[i] = false;
        }
    }

    private void InitializeSpawnPoints() {
        if (SPAWN_POINTS == null)
        {
            SPAWN_POINTS = new Vector3[60];
            SPAWN_POINTS[0] = new Vector3(startVal, 0, -2.5f);
            for (int i = 1; i < SPAWN_POINTS.Length; i++)
            {
                newVal += i == 1 ? startVal * 3 : startVal * 2;
                SPAWN_POINTS[i] = new Vector3(newVal, 0, -2.5f);
            }
        }
    }
}
