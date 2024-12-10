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
            // "Assets/Resources/GameItemData.json" �� Resources ���ł̃p�X���w��
            string filePath = "GameItemData";
            TextAsset file = Resources.Load<TextAsset>(filePath);

            if (file != null)
            {
                items = JsonUtility.FromJson<ItemDataArray>(file.text);
            }
            else
            {
                Debug.LogError($"JSON �t�@�C����������܂���: {filePath}");
                items = new ItemDataArray { gameItems = new Item[0] }; // ��̔z���������
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
                        Debug.Log($"�Ăяo�����A�C�e����: {itemName}�l��{item.effect}");
                        return item.effect;
                    }
                }
            }

            Debug.LogWarning($"�A�C�e����������܂���ł���: {itemName}");
            return 0; // �f�t�H���g�̌��ʒl
        }
    }
}
