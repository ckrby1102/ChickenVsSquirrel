using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Transform parent;
    public int spawnDistance = 1;
    public int min_fieldLength = 1, max_fieldLength = 5;
    public int min_roadLength = 1, max_roadLength = 5;
    public int min_waterLength = 1, max_waterLength = 5;
    public int levelLength = 100;

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
    private void Awake()
    {
        startingAreaPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/StartingArea");
        fieldPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/Field");
        roadPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/Road");
        waterPrefabs = Resources.LoadAll<GameObject>("TilePrefabs/Water");
        lastTile = new Vector3(0, 0, 0);

        EventManager.OnPlayerMoveZ_Event += CheckTiles;

        /* For Infinite Runner version
         * 
        EventManager.OnCollideThreshold_Event += SpawnTile; */

    }

    private void Start()
    {
        InititializeLevel();
    }
    /* For Infinite Runner version
     * 
    public void SpawnTile()
    {
        if (tilePrefabs.Length > 0)
        {
            int randTile = Random.Range(0, tilePrefabs.Length);
            GameObject temp = Instantiate(tilePrefabs[randTile], parent) as GameObject;
            temp.transform.position = new Vector3(temp.transform.position.x, 0, lastTile.z + 1);
            lastTile = temp.transform.position;
            tiles.Add(temp);
        }
    } */

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
                                int randTile = Random.Range(0, fieldPrefabs.Length);
                                GameObject temp = Instantiate(fieldPrefabs[randTile], parent) as GameObject;
                                temp.transform.position = new Vector3(temp.transform.position.x, 0, lastTile.z + 1);
                                lastTile = temp.transform.position;
                                tiles.Add(temp);
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
                                int randTile = Random.Range(0, roadPrefabs.Length);
                                GameObject temp = Instantiate(roadPrefabs[randTile], parent) as GameObject;
                                temp.transform.position = new Vector3(temp.transform.position.x, 0, lastTile.z + 1);
                                lastTile = temp.transform.position;
                                tiles.Add(temp);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        if (waterPrefabs.Length > 0)
                        {
                            int randLength = Random.Range(min_waterLength, max_waterLength);
                            for (int i = 0; i < randLength; i++)
                            {
                                int randTile = Random.Range(0, waterPrefabs.Length);
                                GameObject temp = Instantiate(waterPrefabs[randTile], parent) as GameObject;
                                temp.transform.position = new Vector3(temp.transform.position.x, 0, lastTile.z + 1);
                                lastTile = temp.transform.position;
                                tiles.Add(temp);
                            }
                        }
                    }
                    break;
            }
        }
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
