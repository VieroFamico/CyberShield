using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static EnemySpawner;

public class TowerHotBar : MonoBehaviour
{
    [System.Serializable]
    public class BuildingBar
    {
        public Button building;
        public Image cooldownImage;
        public TextMeshProUGUI cooldownText;
    }

    public BuildingBar[] buildingBar;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
