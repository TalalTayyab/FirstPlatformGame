using UnityEngine;

public class EntityAnimationEvents : MonoBehaviour
{
    private Entity entity;

    void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    private void EnableMovementAndJump()
    {
        entity.EnableMovementAndJump(true);
    }

    private void DisableMovementAndJump()
    {
        entity.EnableMovementAndJump(false);
    }
    
    private void DamageTarget()
    {
        entity.DamageTarget();
    }
}
