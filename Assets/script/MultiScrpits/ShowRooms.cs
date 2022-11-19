using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ShowRooms : MonoBehaviourPunCallbacks
{
    public GameObject roomInfoPrefab;
    public GameObject content;

    private Dictionary<string, RoomInfo> RoomListData;
    private Dictionary<string, GameObject> RoomListGameObject;


    void clearRoomList()
    {
        foreach (var RoomListGameObject in RoomListGameObject.Values)
        {
            Destroy(RoomListGameObject);
        }
        RoomListGameObject.Clear();

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        clearRoomList();


        foreach (RoomInfo room in roomList)
        {
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (RoomListData.ContainsKey(room.Name))
                {
                    RoomListData.Remove(room.Name);
                }
            }
            else
            {
                if (RoomListData.ContainsKey(room.Name))
                {
                    RoomListData[room.Name] = room;
                }
                else
                {
                    RoomListData.Add(room.Name, room);
                }

            }

        }
        foreach (RoomInfo room in RoomListData.Values)
        {
            GameObject obj = Instantiate(roomInfoPrefab);
            obj.transform.SetParent(content.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.Find("RoomName").GetComponent<Text>().text = room.Name;
            obj.transform.Find("players").GetComponent<Text>().text = room.PlayerCount + "/" + room.MaxPlayers;

            Button btn = obj.transform.Find("join").GetComponent<Button>();
            btn.onClick.AddListener(() => joinRoom(room.Name));

            RoomListGameObject.Add(room.Name, obj);
        }
        

    }
    public void joinRoom(string roomName)
    {
        if(PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();

        }
        PhotonNetwork.JoinRoom(roomName);
        SceneManager.LoadScene("EnterRoom");

    }

    public void backB()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        SceneManager.LoadScene("start");


    }
    public override void OnLeftLobby()
    {
        clearRoomList();
        RoomListData.Clear();
    }


    void Start()
    {
        RoomListData = new Dictionary<string, RoomInfo>();
        RoomListGameObject = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
