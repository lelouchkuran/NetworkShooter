using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfterTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(KillThis());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator KillThis()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

}
