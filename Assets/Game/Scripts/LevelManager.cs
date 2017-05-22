using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Transform parent;
    public int spawnDistance = 1;
    public int min_tileLength = 1, max_tileLength = 5;

    [SerializeField]
    private GameObject[] tilePrefabs;
    [SerializeField]
    private List<GameObject> tiles;

    private Vector3 lastTile;
    private void Awake()
    {
        tilePrefabs = Resources.LoadAll<GameObject>("TilePrefabs");
        lastTile = new Vector3(0, 0, 0);

        EventManager.OnCollideThreshold_Event += SpawnTile;
        EventManager.OnPlayerMoveZ_Event += CheckTiles;
    }

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
    }

    public void CheckTiles(float playerPosZ)
    {
        for (int i = 0; i < tiles.Count; i++) {
            if (Mathf.Abs(tiles[i].transform.position.z - playerPosZ) > 26 && tiles[i].activeInHierarchy) tiles[i].SetActive(false);
            else if (Mathf.Abs(tiles[i].transform.position.z - playerPosZ) < 26 && !tiles[i].activeInHierarchy) tiles[i].SetActive(true);
        }
    }
}
