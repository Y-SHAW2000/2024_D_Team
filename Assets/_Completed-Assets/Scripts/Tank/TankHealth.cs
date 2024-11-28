using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Complete
{
    public class TankHealth : MonoBehaviour
    {
        public float m_StartingHealth = 100f;               // The amount of health each tank starts with.
        public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        public Image m_FillImage;                           // The image component of the slider.
        public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.
        
        
        private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
        private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
        private float m_CurrentHealth;                      // How much health the tank currently has.
        private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?
        private PhotonView photonView;      

        private void Awake ()
        {
             photonView = GetComponent<PhotonView>();
            // Instantiate the explosion prefab and get a reference to the particle system on it.
            m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();

            // Get a reference to the audio source on the instantiated prefab.
            m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();

            // Disable the prefab so it can be activated when it's required.
            m_ExplosionParticles.gameObject.SetActive (false);

            
        }

        private void Start()
        {
            m_Slider.maxValue = m_StartingHealth;
            m_Slider.value = m_Slider.maxValue;
        }


        private void OnEnable()
        {
            // When the tank is enabled, reset the tank's health and whether or not it's dead.
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;

            // Update the health slider's value and color.
            SetHealthUI();
        }

        // ダメージを受けた際、HPゲージだけを他のプレイヤーと同期
        [PunRPC]
        public void UpdateOpponentHealth(float health)
        {
            // 受け取った相手のHPをスライダーに反映
            m_Slider.value = health;
            SetHealthUI(); // ゲージの色なども更新
        }

        [PunRPC]
        public void TakeDamage(float amount)
        {
            if (photonView.IsMine) // 自分の戦車に対してのみダメージを適用
            {
                m_CurrentHealth -= amount;
                
                // 自分のHPゲージを更新したら、相手に通知
                photonView.RPC("UpdateOpponentHealth", RpcTarget.Others, m_CurrentHealth);
                if (m_CurrentHealth <= 0f && !m_Dead)
                {
                    m_Dead = true;
                    photonView.RPC("OnDeath", RpcTarget.All); // 全員に死を通知
                }
            }
        }


        private void SetHealthUI ()
        {
            // Set the slider's value appropriately.
            m_Slider.value = m_CurrentHealth;

            // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
            m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
        }

        [PunRPC]
        private void OnDeath ()
        {
            // Set the flag so that this function is only called once.
            m_Dead = true;
            photonView.RPC("DeactivateTank", RpcTarget.All);
            // Move the instantiated explosion prefab to the tank's position and turn it on.
            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive (true);

            // Play the particle system of the tank exploding.
            m_ExplosionParticles.Play ();

            // Play the tank explosion sound effect.
            m_ExplosionAudio.Play();

            // Turn the tank off.
            gameObject.SetActive (false);
        }
        [PunRPC]
        private void DeactivateTank()
        {
            gameObject.SetActive(false);
        }
    }
}