using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    public float MaxSpeed,
                 MoveSpeed;
    protected Rigidbody2D myRigidbody;
    protected GameManager myGameManager;

    protected bool facingRight
    {
        get
        {
            return _facingRight;
        }
        set
        {
            if (value != _facingRight)
            {
                this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                _facingRight = value;
            }
        }
    }
    private bool _facingRight = true;

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myGameManager = FindObjectOfType<GameManager>();
    }
    
    protected virtual void Update()
    {
        
    }

    #region MoveMethods
    protected void Move(Vector2 velocity)
    {
        myRigidbody.AddForce(velocity);
    }

    protected virtual void MoveUp(float intensity)
    {
        Move(new Vector2(0, intensity));
    }
    protected virtual void MoveDown(float intensity)
    {
        Move(new Vector2(0, -intensity));
    }
    protected virtual void MoveLeft(float intensity)
    {
        facingRight = false;
        if (myRigidbody.velocity.x > -MaxSpeed)
            Move(new Vector2(-intensity, 0));
    }
    protected virtual void MoveRight(float intensity)
    {
        facingRight = true;
        if (myRigidbody.velocity.x < MaxSpeed)
            Move(new Vector2(intensity, 0));
    }
    #endregion
}