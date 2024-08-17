using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{

    public GameObject player;
    public Transform playerTransform;
    public Animator playerAnimator;

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.localPosition = new Vector3(playerTransform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDied();
        }
        else if (other.CompareTag("Enemy"))
        {
            StartCoroutine(DestroyEnemyAfterDelay(other.gameObject, 3f));
        }
    }

    IEnumerator DestroyEnemyAfterDelay(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(enemy);
    }
    private void PlayerDied()
    {
        playerAnimator.SetTrigger("Dead");
    }




}
