using Unity.Netcode;
using UnityEngine;

public class StartNetwork : MonoBehaviour
{
    [SerializeField] private GameObject parameterMenu;

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        parameterMenu.SetActive(true);
        parameterMenu.GetComponent<ParameterMenu>().Initialize();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        parameterMenu.SetActive(true);
        parameterMenu.GetComponent<ParameterMenu>().Initialize();
    }

    public void StartLocal()
    {
        
    }
}
