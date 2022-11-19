using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class EnterRoom : MonoBehaviourPunCallbacks
{
    public GameObject playerInfo;
    public GameObject content;
    public Text title;
    private Dictionary<int, GameObject> playerInfoList;
    public GameObject startB;
    // Start is called before the first frame update

    public void startBClick()
    {
        SceneManager.LoadScene("game");

    }
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateTitle()
    {

        title.text = PhotonNetwork.CurrentRoom.Name + " " +
        PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;
    }
    public override void OnCreatedRoom()
    {
        UpdateTitle();

    }
    public override void OnJoinedRoom()
    {
        UpdateTitle();

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startB.SetActive(true);

        }
        else
        {
            startB.SetActive(false);

        }

        if (playerInfoList == null)
        {
            playerInfoList = new Dictionary<int, GameObject>();
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject obj = Instantiate(playerInfo);
            obj.transform.SetParent(content.transform);
            obj.transform.localScale = Vector3.one;

            obj.transform.Find("playername").GetComponent<Text>().text = player.NickName;
            playerInfoList.Add(player.ActorNumber, obj);
            if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                obj.transform.Find("playername").GetComponent<Text>().color = Color.green;

            }
        }

    }
  

    public override void OnPlayerEnteredRoom(Player player)
    {
        UpdateTitle();

         title.text =  PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers;

        GameObject obj = Instantiate(playerInfo);
        obj.transform.SetParent(content.transform);
        obj.transform.localScale = Vector3.one;
        obj.transform.Find("playername").GetComponent<Text>().text = player.NickName;
        playerInfoList.Add(player.ActorNumber, obj);

      

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startB.SetActive(true);

        }

        UpdateTitle();
        Destroy(playerInfoList[otherPlayer.ActorNumber].gameObject);
        playerInfoList.Remove(otherPlayer.ActorNumber);

    }
    public override void OnLeftRoom()
    {
       foreach (GameObject playerInfoList in playerInfoList.Values)
       {
            Destroy(playerInfoList);
       }
       playerInfoList.Clear();
       playerInfoList = null;

        SceneManager.LoadScene("start");

    }
    public void LeaveB()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}




