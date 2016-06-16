using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OwlbertController : MovementController
{
    public enum OwlbertStates
    {
        IDLE,
        FOLLOWROUTE,
        DEATH
    }

    public List<TravelNode> TravelNodes;
    public Transform StartPoint;

    private GameManager _theGameManager;
    private Vector2 _currentTarget;
    private int _currentNodeIndex;
    protected bool movingUp
    {
        get
        {
            return _movingUp;
        }
        set
        {
            if (value != _movingUp)
            {
                _movingUp = value;
            }
        }
    }
    private bool _movingUp;

    protected OwlbertStates currentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            if (value != _currentState)
            {
                switch(value)
                {
                    case OwlbertStates.IDLE:
                        myAnimator.SetBool("dead", false);
                        myAnimator.SetBool("moving", false);
                        break;
                    case OwlbertStates.FOLLOWROUTE:
                        myAnimator.SetBool("moving", true);
                        break;
                    case OwlbertStates.DEATH:
                        myAnimator.SetBool("dead", true);
                        break;
                    default:
                        Debug.LogWarning("That state doens't exist...");
                        break;
                }
                _currentState = value;
            }
        }
    }
    private OwlbertStates _currentState;

    private bool _yMovementCompete;

    protected override void Start()
    {
        base.Start();

        _theGameManager = FindObjectOfType<GameManager>();
        _currentTarget = TravelNodes[0].transform.position;
    }

    protected override void Update()
    {
        base.Update();

        OwlBehavior();

        if(Input.GetKeyDown(KeyCode.Y))
        {
            FindObjectOfType<CameraController>().ChangeFocus(this.gameObject);
        }

        myAnimator.SetFloat("yVelocity", myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SetNextTarget();
        }
    }

    private void OwlBehavior()
    {
        switch(currentState)
        {
            case OwlbertStates.IDLE:
                break;
            case OwlbertStates.FOLLOWROUTE:
                MoveToNode();
                break;
            case OwlbertStates.DEATH:
                myAnimator.SetBool("dead", true);
                break;
        }
    }

    private void SetNextTarget()
    {
        if (_currentNodeIndex < TravelNodes.Count)
            _currentTarget = TravelNodes[++_currentNodeIndex].transform.position;
    }

    private void MoveToNode()
    {
        if (Vector2.Distance(this.transform.position, _currentTarget) > 0.5f)
        {
            if (this.transform.position.y < _currentTarget.y - 0.4f)
            {
                _yMovementCompete = false;
                MoveUp(MoveSpeed);
                movingUp = true;
            }
            else if (this.transform.position.y > _currentTarget.y + 0.4f)
            {
                _yMovementCompete = false;
                MoveDown(MoveSpeed);
                movingUp = false;
            }
            else
            {
                _yMovementCompete = true;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y / 1.1f);
                movingUp = false;
            }

            if (!_yMovementCompete && this.transform.position.x > _currentTarget.x - 1f)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x / 1.1f, myRigidbody.velocity.y);
            }
            //else if()
            else
                MoveRight(MoveSpeed);
        }
        else if (_currentNodeIndex < TravelNodes.Count - 1)
        {
            SetNextTarget();
        }
        else
        {
            this.transform.position = new Vector2(TravelNodes[0].transform.position.x - 6.8f, TravelNodes[0].transform.position.y + 1.3f);
            _currentNodeIndex = 0;
            _currentTarget = TravelNodes[_currentNodeIndex].transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "SwipHitBox")
        {
            _theGameManager.PapaPointsText.rectTransform.position = Camera.main.WorldToScreenPoint(new Vector2(col.transform.position.x + 1, col.transform.position.y));
            _theGameManager.PapaPointsText.text = "+30";
            Invoke("ResetText", 1);
            myGameManager.TheAuidoManager.PlaySFX(AudioLoader.instance.OwlbertHit);
            currentState = OwlbertStates.DEATH;
        }
    }

    private void ResetText()
    {
        _theGameManager.PapaPointsText.text = ""; 
    }

    public void Restart()
    {
        currentState = OwlbertStates.IDLE;
        _currentNodeIndex = 0;
        _currentTarget = TravelNodes[_currentNodeIndex].transform.position;
        this.transform.position = StartPoint.position;
        Invoke("StartMoving", 3);
    }

    private void StartMoving()
    {
        currentState = OwlbertStates.FOLLOWROUTE;
    }

    public void Death()
    {
        currentState = OwlbertStates.IDLE;
        myRigidbody.velocity = Vector2.zero;
        this.transform.position = new Vector3(500, 500, 500);
        if (myGameManager.OwlbertLives > 0)
        {
            myGameManager.OwlbertLives--;
        }
        else
        {
            myGameManager.GameOver();
        }
    }
}