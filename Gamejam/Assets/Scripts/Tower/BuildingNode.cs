using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingNode : MonoBehaviour
{
    private GameObject spriteGameObject;

    private bool isPlaced = false;
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(Sprite towerSprite)
    {
        if(towerSprite != null)
        {
            if (!spriteGameObject)
            {
                spriteGameObject = new GameObject();
                SpriteRenderer spriteRenderer = spriteGameObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = towerSprite;
                spriteRenderer.sortingLayerName = "ForeGround";
                spriteRenderer.sortingOrder = 5;
                spriteGameObject.transform.position = transform.position;

            }
        }
    }
    public void RemoveSprite()
    {
        if(spriteGameObject != null) Destroy(spriteGameObject);
    }

    public bool IsPlacable()
    {
        return !isPlaced;
    }

    public void PlacedATower()
    {
        isPlaced = !isPlaced;
    }
}
