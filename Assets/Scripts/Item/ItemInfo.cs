using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemInfo : MonoBehaviour
{
    private UserStateManager userStateManager;
    private LoginBonusManager loginBonusManager;

    private int loginDays;
    private int stamina;
    private int armor;

    public Text armorText;
    public Text staminaText;


    IEnumerator Start()
    {

        userStateManager = FindObjectOfType<UserStateManager>();
        if (userStateManager == null)
        {
            Debug.LogError("UserStateManager ��������܂���I");
            yield break;
        }

        loginBonusManager = FindObjectOfType<LoginBonusManager>();
        if (loginBonusManager == null)
        {
            Debug.LogError("LoginBonusManager ��������܂���I");
            yield break;
        }

        while (userStateManager.CurrentPlayer == null)
        {
            Debug.Log("CurrentPlayer�ݒ蒆");
            yield return null;
        }

        Debug.Log("LoginBonusDisplay: CurrentPlayer ��F�����܂����I");

        var playerinfo = userStateManager.CurrentPlayer;
        loginDays = playerinfo.Loginday;
        stamina = playerinfo.StaminaItem;
        armor = playerinfo.ArmorItem;

        FetchLoginDays();   
    }
    
    private void Getinfo()
    {
        var playerinfo = userStateManager.CurrentPlayer;
        loginDays = playerinfo.Loginday;
        stamina = playerinfo.StaminaItem;
        armor = playerinfo.ArmorItem;
    }
    private void FetchLoginDays()
    {
        var playerinfo = userStateManager.CurrentPlayer;

        if (playerinfo != null)
        {
            loginDays = playerinfo.Loginday;
            stamina = playerinfo.StaminaItem;
            armor = playerinfo.ArmorItem;

            // ���O�C�������ɉ���������
            if (loginDays % 2 == 0) // �����̏ꍇ
            {
                stamina++;
                Debug.Log($"���O�C�������������Ȃ̂� StaminaItem �� +1 ���܂����B���݂� StaminaItem: {stamina}");
            }
            else // ��̏ꍇ
            {
                armor++;
                Debug.Log($"���O�C����������Ȃ̂� ArmorItem �� +1 ���܂����B���݂� ArmorItem: {armor}");
            }
            userStateManager.SavePlayerinfo(playerinfo);�@//�f�[�^�̍X�V
            Debug.Log($"�X�V���܂����X�^�~�i: {stamina}�A�[�}�[: {armor}");
            UpdateText(armor, stamina);
        }
        else
        {
            Debug.LogError("CurrentPlayer ���ݒ肳��Ă��܂���I");
        }
    }

    public void UpdateText(int armor, int stamina)
    {
        Debug.Log($"Update stamina: {stamina},armor: {armor}");

        // �e�L�X�g���ݒ肳��Ă���ꍇ�ɍX�V
        if (staminaText != null)
        {
            staminaText.text = $"{stamina}";
        }
        else
        {
            Debug.LogError("StaminaText ���ݒ肳��Ă��܂���I");
        }

        if (armorText != null)
        {
            armorText.text = $"{armor}";
        }
        else
        {
            Debug.LogError("ArmorText ���ݒ肳��Ă��܂���I");
        }
    }
}
