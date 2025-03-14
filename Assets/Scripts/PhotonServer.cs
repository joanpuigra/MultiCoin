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

    private void Start()
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
        // _gemBehavior.InstantiateGem();
    }

    // private void Update()
    // {
    //     if (!photonView.IsMine)
    //     {
    //         transform.position = playerGirl.transform.position;
    //         transform.rotation = playerGirl.transform.rotation;
    //     }
    // }

    private static void InstantiatePlayer()
    {
        if (!PhotonNetwork.IsConnected) return;
        var prefabName = PhotonNetwork.PlayerList.Length == 1 ? "PlayerGirl" : "PlayerBoy";
        var prefabToSpawn = Resources.Load<GameObject>(prefabName);

        if (prefabToSpawn is not null)
        {
            PhotonNetwork.Instantiate(
                prefabToSpawn.name,
                prefabToSpawn.transform.position,
                prefabToSpawn.transform.rotation
            );
        }
        else { Debug.LogWarning($"Prefab {prefabName} not found in Resources"); }
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
