using System;
using UnityEngine;

public class PlayerScript : Entity
{
    protected override bool Attack => Input.GetButtonDown("Fire1") && isGrounded;

    protected override float MoveDirectionX => Input.GetAxisRaw("Horizontal");

    protected override bool Jump => Input.GetButtonDown("Jump");
    
    override protected void Die()
    {
        base.Die();
        UIScript.Instance.GameOver();
    }
    
}
