
using UnityEngine;

public class UseItemManager : MonoBehaviour
{
    public static bool useStamina = false;
    public static bool useArmor = false;

    public static void UseStaminaItem()
    {
        useStamina = true;
        Debug.Log("�X�^�~�i�A�C�e�����g�p����܂����I");
    }

    public static void UseArmorItem()
    {
        useArmor = true;
        Debug.Log("�A�[�}�[�A�C�e�����g�p����܂����I");
    }

    public static void ResetItemStates()
    {
        useStamina = false;
        useArmor = false;
        Debug.Log("�A�C�e���g�p��Ԃ����Z�b�g���܂����I");
    }
}
