using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class SyncMove : NetworkBehaviour {

    [SyncVar]
    private Vector3 pos_sync;

    [SerializeField]
    Transform player_t;

    [SerializeField]
    float lerp_rate = 15;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        TransmitPos();
        LerpPos();

	}

    void LerpPos()
    {
        if (isLocalPlayer)
        {
            player_t.position = Vector3.Lerp(player_t.position, pos_sync, Time.deltaTime * lerp_rate);
        }
    }

    [Command]
    void CmdPosition2Server(Vector3 pos)
    {
        pos_sync = pos;
    }

    [ClientCallback]
    void TransmitPos()
    {
        if (isLocalPlayer)
        {

            CmdPosition2Server(player_t.position);
        }
    }
}
