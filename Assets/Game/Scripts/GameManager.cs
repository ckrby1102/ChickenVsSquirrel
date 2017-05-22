using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int MOVE_BACKWARDS_DISTANCE = 6;

    private LevelManager LM;
    private GameObject player;
    private GameObject spawnTileThreshold;

    private int initializeMap = 25;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        LM = GetComponent<LevelManager>();
        spawnTileThreshold = GameObject.FindGameObjectWithTag("Threshold");

        /* For Infinite Runner version
         * 
        for(int i = 0; i < initializeMap; i++)
        {
            LM.SpawnTile();
        } */
    }

    private void Update()
    {   //For Infinite Runner version
        //spawnTileThreshold.transform.position = new Vector3(player.transform.position.x, 0, spawnTileThreshold.transform.position.z);
    }
}
