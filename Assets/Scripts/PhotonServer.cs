using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonServer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    public TextMeshProUGUI greenDText;
    public TextMeshProUGUI redDText;

    private void Start()
    {
        ConnectToServer();
    }

    private static void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        PhotonNetwork.JoinOrCreateRoom(
            "DefaultRoom",
            new RoomOptions { MaxPlayers = 2 },
            TypedLobby.Default
        );
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Failed to join room: " + message);
        PhotonNetwork.CreateRoom(
            "DefaultRoom",
            new RoomOptions { MaxPlayers = 2 },
            TypedLobby.Default
        );
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        if (playerPrefab is not null)
        {
            var player = PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerPrefab.transform.position,
                playerPrefab.transform.rotation
            );

        }
        else if (playerPrefab)
        {
            playerPrefab = Resources.Load<GameObject>("PlayerBoy");

            var player = PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerPrefab.transform.position,
                playerPrefab.transform.rotation
            );
        }
        else { Debug.LogWarning("Player prefab is not set"); }
    }
}
