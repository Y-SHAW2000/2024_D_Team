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
    
    private void FetchLoginDays()
    {
        var playerinfo = userStateManager.CurrentPlayer;

        if (playerinfo != null&& UseItemManager.loginbonus == true)
        {
            // �����̃A�C�e���� +1 ����
            playerinfo.StaminaItem += 1;
            playerinfo.ArmorItem += 1;

            Debug.Log($"���O�C���ɂ�� StaminaItem �� ArmorItem �� +1 ���܂����B���݂� StaminaItem: {playerinfo.StaminaItem}, ArmorItem: {playerinfo.ArmorItem}");

            // �f�[�^�̍X�V
            userStateManager.SavePlayerinfo(playerinfo);
            Debug.Log($"�X�V���܂����X�^�~�i: {playerinfo.StaminaItem}�A�[�}�[: {playerinfo.ArmorItem}");

            // �e�L�X�g�̍X�V
            UpdateText(playerinfo.ArmorItem, playerinfo.StaminaItem);
            UseItemManager.loginbonus = false; //���O�C���{�[�i�X�͎󂯎��ς�
        }
        else if(playerinfo != null && UseItemManager.loginbonus == false)
        {
            Debug.Log("���O�C���{�[�i�X�͂����󂯎������");
            // �f�[�^�̍X�V
            userStateManager.SavePlayerinfo(playerinfo);
            Debug.Log($"�X�V���܂����X�^�~�i: {playerinfo.StaminaItem}�A�[�}�[: {playerinfo.ArmorItem}");

            // �e�L�X�g�̍X�V
            UpdateText(playerinfo.ArmorItem, playerinfo.StaminaItem);
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
