using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Complete
{
    public class TankHealth : MonoBehaviourPunCallbacks
    {
        public float m_StartingHealth = 100f;               // The amount of health each tank starts with.
        public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        public Slider m_Slider200;                          // アイテム使用後追加のHP
        public Image m_FillImage;                           // The image component of the slider.
        public Image m_FillImage200;    
        public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.
        
        private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
        private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
        private float m_CurrentHealth;                      // How much health the tank currently has.
        private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?
        private TankArmorHealth tankarmor;                  // アーマーを管理する
        private MasterData.JsonReaderFromResourcesFolder jsonReader;
        private int damage;

        private void Awake ()
        {
            // Instantiate the explosion prefab and get a reference to the particle system on it.
            m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();
            tankarmor = FindObjectOfType<TankArmorHealth>();

            // Get a reference to the audio source on the instantiated prefab.
            m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();

            // Disable the prefab so it can be activated when it's required.
            m_ExplosionParticles.gameObject.SetActive (false);
            jsonReader = new MasterData.JsonReaderFromResourcesFolder();
        }

        private void Start()
        {
            Debug.Log("tankhealth開始");
            m_Slider.maxValue = m_StartingHealth; // Maxは初期値
            m_Slider.value = m_Slider.maxValue; // 初期値を設定
            CheckItemUse(); // アイテムを使ったかの確認
        }

        private void OnEnable()
        {
            // When the tank is enabled, reset the tank's health and whether or not it's dead.
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;

            // Update the health slider's value and color.
            SetHealthUI();
        }

        // ダメージを受けた際に呼ばれる
        public void TakeDamage(float amount)
        {
            // アーマーがある場合、まずアーマーにダメージを適用
            if (m_Slider200.maxValue > 0 && m_Slider200.value > 0)
            {
                if (m_Slider200.value > amount)
                {
                    m_Slider200.value -= amount;
                }
                else
                {
                    float remainingDamage = Mathf.Abs(m_Slider200.value); // 余剰ダメージを計算
                    m_Slider200.value = 0;
                    m_CurrentHealth -= remainingDamage;
                    tankarmor.UpdateArmor(m_Slider200.value);
                }
            }
            else
            {
                // アーマーがない場合は直接体力にダメージを適用
                m_CurrentHealth -= amount;
            }

            // PUN2で同期を取るためにRPCでHP更新
            photonView.RPC("SyncHealth", RpcTarget.All, m_CurrentHealth, m_Slider200.value);

            // HPバーの更新
            SetHealthUI();

            // 体力が0以下になった場合の処理
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath();
            }
        }

        // RPCメソッドでHPを同期
        [PunRPC]
        private void SyncHealth(float currentHealth, float armorHealth)
        {
            m_CurrentHealth = currentHealth;
            m_Slider200.value = armorHealth;

            SetHealthUI();
        }

        // HPバーを更新
        private void SetHealthUI ()
        {
            if (m_Slider200.value > 0) // アーマーのHPバーを更新
            {
                tankarmor.UpdateArmor(m_Slider200.value);
                m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_Slider200.value / m_Slider200.maxValue);
            }  
            else
            {
                m_Slider.value = m_CurrentHealth;
                m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
            }
        }

        // タンクが死亡した時
        private void OnDeath ()
        {
            m_Dead = true;

            m_ExplosionParticles.transform.position = transform.position;
            m_ExplosionParticles.gameObject.SetActive (true);
            m_ExplosionParticles.Play ();
            m_ExplosionAudio.Play();

            gameObject.SetActive (false);
        }

        private void CheckItemUse()
        {
            if (UseItemManager.useArmor)
            {
                int armorEffect = jsonReader.GetEffectByName("戦車の装甲強化");

                if (armorEffect > 0)
                {
                    m_Slider200.maxValue = armorEffect;
                    m_Slider200.value = m_Slider200.maxValue;
                    tankarmor.InitializeArmorBar(armorEffect);
                }
            }
        }
    }
}
