using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MultiPlayerScript : MonoBehaviourPunCallbacks
{
    public InputField PlayerName;
    public Text msg;
    public GameObject ENterPanel;
    public GameObject StatusPanel;
    public GameObject LObbyPanel;
    public GameObject SettingsPanel;
    public Text LobbyPanelMSG;
    int num = 0;
    public Animator anim;
    public GameObject settings;
    public GameObject joinGameB;
    public GameObject settingsUi;


    //button Voids

    public void SettingsClick()
    {
        StatusPanel.SetActive(false);
        ENterPanel.SetActive(true);
        LObbyPanel.SetActive(false);
        anim.SetInteger("settings", 1);
        settings.SetActive(false);
        joinGameB.SetActive(false);
        StartCoroutine(waitForSecondss());
    }
    IEnumerator waitForSecondss()
    {
        yield return new WaitForSeconds(1);
        settingsUi.SetActive(true);
      
    }
    public void JoinTRoomButton()
    {
         PhotonNetwork.JoinRandomRoom();
    }
    public void btnCreateRoom()
    {
        SceneManager.LoadScene("CreateRoom");
    }
    public void btnShowRoomList()
    {
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("showroom");
    }
    void Start()
    {
        if(PhotonNetwork.IsConnected)
        {
            ENterPanel.SetActive(false);
        }
        else
        {
            StatusPanel.SetActive(false);
            ENterPanel.SetActive(true);
            LObbyPanel.SetActive(false);
            SettingsPanel.SetActive(false);
        }
    }
    void Update()
    { 
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                LObbyPanel.SetActive(false);
                StatusPanel.SetActive(false);
                ENterPanel.SetActive(true);
                settingsUi.SetActive(false);
                anim.SetInteger("settings", 2);
                StartCoroutine(waitForSecondsss());
            }
    }
    IEnumerator waitForSecondsss()
    {
        yield return new WaitForSeconds(1.8f);
        settings.SetActive(true);   
        joinGameB.SetActive(true);
        anim.SetInteger("settings", 0);
    }
    //ISconnect
    public override void OnConnectedToMaster()
    {
        print(PhotonNetwork.NickName + " Connected");
       LobbyPanelMSG.text = PhotonNetwork.NickName + " Connected";
        StatusPanel.SetActive(false);
        ENterPanel.SetActive(false);
        LObbyPanel.SetActive(true);
    }
    public void ConnectToGame(){

        if(string.IsNullOrEmpty(PlayerName.text))
        {
            msg.text = "please enter a player name";
            
        }
        else
        {
            PhotonNetwork.NickName = PlayerName.text;
            if(!PhotonNetwork.IsConnected)
            {
                ENterPanel.SetActive(true);
                StatusPanel.SetActive(true);
                PhotonNetwork.ConnectUsingSettings();
                
            }
        }

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        LobbyPanelMSG.text = "There are no available rooms , please create one";

    }
    public void createAndJoinRoom()
    {
        string roomName = "Room " + num;
        num++;

        RoomOptions options = new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(roomName, options);
    }
    public override void OnJoinedRoom()
    {
        print(PhotonNetwork.NickName + " Join room: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("EnterRoom");
    }
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
}


    
   


