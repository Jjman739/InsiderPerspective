using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SetClientPlayers : NetworkBehaviour
{
    [SerializeField] private GameObject guardPrefab;
    [SerializeField] private GameObject thiefPrefab;
    [SerializeField] private Transform thiefSpawnPoint;
    public override void OnNetworkSpawn()
    {
        if (StartNetwork.Instance.PlayerType == "Guard")
        {
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 0);
        }
        else
        {
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 1);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        GameObject newPlayer;
        if (prefabId == 0)
            newPlayer = Instantiate(guardPrefab);
        else
            newPlayer = Instantiate(thiefPrefab, thiefSpawnPoint);

        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
