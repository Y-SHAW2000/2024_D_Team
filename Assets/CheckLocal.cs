using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CheckLocal : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    private void Check()
    {
        if(!photonView.IsMine)
        {
                   transform.Find("Camera_for_TPS").gameObject.SetActive(false);
        }
    }
}
