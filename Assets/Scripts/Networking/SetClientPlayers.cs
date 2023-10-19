using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SetClientPlayers : NetworkBehaviour
{
    [SerializeField] private GameObject guardPrefab;
    [SerializeField] private GameObject thiefPrefab;
    [SerializeField] private Transform thiefSpawnPoint;
    [SerializeField] private GameObject hostSelection;
    [SerializeField] private GameObject playerSelection;

    private NetworkVariable<bool> guardChosen = new NetworkVariable<bool>(false);

    public override void OnNetworkSpawn()
    {
        if (!guardChosen.Value)
        {
            playerSelection.SetActive(true);
        }
        else
        {
            SpawnAsThief();
        }
    }

    public void SpawnAsGuard()
    {
        if (guardChosen.Value == true)
        {
            SpawnAsThief();
        }
        else
        {
            SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 0);
            SetGuardChosenServerRpc(true);
        }
    }

    public void SpawnAsThief()
    {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId, 1);
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

    [ServerRpc(RequireOwnership = false)]
    public void SetGuardChosenServerRpc(bool chosen)
    {
        guardChosen.Value = chosen;
    }

    public void SpawnThiefLocal()
    {
        Instantiate(thiefPrefab, thiefSpawnPoint);
    }
}
