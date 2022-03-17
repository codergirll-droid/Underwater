using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    //public InventoryItemController[] InventoryItems;

    List<InventoryItemController> InventoryItems = new List<InventoryItemController>();
    List<int> itemCounts = new List<int>();


    private void Awake()
    {
        Instance = this;
    }


    public void AddItem(Item item)
    {
        //edited
        if (!Items.Contains(item))
        {
            Items.Add(item);
            itemCounts.Add(1);
        }
        else
        {
            itemCounts[Items.IndexOf(item)]++;
        }
    }

    //DEBUG HERE
    public bool RemoveItem(Item item)
    {
        if(itemCounts[Items.IndexOf(item)] > 1)
        {
            Debug.Log("RemoveItem/InventoryManager Called"); 
            itemCounts[Items.IndexOf(item)]--;
            Debug.Log("Item " + item.itemName + " count is " + itemCounts[Items.IndexOf(item)]);
            return false;
        }
        else
        {
            Items.Remove(item);
            return true;

        }
    }


    public void ListItems()
    {
        Debug.Log("ListItems Called");

        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        Debug.Log("items in item content is " + ItemContent.childCount);

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").gameObject.GetComponent<Button>();
            var itemCountTxt = obj.transform.Find("ItemCount").gameObject.GetComponent<TMPro.TMP_Text>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemCountTxt.text = itemCounts[Items.IndexOf(item)].ToString();


            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }

        }


        SetInventoryItems();


    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems.Clear();



        foreach (InventoryItemController item in ItemContent.GetComponentsInChildren<InventoryItemController>())
        {
            InventoryItems.Add(item);
        }

        //InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();


        for (int i = 0; i < Items.Count; i++)
        {
            
            InventoryItems[i].AddItem(Items[i]);


        }



    }




}
