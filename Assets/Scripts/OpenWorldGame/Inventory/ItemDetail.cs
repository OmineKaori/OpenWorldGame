using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetail : MonoBehaviour
{
    #region Singleton
    public static ItemDetail instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Mode than one instance of Item Detail found!");
            return;
        }
        
        instance = this;
    }
    #endregion

    private InventorySlot firstItemSlot;
    public Item item;
    
    public Image itemImage;
    public Text itemName;
    public Text itemDescription;

    public void ClearDetailUI()
    {
        itemImage.enabled = false;
        itemName.text = "";
        itemDescription.text = "";
    }
    
    public void Show(Item showedItem)
    {
        item = showedItem;

        if (item != null)
        {
            itemImage.sprite = item.icon;
            itemImage.enabled = true;
            itemName.text = item.name;
            itemDescription.text = item.description;
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            ClearDetailUI();
        }    
    }
}
