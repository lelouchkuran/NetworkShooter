using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {


    [SerializeField] Camera FPSCamera;
    [SerializeField] AudioListener FPSAudioLis;


    // Use this for initialization
    void Start () {

        if (isLocalPlayer)
        {
            GameObject.Find("_Camera_").SetActive(false);
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCamera.enabled = true;
            FPSAudioLis.enabled = true;
        }

	}

	// Update is called once per frame
	void Update () {



	}




}
