
using UnityEngine;
using UnityEngine.UI; // Slider ���������߂ɕK�v

public class TankArmorHealth : MonoBehaviour
{
    public Slider m_Slider200;

    // ������������ǉ�
    public void InitializeArmorBar(float maxArmorValue)
    {
        if (m_Slider200 != null)
        {
            m_Slider200.maxValue = maxArmorValue;
            m_Slider200.value = maxArmorValue; // �����l���ő�l�ɐݒ�
        }
        else
        {
            Debug.LogWarning("m_Slider200 ���ݒ肳��Ă��܂���B");
        }
    }
    // �A�[�}�[�l���X�V���郁�\�b�h
    public void UpdateArmor(float currentArmorValue)
    {
        if (m_Slider200 != null)
        {
            if(currentArmorValue == 0)
            {
                Debug.Log("�A�[�}�[��0�ɂȂ��������");
            }

            // �A�[�}�[�̒l�����݂̒l�ɍX�V
            m_Slider200.value = Mathf.Clamp(currentArmorValue, 0, m_Slider200.maxValue);
            Debug.Log($"�A�[�}�[�o�[���X�V: ���݂̃A�[�}�[�l {currentArmorValue}");
        }
        else
        {
            Debug.LogWarning("m_Slider200 ���ݒ肳��Ă��܂���B");
        }
    }
}
