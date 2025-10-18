using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyScript : Entity
{

    void Awake()
    {
        target = GameObject.FindAnyObjectByType<CatScript>().transform;

        if (transform.position.x > target.position.x)
        {
            facingRight = true;
        }
            

    }
    
    protected override float MoveDirectionX
    {
        get
        {
            float diff = target.position.x - transform.position.x;
            if (Mathf.Abs(diff) < 0.05f) // Small threshold to avoid floating point issues
                return 0f;
            return Mathf.Sign(diff);
        }
    }

    protected override bool Attack => Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, targetLayer).Length > 0;

    override protected void Die()
    {
        base.Die();
        Destroy(gameObject, 4f); // Destroy the enemy after 1 second to allow death animation to play
        UIScript.Instance.UpdateKillCount();
    }

}
