using Photon.Pun;
using TMPro;
using UnityEngine;

public class GemScore : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshProUGUI textGem;

    [PunRPC]
    private void AddScore(int score)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        textGem.text = score.ToString();
    }
}
