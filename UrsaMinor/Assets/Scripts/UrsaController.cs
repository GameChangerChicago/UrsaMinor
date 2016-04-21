using UnityEngine;
using System.Collections;

public class UrsaController : BearController
{
    private CameraController myCameraController;
    private bool _jumpInitiated;

    protected override void Start()
    {
        base.Start();

        myCameraController = Camera.main.GetComponent<CameraController>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !jumped && isGrounded)
        {
            MakeNoise(false);
            _jumpInitiated = true;
        }
        if (Input.GetKey(KeyCode.Mouse0) && !jumped && _jumpInitiated)
        {
            isGrounded = false;
            Jump();
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            jumped = false;
            _jumpInitiated = false;
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