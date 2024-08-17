using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterStats
{

    public float radius;
    public LayerMask enemyLayer;
    public GameObject gameover;
    public float knockback = 0.05f;
    public bool isKnocked = false;
    public bool canGroundCheck = true;

    private void KnockBack(Collision2D other)
    {
        isKnocked = true;
        canGroundCheck = false;
        rb.velocity = Vector2.zero;

        Vector2 direction = (transform.position - other.transform.position).normalized;
        if(direction.x < 0) {
            direction.x = -0.5f;
        } else {
            direction.x = 0.5f;
        }

        Vector2 knockbackForce = new Vector2(direction.x * knockback, direction.y * knockback * 0.5f);
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(DelayGroundCheck(0.2f));
    }

    private IEnumerator DelayGroundCheck(float delay)
    {
        yield return new WaitForSeconds(delay);
        canGroundCheck = true;
        Debug.Log("Knockback ended. isKnocked reset to: " + isKnocked);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CharacterStats enemyStats = other.gameObject.GetComponent<CharacterStats>();

            if (enemyStats != null)
            {
                KnockBack(other);
                HealthChange(-enemyStats.damage);
                Debug.Log(health);
            }
            else
            {
                Debug.LogWarning("Enemy does not have a CharacterStats component.");
            }
        }
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }
}
