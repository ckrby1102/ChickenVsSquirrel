using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : ChrisCustomBehaviour {

    #region ENUMS
    #endregion
    #region Static Variables
    #endregion
    #region Public Variables
    #endregion
    #region Private Variables
    private int initialMapSizeForInfiniteRunner = 15;
    #endregion

    public static int MAX_NUM_OF_OBSTACLES = 15;

    public Transform parent;
    public int spawnDistance = 1;
    public int min_fieldLength = 1, max_fieldLength = 5;
    public int min_roadLength = 1, max_roadLength = 5;
    public int min_waterLength = 1, max_waterLength = 5;
    public int levelLength = 100;
    public int m_max_num_of_obstacles = 15;

    public static GameObject[] FIELD_OBSTACLE_PREFABS;
    public static GameObject[] WATER_OBSTACLE_PREFABS;
    [SerializeField]
    private GameObject[] startingAreaPrefabs;
    [SerializeField]
    private GameObject[] fieldPrefabs;
    [SerializeField]
    private GameObject[] roadPrefabs;
    [SerializeField]
    private GameObject[] waterPrefabs;
    [SerializeField]
    private List<GameObject> tiles;
    private int randEnv;
    private int lastEnv;

    private Vector3 lastTile;

    public override void Init()
    {
        base.Init();
        startingAreaPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/StartingArea");
        fieldPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/Field");
        roadPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/Road");
        waterPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/Water");
        FIELD_OBSTACLE_PREFABS = Resources.LoadAll<GameObject>("ObstaclePrefabs/Field");
        WATER_OBSTACLE_PREFABS = Resources.LoadAll<GameObject>("ObstaclePrefabs/Water");
        lastTile = new Vector3(0, 0, 0);

        EventManager.PLAYER_MOVE_Z += CheckTiles;

        MAX_NUM_OF_OBSTACLES = m_max_num_of_obstacles;

        InititializeLevel();
    }

    public void InititializeLevel()
    {
        if (startingAreaPrefabs.Length > 0)
        {
            int randStart = Random.Range(0, startingAreaPrefabs.Length);
            GameObject temp = Instantiate(startingAreaPrefabs[randStart], parent) as GameObject;
            temp.transform.position = new Vector3(temp.transform.position.x, 0, 0);
            lastTile = new Vector3(temp.transform.position.x, 0, 2);
            tiles.Add(temp);
        }

        if (GameLogic.gameMode == GameLogic.GameMode.RACE)
        {
            RaceMode();
        }
        else if(GameLogic.gameMode == GameLogic.GameMode.INFINITE)
        {
            EventManager.THRESHOLD_COLLIDE += InfiniteMode;
            InfiniteMode(initialMapSizeForInfiniteRunner);
        }


        
    }

    private void RaceMode()
    {
        //Possibly Handle Networked SEED for map generation based on HOST

        for (int l = 0; l < levelLength; l++)
        {
            if (lastEnv == 0)
            {
                randEnv = Random.Range(0, 3);
                lastEnv = randEnv;
            }
            else randEnv = lastEnv = 0;

            switch (randEnv)
            {
                case 0:
                    {
                        if (fieldPrefabs.Length > 0)
                        {
                            int randLength = Random.Range(min_fieldLength, max_fieldLength);
                            for (int i = 0; i < randLength; i++)
                            {
                                SpawnTile(fieldPrefabs);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        if (roadPrefabs.Length > 0)
                        {
                            int randLength = Random.Range(min_roadLength, max_roadLength);
                            for (int i = 0; i < randLength; i++)
                            {
                                SpawnTile(roadPrefabs);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        if (waterPrefabs.Length > 0)
                        {
                            int randDir = Random.Range(-2, 2);
                            randDir = randDir < 0 ? -1 : 1;

                            int randLength = Random.Range(min_waterLength, max_waterLength);
                            for (int i = 0; i < randLength; i++)
                            {
                                SpawnTile(waterPrefabs).transform.GetChild(0).GetComponent<Water>().waterFlow = randDir;
                                randDir *= -1;
                            }
                        }
                    }
                    break;
            }
        }
    }
    private void InfiniteMode(int recur)
    {
        if (lastEnv == 0)
        {
            randEnv = Random.Range(0, 3);
            lastEnv = randEnv;
        }
        else randEnv = lastEnv = 0;

        switch (randEnv)
        {
            case 0:
                {
                    if (fieldPrefabs.Length > 0)
                    {
                        int randLength = Random.Range(min_fieldLength, max_fieldLength);
                        TileThreshold.threshold_move = randLength;
                        for (int i = 0; i < randLength; i++)
                        {
                            SpawnTile(fieldPrefabs);
                        }
                    }
                }
                break;
            case 1:
                {
                    if (roadPrefabs.Length > 0)
                    {
                        int randLength = Random.Range(min_roadLength, max_roadLength);
                        TileThreshold.threshold_move = randLength;
                        for (int i = 0; i < randLength; i++)
                        {
                            SpawnTile(roadPrefabs);
                        }
                    }
                }
                break;
            case 2:
                {
                    if (waterPrefabs.Length > 0)
                    {
                        int randDir = Random.Range(-2, 2);
                        randDir = randDir < 0 ? -1 : 1;

                        int randLength = Random.Range(min_waterLength, max_waterLength);
                        TileThreshold.threshold_move = randLength;
                        for (int i = 0; i < randLength; i++)
                        {
                            SpawnTile(waterPrefabs).transform.GetChild(0).GetComponent<Water>().waterFlow = randDir;
                            randDir *= -1;
                        }
                    }
                }
                break;
        }

        if (recur > 0) InfiniteMode(recur - 1);
    }

    private GameObject SpawnTile(GameObject[] prefabs)
    {
        int randTile = Random.Range(0, prefabs.Length);
        GameObject temp = Instantiate(prefabs[randTile], parent) as GameObject;
        temp.transform.position = new Vector3(temp.transform.position.x, 0, lastTile.z + 1);
        lastTile = temp.transform.position;
        tiles.Add(temp);
        return tiles[tiles.Count - 1];
        
    }

    public void CheckTiles(float playerPosZ)
    {
        for (int i = 0; i < tiles.Count; i++) {
            if (Mathf.Abs(tiles[i].transform.position.z - playerPosZ) > 30 && tiles[i].activeInHierarchy) tiles[i].SetActive(false);
            else if (Mathf.Abs(tiles[i].transform.position.z - playerPosZ) < 30 && !tiles[i].activeInHierarchy) tiles[i].SetActive(true);

            if (tiles[i].transform.position.z - playerPosZ < 6 * -2 - 1) RemoveAndDestroy(tiles[i]);
        }
    }

    public void RemoveAndDestroy(GameObject tile)
    {
        tiles.Remove(tile);
        Destroy(tile);
    }
}
