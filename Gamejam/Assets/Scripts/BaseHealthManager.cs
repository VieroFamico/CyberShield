using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealthManager : MonoBehaviour
{
    public static BaseHealthManager instance;

    public GameObject losePanel;
    public GameObject winPanel;
    public HorizontalLayoutGroup healthDisplay;
    public List<GameObject> healthImages;

    public int HealthPoint = 5;
    private void Awake()
    {
        instance = this;
        for(int i  = 0; i < healthDisplay.transform.childCount; i++)
        {
            healthImages.Add(healthDisplay.transform.GetChild(i).gameObject);
        }
    }
    
    public void TakeDamage(int damage)
    {
        HealthPoint -= damage;
        Destroy(healthImages[HealthPoint]);

        if (HealthPoint <= 0)
        {
            Lose();
        }
    }

    private void Lose()
    {
        Debug.Log("Lost");
    }
}
