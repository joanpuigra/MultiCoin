using System;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonServer : MonoBehaviourPunCallbacks
{
    [Header("Prefabs")]
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject gemGreenPrefab;
    [SerializeField]
    private GameObject gemRedPrefab;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI greenDText;
    [SerializeField]
    private TextMeshProUGUI redDText;

    private GemBehavior _gemBehavior;

    private void Awake()
    {
        _gemBehavior = gameObject.GetComponent<GemBehavior>();
    }

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
        _gemBehavior.InstantiateGem();
    }

    // private void Update()
    // {
    //     if (!photonView.IsMine)
    //     {
    //         transform.position = playerPrefab.transform.position;
    //         transform.rotation = playerPrefab.transform.rotation;
    //     }
    // }

    private void InstantiatePlayer()
    {
        if (playerPrefab is not null && PhotonNetwork.IsConnected)
        {
            var player = PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerPrefab.transform.position,
                playerPrefab.transform.rotation
            );
        }
        else { Debug.LogWarning("Player prefab is not set"); }
    }

    // private void InstantiateGem()
    // {
    //     if (gemGreenPrefab is not null && PhotonNetwork.IsConnected)
    //     {
    //         var gem = PhotonNetwork.Instantiate(
    //             gemGreenPrefab.name,
    //             gemGreenPrefab.transform.position,
    //             gemGreenPrefab.transform.rotation
    //         );
    //     }
    //     else { Debug.LogWarning("Gem prefab is not set"); }
    // }
}
