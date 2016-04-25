using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OwlbertController : MovementController
{
    public List<TravelNode> TravelNodes;

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
    private bool _yMovementCompete;

    protected override void Start()
    {
        base.Start();

        _currentTarget = TravelNodes[0].transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Y))
        {
            FindObjectOfType<CameraController>().ChangeFocus(this.gameObject);
        }

        MoveToNode();
        myAnimator.SetFloat("yVelocity", myRigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SetNextTarget();
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
            myAnimator.SetBool("dead", true);
        }
    }

    public void Death()
    {
        Destroy(this.gameObject);
    }
}