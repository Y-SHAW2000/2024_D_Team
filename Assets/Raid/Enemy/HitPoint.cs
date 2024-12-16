using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public float damage;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerControllerForAgent>().PlayerDamaged(damage);
        }
    }
}
