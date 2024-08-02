using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthManager : MonoBehaviour
{
    public static BaseHealthManager instance;

    public int HealthPoint = 5;
    private void Awake()
    {
        instance = this;
    }
    
    public void TakeDamage(int damage)
    {
        HealthPoint -= damage;
        if(HealthPoint <= 0)
        {
            Lose();
        }
    }

    private void Lose()
    {
        Debug.Log("Lost");
    }
}
