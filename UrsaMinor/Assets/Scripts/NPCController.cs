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
                
                switch(value)
                {
                    case NPCStates.IDLE:
                        break;
                    case NPCStates.PATROL:
                        break;
                    case NPCStates.CALL:
                        Invoke("EndCallState", _currentDuration);
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
    public bool IsParent;
    private float _currentDuration;
    private bool _movingRight = true;

    protected override void Start()
    {
        base.Start();

        if (IsParent)
            currentState = NPCStates.IDLE;
    }

    protected override void Update()
    {
        base.Update();

        NPCBehavior();
        
        if(Input.GetKeyDown(KeyCode.O))
        {
            Call(0.5f, TalkBubbleTypes.ANGRY);
        }
    }

    private void NPCBehavior()
    {
        switch(currentState)
        {
            case NPCStates.IDLE:
                Debug.Log("Nothing");
                break;
            case NPCStates.PATROL:
                if(_movingRight)
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
                    if(obstacle.tag == "Ground")
                    {
                        _movingRight = !_movingRight;
                    }
                }
                break;
            case NPCStates.CALL:
                
                break;
            case NPCStates.RUNAWAY:
                Debug.Log("Retreat!");
                break;
            default:
                Debug.LogWarning("That stated doesn't exist in this context.");
                break;
        }
    }

    public override void Call(float duration, TalkBubbleTypes currentType)
    {
        base.Call(duration, currentType);
        myAnimator.SetBool("calling", true);
        _currentDuration = duration;
        currentState = NPCStates.CALL;
    }

    private void EndCallState()
    {
        myAnimator.SetBool("calling", false);
        currentState = _lastState;
    }
}