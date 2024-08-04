using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPoints : MonoBehaviour
{
    public static EnemyWayPoints instance;
    [System.Serializable]
    public class SpawnPoints
    {
        public Transform[] points;
    }

    public SpawnPoints[] spawnpoints;
    private void Awake()
    {
        instance = this;
        /*points = new Transform[transform.childCount];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
            Debug.Log(points[i].position);
        }*/
    }
    
}
