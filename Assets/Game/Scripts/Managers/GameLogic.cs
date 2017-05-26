using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : ChrisCustomBehaviour {

    #region ENUMS
    public enum GameMode
    {
        RACE,
        INFINITE
    }
    #endregion
    #region Static Variables
    static GameMode _gameMode = GameMode.INFINITE;
    public static GameMode gameMode { get { return _gameMode; } }
    public static int MOVE_BACKWARDS_DISTANCE = 5;

    #endregion
    #region Public Variables
    public Transform playerParent;
    #endregion
    #region Private Variables
    private LevelManager ref_LevelManager;

    private GameObject local_Player;
    private GameObject spawnTileThreshold;
    #endregion


    #region Public Functions
    public override void Init() {
        base.Init();
        Random.InitState((int)System.DateTime.Now.Ticks);

        ref_LevelManager = FindObjectOfType<LevelManager>();
        ref_LevelManager.Init();

        local_Player = Instantiate(Resources.Load("CharacterPrefabs/Chicken_prefab"), Vector3.zero, Quaternion.identity, playerParent) as GameObject;
        Main.AssignCameraTargets(local_Player);

        spawnTileThreshold = Instantiate(Resources.Load("HiddenPrefabs/TileSpawnThreshold"), ref_LevelManager.parent) as GameObject;
        spawnTileThreshold.transform.position = new Vector3(0,0, local_Player.transform.position.z + 1.1f);
    }
    #endregion
    #region Private Functions
    //private void Start()
    //{
    //
    //    //player = GameObject.FindGameObjectWithTag("Player");
    //    //LM = GetComponent<LevelManager>();
    //    //spawnTileThreshold = GameObject.FindGameObjectWithTag("Threshold");
    //
    //    /* For Infinite Runner version
    //     * 
    //    for(int i = 0; i < initializeMap; i++)
    //    {
    //        LM.SpawnTile();
    //    } */
    //}
    //
    //private void Update()
    //{   //For Infinite Runner version
    //    //spawnTileThreshold.transform.position = new Vector3(player.transform.position.x, 0, spawnTileThreshold.transform.position.z);
    //}
    #endregion

}
