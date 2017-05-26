using UnityEngine;
using System.Collections;

public class EventManager {

    //Call Event to spawn new tiles
    public delegate void OnThresholdCollide(int recur);
    public static event OnThresholdCollide THRESHOLD_COLLIDE;
    public static void CallThresholdCollide(int recur)
    {
        if (THRESHOLD_COLLIDE != null)
        {
            THRESHOLD_COLLIDE(recur);
        }
    }
    //Call Event to disable/enable tiles
    public delegate void OnPlayerMoveZ(float pos);
    public static event OnPlayerMoveZ PLAYER_MOVE_Z;
    public static void CallPlayerMoveZ(float pos)
    {
        if (PLAYER_MOVE_Z != null)
        {
            PLAYER_MOVE_Z(pos);
        }
    }
}
