using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public class TankTeleport : MonoBehaviour
    {
        public Transform portal;
        public Transform targetPortal;
        public float teleportDuration = 3f;  // テレポート所要時間
        public float teleportCooldown = 5f;  // テレポートクールダウン

        public GameObject tankRenderer;
        private Collider tankCollider;
        private TankMovement tankMovement;
        private TankShooting tankShooting;
        public bool isTeleporting = false;
        private bool isOnCooldown = false;

        private bool rendering = true;


        void Start()
        {
            tankCollider = GetComponent<Collider>();
            tankMovement = GetComponent<TankMovement>();
            tankShooting = GetComponent<TankShooting>();
        }

        void Update()
        {
            //
            //Debug.Log("iscooldown :" + isOnCooldown);
            if (isTeleporting)
            {
                tankMovement.enabled = false;
                tankShooting.enabled = false;
                Blink();
            }

        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Portal") && !isOnCooldown)
            {
                portal = collision.collider.transform;
                targetPortal = portal.GetComponent<PortalTarget>().GetTarget();
                StartCoroutine(Teleport());
            }
        }

        private IEnumerator Teleport()
        {
            isTeleporting = true;
            isOnCooldown = true;
            tankCollider.enabled = false; // ダメージを受けない

            //地雷の設置不可

            float elapsedTime = 0;
            while (elapsedTime < teleportDuration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 転送
            transform.position = targetPortal.position;

            // リセット
            isTeleporting = false;
            tankRenderer.SetActive(true);
            tankMovement.enabled = true;
            tankShooting.enabled = true;
            tankCollider.enabled = true; // ダメージが受ける


            // クールダウン
            yield return new WaitForSeconds(teleportCooldown);
            isOnCooldown = false;
        }

        private void Blink()
        {
            rendering = !rendering;
            tankRenderer.SetActive(rendering);
        }
    }

}
