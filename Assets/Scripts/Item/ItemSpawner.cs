using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public int spawnItemId;
    public int spawnQuantity;
    private ItemCreator creator;

    private void Start()
    {
        creator = GameManager.Instance.itemCreator;

        SpawnItem();
    }

    private void SpawnItem()
    {
        creator.CreateItem(spawnItemId, spawnQuantity, this.transform.position);
    }
}
