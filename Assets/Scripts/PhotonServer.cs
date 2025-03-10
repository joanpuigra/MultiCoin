using UnityEngine;
using Photon.Pun;
using TMPro;

public class PhotonServer : MonoBehaviourPunCallbacks
{
    public GameObject playerGirlPrefab;
    public GameObject playerBoyPrefab;
    public Transform playerSpawn;

    public TextMeshProUGUI greenDText;
    public TextMeshProUGUI redDText;

    private void Start()
    {
        ConnectToServer();
    }

    private static void ConnectToServer()
    {
        Debug.Log("Connecting to server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        PhotonNetwork.JoinRoom("DefaultRoom");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        // InstantiatePlayer();
    }

    private void InstantiatePlayer()
    {
        var playerGirl = PhotonNetwork.Instantiate(
            "PlayerGirl",
            playerGirlPrefab.transform.position,
            playerGirlPrefab.transform.rotation
        );
    }
}
