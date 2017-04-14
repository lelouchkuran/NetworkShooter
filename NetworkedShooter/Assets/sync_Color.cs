using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class sync_Color : NetworkBehaviour {

    [SyncVar]
    private Color new_color;

    [SerializeField]
    private GameObject color;

    private Color update_color;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        TransmitColor();

    

    }

    [Command]
    void CmdSendColor2Server(Color c)
    {

        new_color = c;
    }

    [ClientCallback]
    void TransmitColor()
    {
        if (isLocalPlayer)
        {
            CmdSendColor2Server(new_color);
        }
    }
}
