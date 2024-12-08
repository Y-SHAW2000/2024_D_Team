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

    [System.Serializable]
    public class ItemDataArray
    {
        public Item[] gameItems;
    }

    public class JsonReaderFromResourcesFolder
    {
        private ItemDataArray items;

        public JsonReaderFromResourcesFolder()
        {
            // "Assets/Resources/GameItemData.json" の Resources 内でのパスを指定
            string filePath = "GameItemData";
            TextAsset file = Resources.Load<TextAsset>(filePath);

            if (file != null)
            {
                items = JsonUtility.FromJson<ItemDataArray>(file.text);
            }
            else
            {
                Debug.LogError($"JSON ファイルが見つかりません: {filePath}");
                items = new ItemDataArray { gameItems = new Item[0] }; // 空の配列を初期化
            }
        }

        public int GetEffectByName(string itemName)
        {
            if (items != null && items.gameItems != null)
            {
                foreach (var item in items.gameItems)
                {
                    if (item.name == itemName)
                    {
                        Debug.Log($"呼び出したアイテムは: {itemName}値は{item.effect}");
                        return item.effect;
                    }
                }
            }

            Debug.LogWarning($"アイテムが見つかりませんでした: {itemName}");
            return 0; // デフォルトの効果値
        }
    }
}
