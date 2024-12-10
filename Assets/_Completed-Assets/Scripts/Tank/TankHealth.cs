﻿
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

namespace Complete
{
    public class TankHealth : MonoBehaviour
    {
        public float m_StartingHealth = 100f;               // The amount of health each tank starts with.
        public Slider m_Slider;                             // The slider to represent how much health the tank currently has.
        public Slider m_Slider200;                             //アイテム使用後ついかのHP
        public Image m_FillImage;                           // The image component of the slider.
        public Image m_FillImage200;                           // The image component of the slider.
        public Color m_FullHealthColor = Color.green;       // The color the health bar will be when on full health.
        public Color m_ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
        public GameObject m_ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.
        
        
        private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
        private ParticleSystem m_ExplosionParticles;        // The particle system the will play when the tank is destroyed.
        private float m_CurrentHealth;                      // How much health the tank currently has.
        private bool m_Dead;                                // Has the tank been reduced beyond zero health yet?
        private MasterData.JsonReaderFromResourcesFolder jsonReader;
        private int damage;

        private void Awake ()
        {
            // Instantiate the explosion prefab and get a reference to the particle system on it.
            m_ExplosionParticles = Instantiate (m_ExplosionPrefab).GetComponent<ParticleSystem> ();

            // Get a reference to the audio source on the instantiated prefab.
            m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource> ();

            // Disable the prefab so it can be activated when it's required.
            m_ExplosionParticles.gameObject.SetActive (false);
            jsonReader = new MasterData.JsonReaderFromResourcesFolder();

        }

        private void Start()
        {

            Debug.Log("tankhealth開始");
            m_Slider.maxValue = m_StartingHealth; //Maxは初期値
            m_Slider.value = m_Slider.maxValue;　//初期値を設定
            CheckItemUse(); //アイテムを使ったかの確認

        }


        private void OnEnable()
        {
            // When the tank is enabled, reset the tank's health and whether or not it's dead.
            m_CurrentHealth = m_StartingHealth;
            m_Dead = false;

            // Update the health slider's value and color.
            SetHealthUI();
        }


        public void TakeDamage(float amount)
        {
            // アーマーがある場合、まずアーマーにダメージを適用
            if (m_Slider200.maxValue > 0 && m_Slider200.value > 0)
            {
                if(m_Slider200.value > amount)
                {
                    m_Slider200.value -= amount;
                    Debug.Log($"今のアーマーは {m_Slider200.value}, 受けたダメージは: {amount}, currenthealthは {m_CurrentHealth}");
                }
                // アーマーが0未満になった場合、残りのダメージを体力に適用
                else if (m_Slider200.value < amount)
                {
                    float remainingDamage = Mathf.Abs(m_Slider200.value); // 余剰ダメージを計算
                    m_Slider200.value = 0; // アーマーを0にする
                    m_CurrentHealth -= remainingDamage; // 残りのダメージを体力に適用
                    Debug.Log($"受けたダメージは: {amount} アーマーが破壊され、残りダメージを適用:剰余ダメージ:{remainingDamage} currenthealthは {m_CurrentHealth}");
                }
                Debug.Log($"今のアーマーは {m_Slider200.value}, 受けたダメージは: {amount}, currenthealthは {m_CurrentHealth}");

            }
            else
            {
                // アーマーがない場合は直接体力にダメージを適用
                m_CurrentHealth -= amount;
                Debug.Log($"アーマーなし: 直接体力にダメージ適用。受けたダメージは: {amount}currenthealthは {m_CurrentHealth}");
            }

            // HPバーの更新
            SetHealthUI();

            // 体力が0以下になった場合の処理
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath();
            }
        }



        private void SetHealthUI ()
        {
            if (m_Slider200.value > 0) // アーマーのHPバーを更新
            {
                m_Slider200.value = Mathf.Clamp(m_Slider200.value, 0, m_Slider200.maxValue);
                m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_Slider200.value / m_Slider200.maxValue);
            }
            else
            {
                // Set the slider's value appropriately.
                m_Slider.value = m_CurrentHealth;

                // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
                m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
            }
        }


        private void OnDeath ()
        {
            // Set the flag so that this function is only called once.
            m_Dead = true;

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

        private void CheckItemUse()
        {
            Debug.Log("使用したアイテムがあるかチェックしてるよ");

            if (UseItemManager.useArmor)
            {
                // アーマーアイテムの効果を取得
                int armorEffect = jsonReader.GetEffectByName("戦車の装甲強化");

                if (armorEffect > 0)
                {
                    m_Slider200.maxValue = armorEffect;// 効果を適用
                    m_Slider200.value = m_Slider200.maxValue;
                    Debug.Log($"アーマーの効果を発動しました: 体力が {m_StartingHealth} に増加, 増加倍率: {armorEffect}");

                }
                else
                {
                    Debug.LogError("アーマーアイテムの効果が見つからないか、効果が 0 です。");
                }
            }
            else
            {
                Debug.Log("効果は発動してないよ");
            }
        }
    }
}