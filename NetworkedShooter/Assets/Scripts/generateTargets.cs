using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class generateTargets : NetworkBehaviour {

    public GameObject target;
    public GameObject[] targets;

    [SyncVar]
    private int num_targ;


    // Use this for initialization
    void Start () {

        GenerateTargetCount();

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    void GenerateTargetCount()
    {
        if (isServer)
        {
            num_targ = Random.Range(1, 16);
            CmdTargetCount(num_targ);
        }
    }


    [Command]
    void CmdTargetCount(int targ)
    {
        num_targ = targ;
        for (int i = 0; i < num_targ; i++)
        {
            GameObject t = Instantiate(target, targets[i].transform.position, targets[i].transform.rotation);
            NetworkServer.Spawn(t);
        }
    }
}
