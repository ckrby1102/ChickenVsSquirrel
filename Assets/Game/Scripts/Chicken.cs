using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : PlayerCharacter{

    protected override void Start() {
        base.Start();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }
}
