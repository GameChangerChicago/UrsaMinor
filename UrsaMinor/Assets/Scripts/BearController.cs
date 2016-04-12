using UnityEngine;
using System.Collections;

public class BearController : MovementController
{
    public float FullJumpSpeed,
                 JumpVelocity;
    protected bool jumped;

    protected bool isGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            if (value != _isGrounded)
            {
                _isGrounded = value;
            }
        }
    }
    private bool _isGrounded;

    private GameObject _genericTalkBubble;

    protected override void Start()
    {
        base.Start();

        _genericTalkBubble = Resources.Load<GameObject>("/Prefabs/GenericTalkBubble");
    }

    protected override void Update()
    {
        base.Update();
    }

    protected virtual void Jump()
    {
        if (_myRigidbody.velocity.y < FullJumpSpeed)
        {
            MoveUp(JumpVelocity);
        }
        else
            jumped = true;
    }

    //This needs to be made better
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    protected virtual void Call(float duration)
    {
        GameObject genericTalkBubble = Instantiate(_genericTalkBubble);
        TalkBubble talkBubble = genericTalkBubble.GetComponent<TalkBubble>();
        talkBubble.Duration = duration;
    }
}