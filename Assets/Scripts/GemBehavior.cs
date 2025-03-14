using Photon.Pun;
using UnityEngine;

public class GemBehavior : MonoBehaviourPun
{
   private int _playerScore;
  private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (!collision.gameObject.CompareTag("Player")) return;
        
        _playerScore++;
        // photonView.RPC("AddScore", RpcTarget.AllBuffered, _playerScore);

        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    // public void InstantiateGem()
    // {
    //     if (gemPrefab is not null && PhotonNetwork.IsConnected)
    //     {
    //         var gem = PhotonNetwork.Instantiate(
    //             gemPrefab.name,
    //             gemPrefab.transform.position,
    //             gemPrefab.transform.rotation
    //         );
    //     }
    //     else { Debug.LogWarning("Gem prefab is not set"); }
    // }

    // [PunRPC]
    // private void AddScore(int score)
    // {
    //     _playerScore = score;
    //     gemText.GetComponent<TMPro.TextMeshProUGUI>().text = _playerScore.ToString();
    // }
}
