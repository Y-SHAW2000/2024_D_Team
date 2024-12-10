using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasItem : MonoBehaviour
{
    private UserStateManager userStateManager;
    public Text staminaText; // �X�^�~�i�\���p��Text
    public Text ArmorText; // �X�^�~�i�\���p��Text
    private int stamina;
    private bool armor;
    void Awake()
    {
        userStateManager = FindObjectOfType<UserStateManager>();
    }
    void Start()
    {
        UpdateStaminaText(); // �����l��\��
        UpdateArmorText();
    }


    private void UpdateStaminaText()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        staminaText.text = $"Stamina: {playerinfo.Stamina}"; // �X�^�~�i�\�����X�V
    }

    private void UpdateArmorText()
    {
        if (UseItemManager.useArmor == true) 
        {
            ArmorText.text = $"Armor: {2}"; // �X�^�~�i�\�����X�V
        }
        else
        {
            ArmorText.text = $"Armor: {1}"; // �X�^�~�i�\�����X�V
        }
    }
}

