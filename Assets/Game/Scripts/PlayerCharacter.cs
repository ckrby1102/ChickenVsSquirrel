using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    public float hopSpaces = 1.0f;
    public float speed = 1.0f;
    public float backwardMax = -1.0f, forwardMax = Mathf.Infinity, rightMax = 20.0f, leftMax = -16.0f;
    public Quaternion myRotation;

    protected Animator anim;
    protected bool canSwim = false;

    public Vector3 endpos;
    private bool moving = false;

    private bool isOnLog = false;
    private Log logScript;
    private Vector3 logOffset;
    private Transform currentLogSnapPoint;
    private Transform targetLogSnapPoint;


    protected virtual void Start()
    {
        endpos = transform.position;
        myRotation = transform.rotation;
    }

    void Update()
    {

        if (moving && (transform.position == endpos))
        {
            moving = false;
            CheckFloorType();

            if (isOnLog)
            {
                currentLogSnapPoint = targetLogSnapPoint;
            }
        }
        else
        {
            if (!moving && Input.GetKeyUp(KeyCode.W))
            {
                if (transform.position.z + (1 * hopSpaces) < forwardMax)
                {
                    Move(Vector3.forward, 0);
                    if (transform.position.z > GameManager.MOVE_BACKWARDS_DISTANCE)
                    {
                        if (backwardMax < transform.position.z - GameManager.MOVE_BACKWARDS_DISTANCE)
                            backwardMax = transform.position.z - GameManager.MOVE_BACKWARDS_DISTANCE;
                    }
                    EventManager.OnPlayerMoveZ(transform.position.z);
                }
            }
            else if (!moving && Input.GetKeyUp(KeyCode.S))
            {
                if (transform.position.z + (-1 * hopSpaces) > backwardMax)
                {
                    Move(-Vector3.forward, 360);
                    EventManager.OnPlayerMoveZ(transform.position.z);
                }
            }
            else if (!moving && Input.GetKeyUp(KeyCode.D))
            {
                if (transform.position.x + (1 * hopSpaces) < rightMax) Move(Vector3.right, 1);
            }
            else if (!moving && Input.GetKeyUp(KeyCode.A))
            {
                if (transform.position.x + (-1 * hopSpaces) > leftMax) Move(-Vector3.right, -1);
            }

        }
        if (isOnLog)
        {
            endpos = targetLogSnapPoint.position;
            transform.position = Vector3.MoveTowards(transform.position, endpos, Time.deltaTime * (speed + logScript.speed));
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, endpos, Time.deltaTime * speed);
        }
        Animating(moving);


        if (Input.GetKeyUp(KeyCode.F)) anim.SetTrigger("Die");
    }


    private void Move(Vector3 target, float angle) {
        if (!CheckObstacleCollision(target))
        {
            if(target.x == 0) isOnLog = false;
            moving = true;
            int snapX = (int)transform.position.x;
            if (isOnLog)
            {
                Vector3 newPos = currentLogSnapPoint.position + (target * hopSpaces);
                for (int i = 0; i < logScript.snapPoints.Length; i++)
                {
                    if (newPos == logScript.snapPoints[i].position)
                    {
                        targetLogSnapPoint = logScript.snapPoints[i];
                    }
                }

                if (targetLogSnapPoint != currentLogSnapPoint) endpos = targetLogSnapPoint.position;
                else
                {
                    isOnLog = false;
                    Move(target, angle);
                }
            }
            else
            {
                endpos = new Vector3(snapX, transform.position.y, transform.position.z) + (target * hopSpaces);
            }

            myRotation = new Quaternion(myRotation.x, angle, myRotation.z, myRotation.w);
            transform.rotation = myRotation;
        }
    }
    private void Animating(bool t)
    {
        anim.SetBool("IsMoving", t);
    }



    bool CheckObstacleCollision(Vector3 target)
    {
        int snapX = (int)transform.position.x;
        RaycastHit hit;
        Vector3 origin = new Vector3(snapX, transform.position.y+ 0.5f, transform.position.z);
        if (Physics.Raycast(origin, target, out hit, 1))
        {
            if (hit.transform.tag == "StaticObstacle")
            {
                return true;
            }
        }
        return false;
    }

    void CheckFloorType()
    {
        RaycastHit hit;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        if (Physics.Raycast(origin, Vector3.down, out hit, 1))
        {
            switch (hit.transform.tag)
            {
                case "Water":
                    {
                        //Check if on standable object
                        //else drown unless chracter can swim
                        DeathByWater();
                    }
                    break;
                case "Road":
                    {
                        //if character is COW, slow down vehicles on road
                    }
                    break;
                case "Grass":
                    {
                        //do nothing
                    }
                    break;
                case "Log":
                    {
                        //force player to move with log
                        RideLog(hit.transform);
                    }
                    break;
            }
        }
    }

    private void RideLog(Transform log)
    {
        logScript = log.transform.GetComponent<Log>();

        float closest = Mathf.Infinity;
        for (int i = 0; i < logScript.snapPoints.Length; i++)
        {
            float dist = Vector3.Distance(logScript.snapPoints[i].position, transform.position);
            if (dist < closest)
            {
                closest = dist;
                currentLogSnapPoint = logScript.snapPoints[i];
                targetLogSnapPoint = currentLogSnapPoint;
            }
        }

        isOnLog = true;
    }
    protected virtual void DeathByWater()
    {
        print("Drowned?");
    }


    
}
