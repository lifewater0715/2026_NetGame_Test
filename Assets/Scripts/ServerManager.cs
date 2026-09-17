using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

using Unity.Services.Multiplayer;
using System.Runtime.InteropServices.WindowsRuntime;


public class ServerManager : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private TMP_Text serverStateUI;

    private ISession currentSession;
    [SerializeField] private string INVTCODE;
    public string JoinCode;

    //[SerializeField] private char serverState = 'N';
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Server_S_UI('N');
        LoginUnityRplay();
        networkManager = gameObject.GetComponent<NetworkManager>();
    }

    public void HostStart()
    {
        Server_S_UI('H');
        networkManager.StartHost();
    }
    public void ServerStart()
    {
        Server_S_UI('S');
        networkManager.StartServer();
    }
    public void ClientStart()
    {
        if (INVTCODE != null)
        {
            JoinGame(INVTCODE);
            return;
        }
        Server_S_UI('C');
        networkManager.StartClient();
    }

    [Rpc(SendTo.Server)]
    private void Server_S_UI(char serverStateText)
    {
        string Text = "Current State : <" + serverStateText + ">";
        serverStateUI.text = Text;
    }

    private async Task LoginUnityRplay()
    {
        try
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            Debug.Log(
                $"로그인 완료 : {AuthenticationService.Instance.PlayerId}"
            );
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    [ContextMenu("HostUnityRplay")]
    public async Task CreateHost()
    {
        try
        {
            SessionOptions options = new SessionOptions
            {
                MaxPlayers = 4
            }
            .WithRelayNetwork();

            currentSession =
                await MultiplayerService.Instance
                    .CreateSessionAsync(options);

            JoinCode = currentSession.Code;

            Debug.Log($"Relay Host 생성 완료");
            Debug.Log($"Join Code : {JoinCode}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    [ContextMenu("JoinUnityRplay")]
    public async Task JoinGame(string joinCode)
    {
        try
        {
            currentSession =
                await MultiplayerService.Instance
                    .JoinSessionByCodeAsync(joinCode);

            Debug.Log("Relay 접속 성공");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
