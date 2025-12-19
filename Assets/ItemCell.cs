using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour
{
    [SerializeField] private Image iconItemCell;

    private void Awake()
    {
        iconItemCell = GetComponent<Image>();
    }

    public void SetIcon(ItemsSO itemsSO)
    {
        iconItemCell.sprite = itemsSO.sprite;
    }
}
