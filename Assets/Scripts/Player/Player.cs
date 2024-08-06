using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterStats
{
    public float radius = 2f;
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Attack");
            Attack();
        }
    }

    public void Attack()
    {
        Vector2 center = transform.position;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius, enemyLayer);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") &&
                collider.TryGetComponent<CharacterStats>(out var characterStats))
            {
                characterStats.healthChange(-damage);
            }
        }
    }

    // Draw the radius in the Scene view
    private void OnDrawGizmos()
    {
        // Set the Gizmo color
        Gizmos.color = Color.red;

        // Draw a wire sphere to visualize the radius
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
