
using UnityEngine;
using UnityEngine.UI; // Slider を扱うために必要

public class TankArmorHealth : MonoBehaviour
{
    public Slider m_Slider200;

    // 初期化処理を追加
    public void InitializeArmorBar(float maxArmorValue)
    {
        if (m_Slider200 != null)
        {
            m_Slider200.maxValue = maxArmorValue;
            m_Slider200.value = maxArmorValue; // 初期値を最大値に設定
        }
        else
        {
            Debug.LogWarning("m_Slider200 が設定されていません。");
        }
    }
    // アーマー値を更新するメソッド
    public void UpdateArmor(float currentArmorValue)
    {
        if (m_Slider200 != null)
        {
            if(currentArmorValue == 0)
            {
                Debug.Log("アーマーが0になっちゃった");
            }

            // アーマーの値を現在の値に更新
            m_Slider200.value = Mathf.Clamp(currentArmorValue, 0, m_Slider200.maxValue);
            Debug.Log($"アーマーバーを更新: 現在のアーマー値 {currentArmorValue}");
        }
        else
        {
            Debug.LogWarning("m_Slider200 が設定されていません。");
        }
    }
}
