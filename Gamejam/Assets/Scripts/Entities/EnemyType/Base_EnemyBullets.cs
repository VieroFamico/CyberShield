using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_EnemyBullets : MonoBehaviour
{
    public Transform target;
    public int damage = 10;
    public float moveSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
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
        Base_Tower tower = collision.gameObject.GetComponent<Base_Tower>();
        if (tower != null)
        {
            Debug.Log("Hit Tower");
            tower.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
    }
}
