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
        InventoryManager.Instance.RemoveItem(item);

        Destroy(gameObject);
    }


    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {

        switch (item.itemType)
        {
            case Item.ItemType.Weapon:
                //equip it maybe
                //player.instance.increasehealth
                RemoveItem();
                break;
            case Item.ItemType.Health:
                //increase health 
                RemoveItem();

                break;
            case Item.ItemType.Breath:
                //increase breath
                RemoveItem();

                break;

        }

    }

}
