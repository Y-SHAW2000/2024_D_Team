

using UnityEngine;

public class UseItemManager : MonoBehaviour
{
    public static bool useStamina = false;
    public static bool useArmor = false;
    public static bool loginbonus = false; //ログインボーナスを受け取ったかどうか　初期値　受け取った

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
    public static void GetLogInBonus()
    {
        loginbonus = true; //まだ受け取ってない
        Debug.Log("今日のログインボーナスは受け取り済み！");
    }
}
