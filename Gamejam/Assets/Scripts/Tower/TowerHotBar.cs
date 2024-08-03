using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        public Image cooldownImage;
        public TextMeshProUGUI cooldownText;
    }

    public BuildingBar[] buildingBar;

    public Button deselectBuilding;
    public Image selectedBuildingImage;

    private Base_Tower selectedTower = null;
    private BuildingNode selectedBuildNode = null;

    private void Awake()
    {
        foreach (BuildingBar b in buildingBar)
        {
            b.towerButton.onClick.AddListener(() => SelectBuilding(b.towerPrefabs));
        }
        deselectBuilding.onClick.AddListener(DeselectBuilding);
        deselectBuilding.gameObject.SetActive(false);
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
                        Instantiate(selectedTower, selectedBuildNode.transform.position, Quaternion.identity, selectedBuildNode.transform);
                        selectedBuildNode.PlacedATower();

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
    }

    private void SelectBuilding(Base_Tower selectedTowerPrefabs)
    {
        selectedTower = selectedTowerPrefabs;
        selectedBuildingImage.gameObject.SetActive(true);
        selectedBuildingImage.sprite = selectedTower.GetComponent<SpriteRenderer>().sprite;

        deselectBuilding.gameObject.SetActive(true);
    }

    private void DeselectBuilding()
    {
        selectedTower = null;
        selectedBuildingImage.gameObject.SetActive(false);
        selectedBuildingImage.sprite = null;

        deselectBuilding.gameObject.SetActive(false);
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
