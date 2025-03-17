using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class GemSpawner : MonoBehaviourPunCallbacks
{
   [SerializeField]
   private GameObject gemPrefab;
   [SerializeField]
   private int gemCount = 10;
   [SerializeField]
   private Vector2 areaSize = new(10f, 5f);
   [SerializeField]
   private float gemTimer = 5f;

   private readonly List<GameObject> _gems = new();

   public void Start()
   {
      if (PhotonNetwork.IsMasterClient)
      {
         InvokeRepeating("SpawnLoop", 1f, gemTimer);
      }
   }

   private void SpawnLoop()
   {
      if (photonView == null || !photonView.IsMine) return;

      try
      {
         CleanGems();
         SpawnGems();
      }
      catch (System.Exception ex)
      {
         Debug.LogError($"[GemSpawner] Error in SpawnLoop: {ex.Message}");
      }
   }

   private void SpawnGems()
   {
      if (gemPrefab == null) return;

      Vector2 parentPosition = transform.position;

      for (var i = 0; i < gemCount; i++)
      {
         Vector2 spawnPosition;
         bool positionIsValid;

         do
         {
            spawnPosition = new Vector2(
               Random.Range(-areaSize.x, areaSize.x),
               Random.Range(-areaSize.y, areaSize.y)
            ) + parentPosition;

            positionIsValid = !Physics2D.OverlapCircle(
               spawnPosition,
               0.5f
            );
         } while (!positionIsValid);

         var newGem = PhotonNetwork.InstantiateRoomObject(
            gemPrefab.name,
            spawnPosition,
            Quaternion.identity
         );

         if (newGem != null)
         {
            _gems.Add(newGem);
            // newGem.transform.SetParent(transform);
         }
         else
         {
            Debug.LogError("[GemSpawner] Failed to instantiate gem");
         }
      }
   }

   private void CleanGems()
   {
      for (var i = _gems.Count - 1; i >= 0; i--)
      {
         var gem = _gems[i];

         if (gem == null)
         {
            _gems.RemoveAt(i);
            continue;
         }

         var gemPhotonView = gem.GetComponent<PhotonView>();
         if (gemPhotonView != null && gemPhotonView.IsMine)
         {
            PhotonNetwork.Destroy(gem);
         }
         _gems.RemoveAt(i);
      }
   }
}
