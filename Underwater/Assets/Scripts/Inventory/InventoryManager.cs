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

    public InventoryItemController[] InventoryItems;

    List<int> itemCounts = new List<int>();
    List<Item> newList = new List<Item>();


    private void Awake()
    {
        Instance = this;
    }


    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    //DEBUG HERE
    public void RemoveItem(Item item)
    {
        if(itemCounts[newList.IndexOf(item)] > 1)
        {
            itemCounts[newList.IndexOf(item)]--;
            //transform.Find("ItemCount").gameObject.GetComponent<TMPro.TMP_Text>().text = itemCounts[newList.IndexOf(item)].ToString();
            Debug.Log("Item " + item.itemName + " count is " + itemCounts[newList.IndexOf(item)]);
        }
        else
        {
            Items.Remove(item);

        }
    }


    public void ListItems()
    {
        newList.Clear();
        itemCounts.Clear();

        foreach (var item in Items)
        {
            if(!newList.Contains(item))
            {
                newList.Add(item);
                itemCounts.Add(1);
            }
            else
            {
                itemCounts[newList.IndexOf(item)]++;
            }
            
        }

        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }


        foreach(var item in newList)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").gameObject.GetComponent<Button>();
            var itemCountTxt = obj.transform.Find("ItemCount").gameObject.GetComponent<TMPro.TMP_Text>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemCountTxt.text = itemCounts[newList.IndexOf(item)].ToString();


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
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < newList.Count; i++)
        {
            InventoryItems[i].AddItem(newList[i]);
        }
    }

}
