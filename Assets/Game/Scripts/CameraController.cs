using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public float camSpeed;

    private Vector3 offset;
    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,player.transform.position + offset, camSpeed * Time.deltaTime);
    }
}
