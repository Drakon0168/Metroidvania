using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : PhysicsEntity
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float sprintSpeed = 7.5f;
    [SerializeField]
    private float moveForce = 100;
    [SerializeField]
    private float jumpSpeed = 7.5f;
    private bool sprinting = false;
    [SerializeField]
    private bool canFly = false;
    [Space]

    [Header("Stats")]
    [SerializeField]
    private float maxHealth;
    private float health;
    [SerializeField]
    private float maxStamina;
    private float stamina;

    #region Properties

    /// <summary>
    /// The current move speed of the entity
    /// </summary>
    public float MoveSpeed {
        get {
            if (sprinting)
            {
                return sprintSpeed;
            }
            return moveSpeed;
        }
    }

    /// <summary>
    /// The maximum health of the entity
    /// </summary>
    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            float percentHealth = health / maxHealth;

            maxHealth = value;
            health = percentHealth * maxHealth;
        }
    }

    /// <summary>
    /// The current health of the entity
    /// </summary>
    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if(health <= 0)
            {
                health = 0;
                Die();
            }

            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    /// <summary>
    /// The maximum stamina of the entity
    /// </summary>
    public float MaxStamina
    {
        get { return maxStamina; }
        set
        {
            float percentStamina = stamina / maxStamina;

            maxStamina = value;
            stamina = percentStamina * maxStamina;
        }
    }

    /// <summary>
    /// The current stamina of the entity
    /// </summary>
    public float Stamina
    {
        get { return stamina; }
        set
        {
            stamina = value;

            if (stamina <= 0)
            {
                stamina = 0;
                Die();
            }

            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
    }

    #endregion

    #region Unity Functions

    protected override void Awake()
    {
        base.Awake();
        Reset();
    }

    #endregion

    #region Living Entity

    /// <summary>
    /// Moves in the target direction
    /// </summary>
    /// <param name="moveDirection">The direction to move in</param>
    public void Move(Vector2 moveDirection)
    {
        //Find the target velocity
        Vector2 targetVelocity = Vector2.zero;

        if (canFly)
        {
            targetVelocity = moveDirection * moveSpeed;
        }
        else
        {
            if(moveDirection.x > 0)
            {
                targetVelocity = Vector2.right * moveSpeed;
            }
            else if(moveDirection.x < 0)
            {
                targetVelocity = Vector2.left * moveSpeed;
            }
        }

        //Find the force to apply
        Vector2 forceDirection = targetVelocity - Velocity;
        if (!canFly)
        {
            forceDirection.y = 0;
        }

        float forceLength = forceDirection.magnitude;

        if(forceLength != 0)
        {
            forceDirection /= forceLength;
        }

        //Scale force by distance needed
        Vector2 force = forceDirection * moveForce * (forceLength / (MoveSpeed * 2));

        //Zero vertical force if unable to fly
        if (!canFly)
        {
            force.y = 0;
        }

        ApplyForce(force);
    }

    /// <summary>
    /// Applies an upwards force to the character
    /// </summary>
    public void Jump()
    {
        if(Velocity.y < jumpSpeed)
        {
            Velocity = new Vector2(Velocity.x, jumpSpeed);
        }
    }

    /// <summary>
    /// Kills the entity
    /// </summary>
    public abstract void Die();

    /// <summary>
    /// Resets the entity to its spawn values
    /// </summary>
    public virtual void Reset()
    {
        health = maxHealth;
        stamina = maxStamina;
    }

    #endregion
}
