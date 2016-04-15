﻿using UnityEngine;
using System.Collections;

public class UrsaController : BearController
{
    private CameraController myCameraController;

    protected override void Start()
    {
        base.Start();

        myCameraController = Camera.main.GetComponent<CameraController>();
    }

    protected override void Update()
    {
        base.Update();

        myCameraController.Follow(this.gameObject);

        if(Input.GetKey(KeyCode.J) && !jumped)
        {
            isGrounded = false;
            Jump();
        }
        if(Input.GetKeyUp(KeyCode.J))
        {
            jumped = false;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            Call(3, TalkBubbleTypes.ANGRY);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft(MoveSpeed);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight(MoveSpeed);
        }
    }
}