using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    public float hopSpaces = 1.0f;
    public float speed = 1.0f;
    private float backwardMax = -1.0f, forwardMax = Mathf.Infinity, rightMax = 20.0f, leftMax = -16.0f;
    public Quaternion myRotation;

    protected Animator anim;
    Rigidbody playerRigidBody;

    private Vector3 endpos;
    private bool moving = false;
    protected virtual void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        endpos = transform.position;
        myRotation = transform.rotation;
    }

    void Update()
    {

        if (moving && (transform.position == endpos)) moving = false;

        if (!moving && Input.GetKeyUp(KeyCode.W))
        {
            if (transform.position.z + (1 * hopSpaces) < forwardMax)
            {
                Move(Vector3.forward, 0);
                if (transform.position.z > GameManager.MOVE_BACKWARDS_DISTANCE) backwardMax = transform.position.z - GameManager.MOVE_BACKWARDS_DISTANCE;
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

        transform.position = Vector3.MoveTowards(transform.position, endpos, Time.deltaTime * speed);

        Animating(moving);


        if (Input.GetKeyUp(KeyCode.F)) anim.SetTrigger("Die");
    }

    private void Move(Vector3 target, float angle) {
        moving = true;
        endpos = transform.position + (target * hopSpaces);

        myRotation = new Quaternion(myRotation.x, angle, myRotation.z, myRotation.w);
        transform.rotation = myRotation;
    }

    private void Animating(bool t)
    {
        anim.SetBool("IsMoving", t);
    }

    
}
