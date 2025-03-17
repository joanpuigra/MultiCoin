using Photon.Pun;
using TMPro;
using UnityEngine;

public class GemBehavior : MonoBehaviourPun
{
    private static int _playerScore;
    private TextMeshProUGUI _textGem;

    private void Start()
    {
        _textGem = GameObject.FindWithTag("TextGem").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (!photonView.IsMine) return;

        if (!target.gameObject.CompareTag("Player")) return;

        _playerScore++;
        photonView.RPC("AddScore", RpcTarget.AllBuffered, _playerScore);
}

    [PunRPC]
    private void AddScore(int newScore)
    {
        _textGem.text = newScore.ToString();
        if (gameObject != null)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
