using System;
using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


        private string m_FireButton;                // The input axis that is used for launching shells.
        private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
        private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

        // 弾数管理用の変数を追加
        public int BulletStart = 10;                // ゲーム開始時の砲弾の所持数
        public int MaxBullet = 50;                    // 所持可能な最大砲弾数
        public int ReroadBullet = 10;            // 補充する砲弾の数
        private int CurrentBullet;                    // 現在の砲弾数
        private bool Increasing;             //飛距離ゲージが伸びてるかどうか
        public event Action<int> OnShellStockChanged; // 砲弾所持数の変化を通知するイベント

        private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;
        }


        private void Start()
        {
            // The fire axis is based on the player number.
            m_FireButton = "Fire" + m_PlayerNumber;

            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

            // 弾数の初期化
            CurrentBullet = BulletStart;
            OnShellStockChanged?.Invoke(CurrentBullet); // イベントを発生
        }

        private void Update()
        {
            if (CurrentBullet > 0) // 弾数が0でないときのみ処理
            {
                if (Input.GetButton(m_FireButton)) // ボタンが押されている間
                {
                    if (Increasing)
                    {
                        m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                        if (m_CurrentLaunchForce >= m_MaxLaunchForce)
                        {
                            m_CurrentLaunchForce = m_MaxLaunchForce;
                            Increasing = false; // 減少に切り替え
                        }
                    }
                    else
                    {
                        m_CurrentLaunchForce -= m_ChargeSpeed * Time.deltaTime;
                        if (m_CurrentLaunchForce <= m_MinLaunchForce)
                        {
                            m_CurrentLaunchForce = m_MinLaunchForce;
                            Increasing = true; // 増加に切り替え
                        }
                    }

                    // スライダーを現在の発射力に合わせて更新
                    m_AimSlider.value = m_CurrentLaunchForce;
                }

                if (Input.GetButtonDown(m_FireButton)) // ボタンが押されたときに発射準備
                {
                    m_Fired = false;
                    m_CurrentLaunchForce = m_MinLaunchForce;
                    m_ShootingAudio.clip = m_ChargingClip;
                    m_ShootingAudio.Play();
                }

                if (Input.GetButtonUp(m_FireButton) && !m_Fired) // ボタンが離されたら発射
                {
                    Fire();
                    m_CurrentLaunchForce = m_MinLaunchForce;
                    m_AimSlider.value = m_MinLaunchForce;
                }
            }
        }


        private void Fire()
        {
            if (CurrentBullet <= 0) return; // 弾がない場合は発射できない

            m_Fired = true;
            CurrentBullet--;  // 弾数を減少
            OnShellStockChanged?.Invoke(CurrentBullet); // イベントを発生

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            // Reset the launch force.  This is a precaution in case of missing button events.
            m_CurrentLaunchForce = m_MinLaunchForce;


        }

        public void Reload()
        {
            CurrentBullet += ReroadBullet;

            if (CurrentBullet > MaxBullet) //リロードしたときに最大弾数を超えないようにする
            {
                CurrentBullet = MaxBullet;
            }
            OnShellStockChanged?.Invoke(CurrentBullet); // イベントを発生
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("ShellCartridge")) //ShellCartridgeタグのついたものが触れたら(今回は)
            {
                Reload(); // リロード
                Destroy(collision.gameObject); // 衝突したカートリッジを消滅
            }
        }

    }
}