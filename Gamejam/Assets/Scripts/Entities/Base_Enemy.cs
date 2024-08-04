using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Base_Enemy : MonoBehaviour
{
    [Header("Attributes")]
    public Transform towerTarget;
    public float speed;
    public int healthPoint;

    [Header("Firing")]
    public GameObject bulletPrefabs;
    public Transform firePoint;
    public int damageDealt;
    public float range = 10f;
    public float secondsUntilFire = 1f;
    public float fireCountdown = 0f;

    private Transform destinationTarget;
    private int waypointIndex = 0;

    private int serverDamageDealt = 1;
    void Start()
    {
        destinationTarget = EnemyWayPoints.instance.points[waypointIndex];
        StartCoroutine(UpdateClosestTower());
    }

    // Update is called once per frame
    void Update()
    {
        float dir = destinationTarget.position.x - transform.position.x;

        if(dir < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        transform.position = Vector2.MoveTowards(transform.position, destinationTarget.position, speed * Time.deltaTime);
        
        if(MathF.Abs(Vector2.Distance(transform.position, destinationTarget.position)) < 0.1f)
        {
            GetNextTarget();
        }

        if (towerTarget == null) return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            Debug.Log("enemy shooting");
            fireCountdown = secondsUntilFire;
        }

        fireCountdown -= Time.deltaTime;
    }

    private void GetNextTarget()
    {
        waypointIndex++;
        if (waypointIndex >= EnemyWayPoints.instance.points.Length)
        {
            BaseHealthManager.instance.TakeDamage(serverDamageDealt);
            Destroy(gameObject);
            return;
        }

        destinationTarget = EnemyWayPoints.instance.points[waypointIndex];
    }

    #region Dealing and Taking Damage

    private IEnumerator UpdateClosestTower()
    {
        while(true)
        {
            Base_Tower[] towers = FindObjectsOfType<Base_Tower>();
            float shortestDistance = float.PositiveInfinity;
            Base_Tower nearestTower = null;

            foreach (Base_Tower tower in towers)
            {
                float distanceFromEnemy = Vector2.Distance(transform.position, tower.transform.position);

                if (shortestDistance > distanceFromEnemy)
                {
                    shortestDistance = distanceFromEnemy;
                    nearestTower = tower;
                }
            }
            if (nearestTower != null && shortestDistance < range)
            {
                towerTarget = nearestTower.transform;
            }
            else
            {
                towerTarget = null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
        Base_EnemyBullets bulletScript = bullet.GetComponent<Base_EnemyBullets>();
        if (bulletScript)
        {
            bulletScript.target = towerTarget;
            bulletScript.damage = damageDealt;
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        if(healthPoint <= 0)
        {
            healthPoint = 0;
            Destroy(gameObject);
            return;
        }
    }

    #endregion
}
