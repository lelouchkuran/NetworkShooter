using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowup : MonoBehaviour {

    public GameObject explosion;
    public GameObject barrel;
    public int life;


	// Use this for initialization
	void Start () {
       
        StartCoroutine(End());
    }
	
	// Update is called once per frame
	void Update () {
 

	}

    IEnumerator End()
    {
        yield return new WaitForSeconds(life);
        Explode();
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
