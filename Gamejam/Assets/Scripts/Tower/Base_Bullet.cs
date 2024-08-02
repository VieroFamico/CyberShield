using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Bullet : MonoBehaviour
{
    public Transform target;
    public int damage = 10;
    public float moveSpeed = 5;

    public GameObject impactEffect;

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Base_Enemy enemy = collision.gameObject.GetComponent<Base_Enemy>();
        if (enemy != null)
        {
            GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
    }
}
