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
        public Button towerButton;
        public Base_Tower towerPrefabs;
        public Image cooldownImage;
        public TextMeshProUGUI cooldownText;
    }

    public BuildingBar[] buildingBar;

    private Base_Tower selectedTower = null;

    private void Awake()
    {
        foreach (BuildingBar b in buildingBar)
        {
            b.towerButton.onClick.AddListener(() => SelectBuilding(b.towerPrefabs));
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedTower != null)
        {

        }
    }

    private void SelectBuilding(Base_Tower selectedTowerPrefabs)
    {
        selectedTower = selectedTowerPrefabs;
    }
}
