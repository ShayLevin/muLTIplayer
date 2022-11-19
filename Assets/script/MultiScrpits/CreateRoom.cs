using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    public InputField roomName;
    public InputField maxPlayers;
    public Text msg;


    public void createRoomBtn()
    {
        if (string.IsNullOrEmpty(roomName.text))
        {
            msg.text = "please enter room name";
        }
        else if (string.IsNullOrEmpty(maxPlayers.text))
        {
            msg.text = "please enter max players";
        }
        else if (int.Parse(maxPlayers.text ) < 1 || int.Parse (maxPlayers.text) > 21)
        {
            msg.text = "please enter a number between 1 to 20";
        }
        else
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = (byte)int.Parse(maxPlayers.text);
            PhotonNetwork.CreateRoom(roomName.text, options);
            SceneManager.LoadScene("EnterRoom");
        }
    }
    public void backButton()
    {
        SceneManager.LoadScene("start");
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
