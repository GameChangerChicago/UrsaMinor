using UnityEngine;
using System.Collections;

public class UrsaController : BearController
{
    public NPCController[] Parents;
    public bool InputActive = true;
    private CameraController myCameraController;
    private float _clickTime;
    //_jumpFinished = true;
    private bool _firstJump = true;

    protected override void Start()
    {
        base.Start();

        myCameraController = Camera.main.GetComponent<CameraController>();
    }

    protected override void Update()
    {
        base.Update();

        if (InputActive)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !jumped)
            {
                MakeNoise(false);
                jumpInitiated = true;
                _clickTime = 0;
                if (_firstJump)
                {
                    Parents[0].Call(0.5f, TalkBubbleTypes.ANGRY);
                    Parents[0].DisableParentRaction();
                    Parents[1].Call(0.5f, TalkBubbleTypes.ANGRY);
                    Parents[1].DisableParentRaction();
                    _firstJump = false;
                }
            }
            if (Input.GetKey(KeyCode.Mouse0) && !jumped && jumpInitiated)
            {
                _clickTime += Time.deltaTime;
                isGrounded = false;
                Jump();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (!isGrounded && _clickTime >= 0.5f)
                    jumped = true;
                else if (!isGrounded)
                    Invoke("DelayedJumpEnd", 0.5f - _clickTime);
                else
                    jumped = false;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Swipe();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                myAnimator.SetBool("calling", true);
                Call(0.2f, TalkBubbleTypes.LEFTARROW);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft(MoveSpeed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight(MoveSpeed);
            }
        }
    }

    void DelayedJumpEnd()
    {
        jumped = true;
    }
}