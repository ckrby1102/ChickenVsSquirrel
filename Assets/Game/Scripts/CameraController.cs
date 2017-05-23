using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public float camSpeed;

    private Vector3 offset;
    private float clampZ;
    private void Start()
    {
        offset = transform.position - player.transform.position;
        clampZ = transform.position.z;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,player.transform.position + offset, camSpeed * Time.deltaTime);
        if (transform.position.z < clampZ) transform.position = new Vector3(transform.position.x, transform.position.y, clampZ);
        else clampZ = transform.position.z;
    }
}
