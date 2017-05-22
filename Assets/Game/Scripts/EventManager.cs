using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void SpawnTile();
    public static event SpawnTile OnCollideThreshold_Event;

    public delegate void CheckTiles(float pos);
    public static event CheckTiles OnPlayerMoveZ_Event;

    public static void OnCollideThreshold() {
        OnCollideThreshold_Event();
    }

    public static void OnPlayerMoveZ(float pos) {
        OnPlayerMoveZ_Event(pos);
    }
}
