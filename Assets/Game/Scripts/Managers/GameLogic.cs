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
    static GameMode _gameMode = GameMode.RACE;
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

        ref_LevelManager = FindObjectOfType<LevelManager>();
        ref_LevelManager.Init();

        local_Player = Instantiate(Resources.Load("CharacterPrefabs/Chicken_prefab"), Vector3.zero, Quaternion.identity, playerParent) as GameObject;
        Main.AssignCameraTargets(local_Player);

        if (gameMode == GameMode.INFINITE) InfiniteMode();
    }
    #endregion
    #region Private Functions
    private void RaceMode()
    {

    }
    private void InfiniteMode()
    {
        spawnTileThreshold = Instantiate(Resources.Load("HiddenPrefabs/TileSpawnThreshold"), ref_LevelManager.parent) as GameObject;
        spawnTileThreshold.transform.position = new Vector3(0, 0, local_Player.transform.position.z + 1.1f);
    }
    #endregion

}
