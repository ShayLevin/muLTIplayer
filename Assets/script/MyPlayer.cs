using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class MyPlayer : MonoBehaviourPunCallbacks
{
    public Text playerName;
    public Camera cam;
    public float health;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<MoveCapsule>().enabled = true;
            cam.GetComponent<Camera>().enabled = true;


        }
        else
        {

            transform.GetComponent<MoveCapsule>().enabled = false;
            cam.GetComponent<Camera>().enabled = false;

        }
        playerUI();

        GameObject[] bullet;
        bullet = GameObject.FindGameObjectsWithTag("bullet");

        foreach (GameObject b in bullet)
        {
            Destroy(b);

        }
        health = 1;
        healthBar.fillAmount = health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "bullet")
        {
            this.GetComponent<PhotonView>().RPC("hitPlayer", RpcTarget.AllBuffered , 0.1f);


        }
    }
    [PunRPC]
    public void hitPlayer(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health;

        if(healthBar.fillAmount <= 0)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.LeaveRoom();
            }
        }
       

    }
    //NameCanvas & ....
    void playerUI()
    {
        if (playerName != null)
        {
            playerName.text = photonView.Owner.NickName;

        }


    }
   








    // Update is called once per frame
    void Update()
    {
        
    }
}
