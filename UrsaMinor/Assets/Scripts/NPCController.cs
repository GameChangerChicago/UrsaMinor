using UnityEngine;
using System.Collections;

public class NPCController : BearController
{
    protected NPCStates currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            if (value != _currentState)
            {
                _lastState = _currentState;

                switch (value)
                {
                    case NPCStates.IDLE:
                        break;
                    case NPCStates.PATROL:
                        break;
                    case NPCStates.CALL:
                        Invoke("EndCallState", _currentDuration);
                        break;
                    case NPCStates.SWIPE:
                        Invoke("Swipe", 0.35f);
                        break;
                    case NPCStates.RUNAWAY:
                        break;
                }
                _currentState = value;
            }
        }
    }
    private NPCStates _currentState = NPCStates.PATROL;
    private NPCStates _lastState = NPCStates.IDLE;

    public GameObject EdgeChecker;
    public GameObject ParentReaction;
    public bool IsParent,
                IsFriend;
    private UrsaController _ursa;
    private float _currentDuration;
    private bool _movingRight = true,
                 _minorJumping;

    protected override void Start()
    {
        base.Start();

        _ursa = FindObjectOfType<UrsaController>();

        if (IsParent || IsFriend)
            currentState = NPCStates.IDLE;

        if (IsFriend)
        {
            facingRight = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        NPCBehavior();
    }

    private void NPCBehavior()
    {
        switch (currentState)
        {
            case NPCStates.IDLE:

                break;
            case NPCStates.PATROL:
                if (_movingRight)
                {
                    MoveRight(MoveSpeed);
                }
                else
                {
                    MoveLeft(MoveSpeed);
                }

                Collider2D obstacle = Physics2D.OverlapCircle(new Vector2(EdgeChecker.transform.position.x, EdgeChecker.transform.position.y), 0.1f);
                if (obstacle)
                {
                    if (obstacle.tag == "Ground")
                    {
                        _movingRight = !_movingRight;
                    }
                }
                break;
            case NPCStates.CALL:

                break;
            case NPCStates.RUNAWAY:
                MoveRight(MoveSpeed);
                Invoke("Jump", 0.5f);
                Invoke("EndGame", 1f);
                break;
            case NPCStates.SWIPE:
                break;
            default:
                Debug.LogWarning("That stated doesn't exist in this context.");
                break;
        }
    }

    public override void Call(float duration, TalkBubbleTypes currentType)
    {
        base.Call(duration, currentType);

        if (currentType == TalkBubbleTypes.ANGRY)
            myAnimator.SetBool("angryCalling", true);
        else
            myAnimator.SetBool("calling", true);

        if (IsFriend && currentType == TalkBubbleTypes.SURPRPISE)
        {
            _ursa.InputActive = false;
        }

        _currentDuration = duration;
        currentState = NPCStates.CALL;
    }

    public void NPCSwipeFinsih()
    {
        if (IsParent)
        {
            currentState = NPCStates.IDLE;
        }
        else
        {
            currentState = NPCStates.PATROL;
        }
    }

    private void EndCallState()
    {
        myAnimator.SetBool("calling", false);
        myAnimator.SetBool("angryCalling", false);

        if (IsParent)
        {
            currentState = NPCStates.IDLE;
        }
        else if (IsFriend)
        {
            currentState = NPCStates.RUNAWAY;
        }
        else
        {
            currentState = NPCStates.PATROL;
        }
    }

    //Start here tomorrow
    private void Jump()
    {
        jumpInitiated = true;
    }

    public void EnemySpotted()
    {
        currentState = NPCStates.SWIPE;
    }

    public void DisableParentRaction()
    {
        ParentReaction.GetComponent<Collider2D>().enabled = false;
    }

    private void EndGame()
    {
        //Load credits
    }
}