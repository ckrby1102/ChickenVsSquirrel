using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileThreshold : MonoBehaviour {
    public static int threshold_move = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print(threshold_move);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z + threshold_move);
            EventManager.CallThresholdCollide(0);
        }
    }
}
