using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasItem : MonoBehaviour
{
    private UserStateManager userStateManager;
    public Text staminaText; // スタミナ表示用のText
    public Text ArmorText; // スタミナ表示用のText
    private int stamina;
    private bool armor;
    void Awake()
    {
        userStateManager = FindObjectOfType<UserStateManager>();
    }
    void Start()
    {
        UpdateStaminaText(); // 初期値を表示
        UpdateArmorText();
    }


    private void UpdateStaminaText()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        staminaText.text = $"Stamina: {playerinfo.Stamina}"; // スタミナ表示を更新
    }

    private void UpdateArmorText()
    {
        if (UseItemManager.useArmor == true) 
        {
            ArmorText.text = $"Armor: {2}"; // スタミナ表示を更新
        }
        else
        {
            ArmorText.text = $"Armor: {1}"; // スタミナ表示を更新
        }
    }
}

