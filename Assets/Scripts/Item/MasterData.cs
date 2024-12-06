using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterData : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public string description;
        public int effect;
        public int max;
    }
    public class ItemDataArray
    {
        public Item[] gameItems;
    }
    public class JsonReaderFromResourcesFolder
    {
        public ItemDataArray GetItemDataArray()
        {
            string filePath = "Assets/Resources/GameItemData.json";
            TextAsset file = Resources.Load(filePath) as TextAsset;
            ItemDataArray items = JsonUtility.FromJson<ItemDataArray>(file.text);
            return items;
        }
    }
}
