using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler<EventArgsItem> OnGetItem;

    public class EventArgsItem
    {
        public ItemsSO Item;
    }

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

        OnGetItem?.Invoke(this, new EventArgsItem { Item = item });

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

    public void DeleteItem(ItemsSO itemToRemove)
    {
        if (itemsSOList.Contains(itemToRemove))
        {
            itemsSOList.Remove(itemToRemove);
            UpdateVisual();
        }
    }

    public bool HasInventory(ItemsSO item)
    {
        foreach (var itemInventory in itemsSOList)
        {
            if (itemInventory == item)
            {
                return true;
            }
        }
        return false;
    }
}
