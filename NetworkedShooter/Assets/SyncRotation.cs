using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncRotation : NetworkBehaviour {

    [SyncVar]
    private Quaternion net_player_rot;
    [SyncVar]
    private Quaternion net_cam_rot;

    [SerializeField]
    private Transform player_transform;
    [SerializeField]
    private Transform cam_transform;
    [SerializeField]
    private float lerpRot = 15;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        TransmitRot();
        LerpNetRot();


	}

    void LerpNetRot()
    {
        if (isLocalPlayer)
        {
            player_transform.rotation = Quaternion.Lerp(player_transform.rotation, net_player_rot, Time.deltaTime * lerpRot);
            cam_transform.rotation = Quaternion.Lerp(cam_transform.rotation, net_cam_rot, Time.deltaTime * lerpRot);

        }
    }

    [Command]
    void CmdSendRot2Server(Quaternion playerRot, Quaternion camRot)
    {
        net_player_rot = playerRot;
        net_cam_rot = camRot;
    }

    [ClientCallback]
    void TransmitRot()
    {
        if (isLocalPlayer)
        {
            CmdSendRot2Server(player_transform.rotation, cam_transform.rotation);
        }
    }

}
