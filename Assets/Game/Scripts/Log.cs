using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {

    public Transform[] snapPoints;
    public float speed = 0;

    float delay = 0;
    Vector3 startPoint;
    Vector3 endPoint;

    private void Start()
    {

    }

    private void Update()
    {
        if (delay < 0)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (Mathf.Abs(transform.position.x - endPoint.x) < 1)
            {
                transform.position = new Vector3(startPoint.x, transform.position.y, transform.position.z);
            }
        }
        else
        {
            delay -= Time.deltaTime;
        }
    }

    public void Initialize(float s, float d, Vector3 start, Vector3 end)
    {
        speed = s;
        delay = d;
        startPoint = start;
        endPoint = end;
    }
}
