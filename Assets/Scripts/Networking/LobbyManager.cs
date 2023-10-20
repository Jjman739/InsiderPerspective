using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    private Lobby hostLobby;
    private Lobby joinedLobby;
    private string playerName;
    private string playerType;
    private float heartBeatTimer;
    private float lobbyUpdateTimer;
    private const string KEY_START_GAME = "StartGame";
    private const string KEY_PLAYER_THIEF = "Thief";
    private const string KEY_PLAYER_GUARD = "Guard";
    [SerializeField] private float heartBeatTimerMax = 15;
    [SerializeField] private float lobbyUpdateTimerMax = 2f;
    [SerializeField] private TMP_InputField roomCodeInput;
    [SerializeField] private GameObject hostSelection;
    [SerializeField] private GameObject playerSelection;
    [SerializeField] private GameObject background;
    [SerializeField] private TextMeshProUGUI roomCodeText;
    [SerializeField] private TextMeshProUGUI playerListText;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in " + AuthenticationService.Instance.PlayerId);
        };

        #if UNITY_EDITOR
            AuthenticationService.Instance.ClearSessionToken();
        #endif

        await AuthenticationService.Instance.SignInAnonymouslyAsync(); 

        playerName = "Player" + Random.Range(10,99);

        Debug.Log(playerName);
    }

    private void Update()
    {
        HandleLobbyHeartbeat();
        HandleLobbyPollForUpdates();
    }

    private void HandleLobbyHeartbeat()
    {
        if (hostLobby != null)
        {
            heartBeatTimer -= Time.deltaTime;
            if (heartBeatTimer < 0f)
            {
                heartBeatTimer = heartBeatTimerMax;

                LobbyService.Instance.SendHeartbeatPingAsync(hostLobby.Id);
            }
        }
    }

    private async void HandleLobbyPollForUpdates()
    {
        if (joinedLobby != null)
        {
            lobbyUpdateTimer -= Time.deltaTime;
            if (lobbyUpdateTimer < 0f)
            {
                lobbyUpdateTimer = lobbyUpdateTimerMax;

                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(joinedLobby.Id);
                joinedLobby = lobby;

                playerListText.text = PrintPlayers();
            }

            if (joinedLobby.Data[KEY_START_GAME].Value != "0")
            {
                if (!IsLobbyHost())
                {
                    playerSelection.SetActive(false);
                    background.SetActive(false);
                    StartNetwork.Instance.JoinRelay(joinedLobby.Data[KEY_START_GAME].Value, playerType);
                }

                joinedLobby = null;
            }
        }
    }

    public async void CreateLobby()
    {
        try
        {
            string lobbyName = "MyLobby";
            int maxPlayers = 4;
            CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions()
            {
                IsPrivate = false,
                Player = GetPlayerObject(KEY_PLAYER_GUARD),
                Data = new Dictionary<string, DataObject>
                {
                    { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, "0") }
                }
            };

            Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(lobbyName, maxPlayers, createLobbyOptions);

            hostLobby = lobby;
            joinedLobby = hostLobby;

            hostSelection.SetActive(false);
            playerSelection.SetActive(true);

            roomCodeText.text = lobby.LobbyCode;
            playerListText.text = PrintPlayers();

            Debug.Log($"Created Lobby! {lobby.LobbyCode}");
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    public async void ListLobbies()
    {
        try
        {
            QueryLobbiesOptions queryLobbiesOptions = new QueryLobbiesOptions
            {
                Count = 25,
                Filters = new List<QueryFilter>
                {
                    new QueryFilter(QueryFilter.FieldOptions.AvailableSlots, "0", QueryFilter.OpOptions.GT)
                },
                Order = new List<QueryOrder>
                {
                    new QueryOrder(false, QueryOrder.FieldOptions.Created)
                }
            };

            QueryResponse queryResponse = await Lobbies.Instance.QueryLobbiesAsync();

            Debug.Log("Lobbies found: " + queryResponse.Results.Count);
            foreach (Lobby lobby in queryResponse.Results)
            {
                Debug.Log(lobby.Name + " " + lobby.MaxPlayers + " " + lobby.Data["GameMode"].Value);
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    public void JoinLobby()
    {
        string lobbyCode = roomCodeInput.text;
        joinLobbyByCode(lobbyCode);
    }

    private async void joinLobbyByCode(string lobbyCode)
    {
        try
        {
            JoinLobbyByCodeOptions joinLobbyByCodeOptions = new JoinLobbyByCodeOptions
            {
                Player = GetPlayerObject(KEY_PLAYER_THIEF)
            };

            Lobby lobby = await Lobbies.Instance.JoinLobbyByCodeAsync(lobbyCode, joinLobbyByCodeOptions);
            joinedLobby = lobby;

            hostSelection.SetActive(false);
            playerSelection.SetActive(true);

            roomCodeText.text = lobby.LobbyCode;
            playerListText.text = PrintPlayers();

            Debug.Log("Joined lobby with code " + lobbyCode);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    private Player GetPlayerObject(string playerType)
    {
        return new Player
        {
            Data = new Dictionary<string, PlayerDataObject>
            {
                { "PlayerName", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerName) },
                { "PlayerType", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerType) }
            }
        };
    }

    public async void QuickJoinLobby()
    {
        try
        {
            await LobbyService.Instance.QuickJoinLobbyAsync();
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    public string PrintPlayers()
    {
        return PrintPlayers(joinedLobby);
    }

    public string PrintPlayers(Lobby lobby)
    {
        string sb = "";
        foreach (Player player in lobby.Players)
        {
            sb += $"{player.Data["PlayerName"].Value}\n";
        }
        return sb;
    }

    public void SetPlayerAsGuard()
    {
        UpdatePlayerType(KEY_PLAYER_GUARD);
    }

    public void SetPlayerAsThief()
    {
        UpdatePlayerType(KEY_PLAYER_THIEF);
    }

    public async void UpdatePlayerType(string newPlayerType)
    {
        try
        {
            playerType = newPlayerType;
            await LobbyService.Instance.UpdatePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId, new UpdatePlayerOptions
            {
                Data = new Dictionary<string, PlayerDataObject>
                {
                    { "PlayerType", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, playerType) }
                }
            });
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    public async void LeaveLobby()
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, AuthenticationService.Instance.PlayerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    public async void KickPlayer(string playerId)
    {
        try
        {
            await LobbyService.Instance.RemovePlayerAsync(joinedLobby.Id, playerId);
        }
        catch (LobbyServiceException e)
        {
            Debug.LogError(e);
        }
    }

    private bool IsLobbyHost()
    {
        return AuthenticationService.Instance.PlayerId == joinedLobby.HostId;
    }

    private Player GetPlayerById(string id)
    {
        foreach (Player player in joinedLobby.Players)
        {
            if (player.Id == id) return player;
        }

        return null;
    }

    public async void StartGame()
    {
        if (IsLobbyHost())
        {
            try
            {
                Debug.Log("StartGame");

                string relayCode = await StartNetwork.Instance.CreateRelay(playerType);
                
                Debug.Log("Successfully joined relay");

                Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(joinedLobby.Id, new UpdateLobbyOptions
                {
                    Data = new Dictionary<string, DataObject>
                    {
                        { KEY_START_GAME, new DataObject(DataObject.VisibilityOptions.Member, relayCode) }
                    }
                });

                playerSelection.SetActive(false);   
                background.SetActive(false);

                joinedLobby = lobby;
            }
            catch (LobbyServiceException e)
            {
                Debug.LogError(e);
            }
        }
    }
}
