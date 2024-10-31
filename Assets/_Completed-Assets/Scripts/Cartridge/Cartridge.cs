using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    public float BlinkInterval = 1.0f; // ���ł̊Ԋu
    public float DestroyTime = 1.5f;   // �J�[�g���b�W�����ł���܂ł̎���

    private Renderer cartridgeRenderer;

    // Start is called before the first frame update
    void Start()
    {
        cartridgeRenderer = GetComponent<Renderer>();
        StartCoroutine(BlinkAndDestroy());
    }

    private IEnumerator BlinkAndDestroy()
    {
        float elapsedTime = 0f;

        while (elapsedTime < DestroyTime)
        {
            cartridgeRenderer.enabled = !cartridgeRenderer.enabled; // ����
            yield return new WaitForSeconds(BlinkInterval);
            elapsedTime += BlinkInterval;
        }

        Destroy(gameObject); // ��莞�Ԍ�ɏ���
    }
}
