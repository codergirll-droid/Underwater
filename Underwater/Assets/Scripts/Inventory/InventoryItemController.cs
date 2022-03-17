using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        Debug.Log("RemoveItem/InventoryItemController called");

        bool destroyItem = InventoryManager.Instance.RemoveItem(item);

        if (destroyItem)
        {
            Destroy(gameObject);

        }


    }


    public void AddItem(Item newItem)
    {
        item = newItem;

    }

    public void UseItem()
    {
        Debug.Log("Item name is " + item);
        Debug.Log("UseItem called, item.itemType is " + item.itemType);

        switch (item.itemType)
        {
            case Item.ItemType.Weapon:
                //equip it maybe
                //player.instance.increasehealth
                RemoveItem();
                break;
            case Item.ItemType.Health:
                //increase health 
                Debug.Log("UseItem/Health called");
                RemoveItem();

                break;
            case Item.ItemType.Breath:
                //increase breath
                RemoveItem();

                break;

        }

    }


    public void updateCountofItem()
    {
        String num = gameObject.transform.Find("ItemCount").GetComponent<TMPro.TMP_Text>().text;
        int numInt = Int32.Parse(num);
        numInt--;
        gameObject.transform.Find("ItemCount").GetComponent<TMPro.TMP_Text>().text = numInt.ToString();
    }

}
