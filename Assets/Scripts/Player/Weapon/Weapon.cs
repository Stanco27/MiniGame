using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 15f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    private readonly float despawnTime = 1f;

    public Player player;

    public void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (projectilePrefab != null && firePoint != null)
            {
                GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

                Rigidbody2D rb = projectileInstance.GetComponent<Rigidbody2D>();
                if(player.transform.localScale.x < 0 && rb != null) {
                    rb.velocity = -firePoint.right * projectileSpeed;
                }
                else if (player.transform.localScale.x > 0 && rb != null)
                {
                    rb.velocity = firePoint.right * projectileSpeed;
                }

                Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.SetDamage(player.damage);
                }

                StartCoroutine(Despawn(projectileInstance));
            }
        }
    }

    private IEnumerator Despawn(GameObject projectile)
    {
        yield return new WaitForSeconds(despawnTime);
        if (projectile != null)
        {
            Destroy(projectile);
        }
    }
}
