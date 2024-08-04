using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EnemySpawner;
using static UnityEngine.UI.Image;

public class TowerHotBar : MonoBehaviour
{
    [System.Serializable]
    public class BuildingBar
    {
        public Button towerButton;
        public Base_Tower towerPrefabs;
        public float towerCooldown;
        public Image cooldownImage;
        public TextMeshProUGUI cooldownText;
        public int index;
    }

    public List<BuildingBar> buildingBar;

    public Image selectedBuildingImage;

    public GameObject hintForWhenSelected;

    private Base_Tower selectedTower = null;
    private BuildingNode selectedBuildNode = null;
    

    private List<float> towerCooldownTimer = new();
    private int selectedIndex = -1;

    private void Awake()
    {
        int i = 0;
        foreach (BuildingBar b in buildingBar)
        {
            b.towerButton.onClick.AddListener(() => SelectBuilding(b));
            b.index = i;
            towerCooldownTimer.Add(0f);
            i++;
        }

        selectedBuildingImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if(selectedTower != null)
        {
            if (selectedBuildNode == null)
            {
                selectedBuildingImage.gameObject.SetActive(true);
                selectedBuildingImage.transform.position = Input.mousePosition;
            }
            else
            {
                selectedBuildingImage.gameObject.SetActive(false);

                if(Input.GetMouseButtonDown(0))
                {
                    if (selectedBuildNode.IsPlacable())
                    {
                        Base_Tower tower = Instantiate(selectedTower, selectedBuildNode.transform.position, Quaternion.identity, selectedBuildNode.transform);
                        selectedBuildNode.PlacedATower();
                        tower.placedBuildingNode = selectedBuildNode;
                        towerCooldownTimer[selectedIndex] = buildingBar[selectedIndex].towerCooldown;


                        DeselectBuilding();
                    }
                }
            }

            GetSelectedNode();
        }
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            DeselectBuilding();
        }

        CooldownTimer();
    }
    private void CooldownTimer()
    {
        for(int i = 0; i < towerCooldownTimer.Count; i++)
        {
            towerCooldownTimer[i] -= Time.deltaTime;

            if (towerCooldownTimer[i] < 0f)
            {
                buildingBar[i].cooldownImage.fillAmount = 0f;
                buildingBar[i].cooldownText.gameObject.SetActive(false);
                buildingBar[i].cooldownImage.gameObject.SetActive(false);
            }
            else
            {
                buildingBar[i].cooldownText.text = Mathf.RoundToInt(towerCooldownTimer[i]).ToString();
                buildingBar[i].cooldownImage.fillAmount = towerCooldownTimer[i] / buildingBar[i].towerCooldown;
                buildingBar[i].cooldownText.gameObject.SetActive(true);
                buildingBar[i].cooldownImage.gameObject.SetActive(true);
            }
        }
    }

    private void SelectBuilding(BuildingBar selectedTowerButton)
    {
        if (selectedTowerButton.towerCooldown < towerCooldownTimer[selectedTowerButton.index])
        {
            return;
        }

        selectedIndex = selectedTowerButton.index;
        selectedTower = selectedTowerButton.towerPrefabs;
        selectedBuildingImage.gameObject.SetActive(true);
        selectedBuildingImage.sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;
    }

    private void DeselectBuilding()
    {
        selectedIndex = -1;
        selectedTower = null;
        selectedBuildingImage.gameObject.SetActive(false);
        selectedBuildingImage.sprite = null;
        
    }

    public void SelectBuildNode(BuildingNode buildNode)
    {
        if(selectedBuildNode != null)
        {
            selectedBuildNode.RemoveSprite();
        }

        if(buildNode == null)
        {
            selectedBuildNode = null;
            return;
        }

        selectedBuildNode = buildNode;
        selectedBuildNode.SetSprite(selectedBuildingImage.sprite);
    }

    private void GetSelectedNode()
    {
        Vector2 dir = Vector2.zero;

        Vector2 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(origin, dir);

        if (hit)
        {
            if (hit.collider.gameObject.TryGetComponent<BuildingNode>(out var collidedNode))
            {
                if(selectedBuildNode == collidedNode)
                {
                    return;
                }
                else
                {
                    SelectBuildNode(collidedNode);
                }
            }
            else
            {
                SelectBuildNode(null);
            }
        }
        else
        {
            if (selectedBuildNode != null)
            {
                selectedBuildNode.RemoveSprite();
            }
            selectedBuildNode = null;
        }
    }
}
