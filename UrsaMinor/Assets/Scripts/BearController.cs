using UnityEngine;
using System.Collections;

public class BearController : MovementController
{
    public GameObject TalkBubblePoint;
    public float FullJumpSpeed,
                 JumpVelocity;
    public bool SmallBear;

    protected bool jumped
    {
        get
        {
            return _jumped;
        }
        set
        {
            if (value != _jumped)
            {
                if (value)
                {
                    jumpInitiated = false;
                    myRigidbody.gravityScale = 5;
                }

                myAnimator.SetBool("jumped", value);
                _jumped = value;
            }
        }
    }
    private bool _jumped;

    protected bool jumpInitiated
    {
        get
        {
            return _jumpInitiated;
        }
        set
        {
            if(_jumpInitiated != value)
            {
                if (value)
                {
                    myRigidbody.gravityScale = 0;
                }

                myAnimator.SetBool("jumpStart", value);
                _jumpInitiated = value;
            }
        }
    }
    private bool _jumpInitiated;

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
                if (value)
                {
                    jumped = false;
                }

                myAnimator.SetBool("landed", value);
                _isGrounded = value;
            }
        }
    }
    private bool _isGrounded;

    protected int happyCallIndex,
                  angryCallIndex,
                  swipeIndex;

    private bool _swatting;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Y))
        {
            Call(1, TalkBubbleTypes.ANGRY);
        }

        myAnimator.SetFloat("xVelocity", Mathf.Abs(myRigidbody.velocity.x));
    }

    protected virtual void Jump()
    {
        if (myRigidbody.velocity.y < FullJumpSpeed)
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

    public virtual void Call(float duration, TalkBubbleTypes currentType)
    {
        GameObject talkBubbleObject = new GameObject();
        switch (currentType)
        {
            case TalkBubbleTypes.ANGRY:
                MakeNoise(true);

                talkBubbleObject = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/AngryTalkBubble"), TalkBubblePoint.transform.position, Quaternion.identity);
                break;
            case TalkBubbleTypes.LEFTARROW:
                MakeNoise(false);

                talkBubbleObject = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/LeftArrowTalkBubble"), TalkBubblePoint.transform.position, Quaternion.identity);
                break;
            case TalkBubbleTypes.RIGHTARROW:
                MakeNoise(false);

                talkBubbleObject = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/RightArrowTalkBubble"), TalkBubblePoint.transform.position, Quaternion.identity);
                break;
            default:
                Debug.LogWarning("That talk bubble type either doesn't exist or hasn't been set up yet.");
                break;
        }
        talkBubbleObject.transform.localScale = new Vector3(this.transform.localScale.x, 1, 1);
        TalkBubble talkBubble = talkBubbleObject.GetComponent<TalkBubble>();
        talkBubble.Duration = duration;
    }

    protected void MakeNoise(bool angry)
    {
        int angryIndex = Random.Range(0, 2);
        int happyIndex = Random.Range(0, 3);

        if(angry)
        {
            if (angryIndex == 0)
                myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.BearAngry1);
            else if (angryIndex == 1)
                myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.BearAngry2);
            else if (angryIndex == 2)
                myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.BearAngry3);
            else
                Debug.LogWarning("This index should be any lower than 0 or higher than 2.");
        }
        else
        {
            if (SmallBear)
            {
                if (happyIndex == 0)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.SmallBearHappy1);
                else if (happyIndex == 1)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.SmallBearHappy2);
                else if (happyIndex == 2)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.SmallBearHappy3);
                else if (happyIndex == 3)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.SmallBearHappy4);
                else
                    Debug.LogWarning("This index should be any lower than 0 or higher than 2.");
            }
            else
            {
                if (happyIndex == 0)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.AdultBearHappy1);
                else if (happyIndex == 1)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.AdultBearHappy2);
                else if (happyIndex == 2)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.AdultBearHappy3);
                else if (happyIndex == 3)
                    myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.AdultBearHappy4);
                else
                    Debug.LogWarning("This index should be any lower than 0 or higher than 2.");
            }
        }
    }

    protected void Swipe()
    {
        myAnimator.SetBool("swatting", true);
        _swatting = true;
    }

    public void FinishSwipe()
    {
        myAnimator.SetBool("swatting", false);
        _swatting = false;
    }

    protected override void Move(Vector2 velocity)
    {
        if (!_swatting)
            base.Move(velocity);
    }

    public void FinishCall()
    {
        myAnimator.SetBool("calling", false);
    }
}