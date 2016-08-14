using UnityEngine;
using System.Collections;

namespace EntroMinds.NetworkScripts
{
    public class NetworkManager : MonoBehaviour
    {

        public GameObject standbyCam;
        private Spawn[] spawnspots;

        // Use this for initialization
        void Start()
        {
            spawnspots = GameObject.FindObjectsOfType<Spawn>(); //Find all spawnspots once at level load
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
            Debug.Log("<color=red>Error: " + codeAndMsg[0] + ", " + codeAndMsg[1] + "</color>");
            PhotonNetwork.CreateRoom(null);
        }

        void SpawnPlayer()
        {
            Spawn newSpawn = spawnspots[Random.Range(0, spawnspots.Length)];

            GameObject newPlayer = PhotonNetwork.Instantiate(
                "FirstPersonPlayerController",
                newSpawn.transform.position,
                newSpawn.transform.rotation,
                0
            );
            standbyCam.SetActive(false);

            // Only activate the local player's assets
            newPlayer.GetComponent<EntroMinds.Characters.FirstPerson.FirstPersonPlayer>().enabled = true;
            newPlayer.GetComponent<CharacterController>().enabled = true;
            newPlayer.GetComponentInChildren<Camera>().enabled = true;
            newPlayer.GetComponentInChildren<AudioListener>().enabled = true;
            newPlayer.GetComponentInChildren<FlareLayer>().enabled = true;
        }
    }
}