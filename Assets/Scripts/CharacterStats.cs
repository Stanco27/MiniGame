using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    private int health;
    public int damage = 1;
    public int maxHealth;
    public Animator animator;

    void Start() {
        health = maxHealth;
    }

    public void healthChange(int number)
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
        Debug.Log("Dead");
        gameObject.SetActive(false);
    }

}
