using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    public float BlinkInterval = 1.0f; // 明滅の間隔
    public float DestroyTime = 1.5f;   // カートリッジが消滅するまでの時間

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
            cartridgeRenderer.enabled = !cartridgeRenderer.enabled; // 明滅
            yield return new WaitForSeconds(BlinkInterval);
            elapsedTime += BlinkInterval;
        }

        Destroy(gameObject); // 一定時間後に消滅
    }
}
