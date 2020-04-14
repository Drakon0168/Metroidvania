using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : LivingEntity
{
    private Vector2 moveInput;
    private bool attacking = false;

    #region Unity Functions

    protected override void Update()
    {
        if (!attacking)
        {
            Move(moveInput);
        }

        base.Update();
    }

    #endregion

    #region Living Entity

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Input Management

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        Jump();
    }

    #endregion
}
