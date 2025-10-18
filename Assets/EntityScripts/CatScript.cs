using UnityEngine;

public class CatScript : Entity
{
    override protected void Die()
    {
        base.Die();
        UIScript.Instance.GameOver();
    }
}
