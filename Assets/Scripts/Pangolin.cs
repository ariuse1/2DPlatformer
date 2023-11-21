using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pangolin : Monster
{
    private void Awake()
    {        
        flipX = -1;
    }

    private void FixedUpdate()
    {
        isGrounded = animationObject.CheckGround(transform);
    }

    private void Update()
    {
        this.Move();
    }
}

