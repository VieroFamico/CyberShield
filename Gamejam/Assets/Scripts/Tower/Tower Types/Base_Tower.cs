using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Tower : MonoBehaviour
{
    [Header("Attributes")]
    public Transform target;
    public BuildingNode placedBuildingNode;
    public int healthPoint;
    public float range = 10f;

    [Header("Firing")]
    public GameObject bulletPrefabs;
    public Transform firePoint;
    public int damage;
    public float secondsUntilFire = 1f;
    public float fireCountdown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector2 dir = target.position - transform.position;

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = secondsUntilFire;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
        Base_Bullet bulletScript = bullet.GetComponent<Base_Bullet>();
        if (bulletScript)
        {
            bulletScript.target = target;
            bulletScript.damage = damage;
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            healthPoint = 0;
            Destroy(gameObject);
            placedBuildingNode.PlacedATower();
            return;
        }
    }

    #region Visual And Getting Enemy

    private IEnumerator UpdateTarget()
    {
        while (true)
        {
            Base_Enemy[] enemies = FindObjectsOfType<Base_Enemy>();
            float shortestDistance = float.PositiveInfinity;
            Base_Enemy nearestEnemy = null;

            foreach (Base_Enemy enemy in enemies)
            {
                float distanceFromEnemy = Vector2.Distance(transform.position, enemy.transform.position);

                if(shortestDistance > distanceFromEnemy)
                {
                    shortestDistance = distanceFromEnemy;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null && shortestDistance < range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    #endregion
}
