using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;
using TMPro;
using System.Linq;

public class ServerManager : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private TMP_Text serverStateUI;
    [SerializeField] private char serverState = 'N';
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Server_S_UI('N');
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
        Server_S_UI('C');
        networkManager.StartClient();
    }

    private void Server_S_UI(char serverStateText)
    {
        string Text = "Current State : <" + serverStateText + ">";
        serverStateUI.text = Text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
