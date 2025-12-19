using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform cellsInventory;
    [SerializeField] private Transform itemCell;

    [SerializeField] private List<ItemsSO> itemsSOList;
    [SerializeField] private int maxInventory = 5;


    private void Awake()
    {
        itemCell.gameObject.SetActive(false);
    }

    public void AddInventory(ItemsSO item)
    {
        if (maxInventory <= itemsSOList.Count)
            return;

        itemsSOList.Add(item);
        UpdateVisual();
    }

    public List<ItemsSO> GetInventoryList()
    {
        if (itemsSOList == null) return null;

        return itemsSOList;
    }
    
    public void UpdateVisual()
    {
        foreach (Transform child in cellsInventory)
        {
            if(child == itemCell) continue;
            Destroy(child.gameObject);
        }

        foreach (ItemsSO item in itemsSOList)
        {

            Transform transformCellInventory = Instantiate(itemCell, cellsInventory);
            transformCellInventory.gameObject.SetActive(true);
            transformCellInventory.GetComponent<ItemCell>().SetIcon(item);
            
        }
    }
}
