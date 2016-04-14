using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
    public float MaxSpeed,
                 MoveSpeed;
    protected Rigidbody2D _myRigidbody;

    protected virtual void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }
    
    protected virtual void Update()
    {
        
    }

    #region MoveMethods
    protected void Move(Vector2 velocity)
    {
        _myRigidbody.AddForce(velocity);
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
        if (_myRigidbody.velocity.x > -MaxSpeed)
            Move(new Vector2(-intensity, 0));
    }
    protected virtual void MoveRight(float intensity)
    {
        if (_myRigidbody.velocity.x < MaxSpeed)
            Move(new Vector2(intensity, 0));
    }
    #endregion
}