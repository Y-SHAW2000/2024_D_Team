using UnityEngine;
using UnityEngine.UI;
using Complete;

public class PlayerStockArea : MonoBehaviour
{
    // �e�̎Q�� (Shell1�`Shell10)
    [SerializeField] private Image[] SingleBullet;

    // 10�����̒e�̎Q�� (Shells10�`Shells40)
    [SerializeField] private Image[] Bullets;

    // �����̃X�g�b�N��
    [SerializeField] private int initialStockCount = 10;

    private void Start()
    {
        // �����X�g�b�N������A�C�R���̕\���ݒ�
        UpdatePlayerStockArea(initialStockCount);
    }



    // �X�g�b�N���ɉ����ĖC�e�A�C�R����\������
    public void UpdatePlayerStockArea(int StockCount)
    {
        // 1���A�C�R���̕\���E��\���ݒ�
        for (int i = 0; i < SingleBullet.Length; i++)
        {
            // stockCount�ɂ���ăA�C�R����\���E��\��
            SingleBullet[i].gameObject.SetActive(i < StockCount % 10); //()���𖞂������\������
        }
 
        // 10���A�C�R���̕\���E��\���ݒ�
        int TenCount = StockCount / 10;
        for (int i = 0; i < Bullets.Length; i++)
        {
            Bullets[i].gameObject.SetActive(i < TenCount);
        }
        
    }
}

