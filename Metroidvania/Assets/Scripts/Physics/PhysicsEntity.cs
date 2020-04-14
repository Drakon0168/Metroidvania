using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PhysicsEntity : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 acceleration;

    #region Properties

    /// <summary>
    /// The current position of the object
    /// </summary>
    public Vector2 Position
    {
        get { return new Vector2(transform.position.x, transform.position.y); }
        set { transform.position = new Vector3(value.x, value.y, transform.position.z); }
    }

    /// <summary>
    /// The velocity of the entity
    /// </summary>
    public Vector2 Velocity
    {
        get { return rigidbody.velocity; }
        set { rigidbody.velocity = value; }
    }

    /// <summary>
    /// The acceleration of the entity
    /// </summary>
    public Vector2 Acceleration
    {
        get { return acceleration; }
        set { acceleration = value; }
    }

    #endregion

    #region Unity Functions

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Velocity += Acceleration * Time.deltaTime;
        Acceleration = Vector2.zero;
    }

    #endregion

    #region PhysicsEntity

    protected void ApplyForce(Vector2 force)
    {
        Acceleration += force;
    }

    #endregion
}