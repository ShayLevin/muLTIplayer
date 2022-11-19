using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class game : MonoBehaviourPunCallbacks
{
    public Text PlayerCountT;
    public GameObject player;
    public static game gameScript;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCountT.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount + " / 20";
        if (PhotonNetwork.IsConnected && player != null)
        {
            int x = Random.Range(115, 276);
            int z = Random.Range(114, 246);
            PhotonNetwork.Instantiate(player.name,new Vector3(x,205,z), Quaternion.identity);

        }
        
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("start");
    }
    private void Awake()
    {
        if (gameScript != null)
        {
            Destroy(this.gameObject);

        }
        else
        {
            gameScript = this;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountT.text = "Players: " + PhotonNetwork.CurrentRoom.PlayerCount + " / 20";

    }
}
