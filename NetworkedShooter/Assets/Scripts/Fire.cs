using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fire : NetworkBehaviour
{

    public GameObject bullet;
    public int speed;
    public Transform pos;
    [SyncVar]
    private Color randomColor;


    [SerializeField]
    public Camera FPSCamera;

    [SerializeField]
    public AudioListener FPSAudio;


    // Use this for initialization
    void Start()
    {

        if (isLocalPlayer)
        {
            FPSCamera.enabled = true;
            FPSAudio.enabled = true;
            randomColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            GetComponentInChildren<Renderer>().material.color = randomColor;
        }
    }

    // Update is called once per frame

    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetMouseButtonDown(0))
        {

            CmdFire();

        }


    }


    [Command]
    void CmdFire()
    {
        GameObject bulletI = Instantiate(bullet, pos.position, pos.rotation);

        bulletI.GetComponent<Rigidbody>().velocity = pos.transform.forward * 30;

        NetworkServer.Spawn(bulletI);

    }
    [Command]
    void Cmd_ProvideColorToServer(Color c)
    {

        randomColor = c;
    }

    [ClientCallback]
    void TransmitColor()
    {
        if (isLocalPlayer)
        {
            Cmd_ProvideColorToServer(randomColor);
        }
    }

    public override void OnStartClient()
    {
        StartCoroutine(UpdateColor(1.5f));

    }

    IEnumerator UpdateColor(float time)
    {

        float timer = time;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            TransmitColor();
            if (!isLocalPlayer)
                GetComponentInChildren<Renderer>().material.color = randomColor;


            yield return null;
        }


    }
}