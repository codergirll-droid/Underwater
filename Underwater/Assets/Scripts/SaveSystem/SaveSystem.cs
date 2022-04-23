using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void saveGame(Player player, InventoryManager inventoryManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/underwater.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(player, inventoryManager);

        formatter.Serialize(stream, data);
        stream.Close();


    }


    public static GameData loadGame()
    {
        string path = Application.persistentDataPath + "/underwater.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File cannot be found at " + path);
            return null;
        }
    }


}
