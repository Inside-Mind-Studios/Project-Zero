using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    public Camera standbyCam;

	// Use this for initialization
	void Start ()
    {
        Connect();
	}

    void Connect()
    {
        PhotonNetwork.ConnectUsingSettings("v0.0.0.0.1"); //Connect with Sandbox Settings
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        foreach (RoomInfo game in PhotonNetwork.GetRoomList())
        {
            GUILayout.Label(game.name + " " + game.playerCount + "/" + game.maxPlayers);
        }
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinRandomRoom();
    }


    void OnJoinedRoom()
    {
        Debug.Log("<color=green>Successful connection to Room established</color>");
        SpawnPlayer();
    }

    void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("<color=red>Error: " + codeAndMsg[0] + ", " + codeAndMsg[1] +"</color>");
        PhotonNetwork.CreateRoom(null);
    }

    void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("FPSPlayer", new Vector3(-75, 14, 0), Quaternion.identity, 0);
        standbyCam.enabled = false;
    }

}
