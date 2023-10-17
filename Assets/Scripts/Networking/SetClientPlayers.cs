using Unity.Netcode;
using UnityEngine;

public class SetClientPlayers : NetworkBehaviour
{
    [SerializeField] private GameObject guardPrefab;
    [SerializeField] private GameObject thiefPrefab;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 0);
        else
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 1);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ulong clientId, int prefabId)
    {
        GameObject newPlayer;
        if (prefabId == 0)
            newPlayer = Instantiate(guardPrefab);
        else
            newPlayer = Instantiate(thiefPrefab);

        NetworkObject netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId, true);
    }
}
