using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class CartridgeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject CartridgePrefab;
    [SerializeField]
    private float spawnInterval = 5f; // �J�[�g���b�W�̐����Ԋu
    [SerializeField]
    private Vector2 spawnAreaMin; // �����͈͂̍ŏ��l�iX, Z���W�j
    [SerializeField]
    private Vector2 spawnAreaMax; // �����͈͂̍ő�l�iX, Z���W�j
    [SerializeField]
    public CartridgeData cartridgedata;

    private Complete.GameManager gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<Complete.GameManager>(); //�Q�[���I�u�W�F�N�g�ւ̎Q��

        // OnGameStateChanged �C�x���g�� HandleGameStateChanged ��o�^
        gameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(Complete.GameManager.GameState gameState) //�q���[�`���̊J�n�E��~
    {
        if (gameState == Complete.GameManager.GameState.RoundPlaying)
        {
            // �Q�[���v���C���Ȃ� SpawnRoutine �R���[�`�����J�n
            StartCoroutine(SpawnRoutine(cartridgedata));
        }
        else
        {
            // �Q�[�����v���C���łȂ��Ȃ�R���[�`�����~
            StopCoroutine(SpawnRoutine(cartridgedata));
        }
    }


    // �J�[�g���b�W�������_���Ȉʒu�ɐ������郁�\�b�h
    private void SpawnCartridge(CartridgeData cartridgedata)
    {
        // �����_���Ȉʒu���v�Z
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);//x���W�̒u����ʒu
        float randomZ = Random.Range(spawnAreaMin.y, spawnAreaMax.y);//z���W�̒u����ʒu
        Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ); // �n�ʂɔz�u���邽��Y���W��0

        // �J�[�g���b�W�𐶐�
        Instantiate(CartridgePrefab, spawnPosition, Quaternion.identity);
    }

    // ���Ԋu�ŃJ�[�g���b�W�𐶐�����R���[�`��
    private IEnumerator SpawnRoutine(CartridgeData cartridgedata)
    {
        while (true)
        {
            SpawnCartridge(cartridgedata); // �J�[�g���b�W�𐶐�
            yield return new WaitForSeconds(spawnInterval); // spawnInterval�b�ҋ@
        }
    }
}
