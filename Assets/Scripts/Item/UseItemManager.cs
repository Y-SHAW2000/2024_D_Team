
using UnityEngine;

public class UseItemManager : MonoBehaviour
{
    public static bool useStamina = false;
    public static bool useArmor = false;

    public static void UseStaminaItem()
    {
        useStamina = true;
        Debug.Log("スタミナアイテムが使用されました！");
    }

    public static void UseArmorItem()
    {
        useArmor = true;
        Debug.Log("アーマーアイテムが使用されました！");
    }

    public static void ResetItemStates()
    {
        useStamina = false;
        useArmor = false;
        Debug.Log("アイテム使用状態をリセットしました！");
    }
}
