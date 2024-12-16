using Complete;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUpdate : MonoBehaviour
{
    public Slider HPBar;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        HPBar.maxValue = player.GetComponent<TankHealthForRaid>().m_StartingHealth;
    }
    void Update()
    {
        HPBar.value = player.GetComponent<TankHealthForRaid>().m_CurrentHealth;
    }
}
