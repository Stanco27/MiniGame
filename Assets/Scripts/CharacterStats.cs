using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public int damage = 1;
    public int maxHealth = 10;
    public int health;
    public Animator animator;
    public bool isDying = false;
    public Rigidbody2D rb;
    


    public int getDamamge { get { return damage; } }

    void Start()
    {
        health = maxHealth;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator component not found on " + gameObject.name);
            }
        }
    }

    public void HealthChange(int number)
    {
        health += number;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health < 1)
        {
            Die();
        }
    }

    private void Die()
    {
        isDying = true;
        animator.SetTrigger("Dead");
    }
    public void OnDeathAnimation()
    {
        Destroy(gameObject);
    }

}
