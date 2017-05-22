using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileThreshold : MonoBehaviour {

    

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z + 1);
            EventManager.OnCollideThreshold();
        }
    }
}
