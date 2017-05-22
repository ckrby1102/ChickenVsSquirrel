using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int initializeMap = 1;

    private LevelManager LM;
    private GameObject player;
    private GameObject spawnTileThreshold;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LM = GetComponent<LevelManager>();
        spawnTileThreshold = GameObject.FindGameObjectWithTag("Threshold");

        for(int i = 0; i < initializeMap; i++)
        {
            LM.SpawnTile();
        }
    }

    private void Update()
    {
        spawnTileThreshold.transform.position = new Vector3(player.transform.position.x, 0, spawnTileThreshold.transform.position.z);
    }
}
