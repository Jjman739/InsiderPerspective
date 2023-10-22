using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class StartNetwork : MonoBehaviour
{
    public static StartNetwork Instance { get; private set; }
    public string PlayerType;
    [SerializeField] private GameObject parameterMenu;

    private void Start()
    {
        if (Instance != null)
            return;
        
        Instance = this;
    }

    public async Task<string> CreateRelay(string type)
    {
        try 
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            PlayerType = type;

            NetworkManager.Singleton.StartHost();

            parameterMenu.SetActive(true);
            parameterMenu.GetComponent<ParameterMenu>().Initialize();

            return joinCode;
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    public async void JoinRelay(string joinCode, string type)
    {
        try
        {
            Debug.Log("Joining Relay with " + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
            
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            PlayerType = type;

            NetworkManager.Singleton.StartClient();
            
            parameterMenu.SetActive(true);
            parameterMenu.GetComponent<ParameterMenu>().Initialize();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
        }
    }
}
