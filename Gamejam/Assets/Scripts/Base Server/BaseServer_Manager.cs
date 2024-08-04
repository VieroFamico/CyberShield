using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseServer_Manager : MonoBehaviour
{

    public int maxHealthPoint = 5;
    private float healthPoint;

    private void Awake()
    {
        healthPoint = maxHealthPoint;
    }

    public void TakeDamage()
    {
        healthPoint--;
        if(healthPoint <= 0)
        {

        }
    }
}
