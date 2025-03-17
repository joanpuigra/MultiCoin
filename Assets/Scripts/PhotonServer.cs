using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonServer : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GemSpawner gemSpawner;
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
        gemSpawner.Start();
    }

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
}
