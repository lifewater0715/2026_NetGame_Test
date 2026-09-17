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
    [SerializeField] private TMP_Text inviteCodeUI;

    private ISession currentSession;
    [SerializeField] private string InviteCode;
    [SerializeField] private string JoinCode;

    //[SerializeField] private char serverState = 'N';
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        LoginUnityRplay();
        Server_S_UI_Rpc('N');
        networkManager = gameObject.GetComponent<NetworkManager>();
    }

    public void HostStart(String ip, String host)
    {
        if (ip != "" && host != "")
        {
            networkManager.StartHost();
        }
        else
        {
            CreateHost();
        }
        Server_S_UI_Rpc('H');
        return;
    }

    public void ClientStart(String code)
    {
        if (code != "")
        {
            JoinGame(code);
        }
        else
        {
            networkManager.StartClient();
        }
        Server_S_UI_Rpc('C');
        return;
    }

    public void StopServer()
    {
        Debug.Log("STOP!");
    }

    [Rpc(SendTo.Server)]
    private void Server_S_UI_Rpc(char serverStateText)
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
            inviteCodeUI.text = "InviteCode : <" + JoinCode + ">";
            Debug.Log($"Relay Host 생성 완료");
            Debug.Log($"Join Code : {JoinCode}");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

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
