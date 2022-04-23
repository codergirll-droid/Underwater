using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //data to be saved
    public int playerHealth;
    public int playerBreath;
    public int levelSceneIndex;

    public List<Item> Items = new List<Item>();
    public List<int> itemCounts = new List<int>();


    public GameData(Player player, InventoryManager inventoryManager)
    {
        playerHealth = player.health;
        playerBreath = player.breath;
        levelSceneIndex = player.activeSceneIndex;

        Items = inventoryManager.Items;
        itemCounts = inventoryManager.itemCounts;
    }



}
