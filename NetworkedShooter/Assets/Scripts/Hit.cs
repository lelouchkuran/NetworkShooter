using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Hit : NetworkBehaviour {

    [SyncVar]
    private Color new_color;
   
    private NetworkIdentity objNetId;


    public GameObject expls;
    public GameObject center;

	// Use this for initialization
	void Start () {

        new_color = gameObject.GetComponent<Renderer>().material.color;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "bullet")
        {
            
            Instantiate(expls,col.gameObject.transform.position,gameObject.transform.rotation);
            Destroy(col.gameObject);
            new_color = new Color(Random.value, Random.value, Random.value, 1.0f);
            // apply it on current object's material
            CmdPaint(gameObject, new_color);

        }
    }

    [ClientRpc]
    void RpcPaint(GameObject obj, Color col)
    {
        Debug.Log("RPC ran");
        obj.GetComponent<Renderer>().material.color = col;        
    }


    [Command]
    void CmdPaint(GameObject obj, Color col)
    {
        objNetId = obj.GetComponent<NetworkIdentity>();       
        objNetId.AssignClientAuthority(connectionToClient);    
        RpcPaint(obj, col);                                   
        objNetId.RemoveClientAuthority(connectionToClient);    
    }
}
