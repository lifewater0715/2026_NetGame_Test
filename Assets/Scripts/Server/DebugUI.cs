using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private ServerManager serverManager;

    [SerializeField] private TMP_InputField GetIP;
    [SerializeField] private TMP_InputField GetPort;
    [SerializeField] private TMP_InputField GetCode;
    [SerializeField] private TMP_InputField GetUserName;

    [SerializeField] private String IP;
    [SerializeField] private String Port;
    [SerializeField] private String Code;
    [SerializeField] private String UserName;

    void Awake()
    {
        serverManager = gameObject.GetComponent<ServerManager>();
        //SetDrbugUI
    }

    public void StartHost()
    {
        serverManager.HostStart(IP,Port);
    }
    public void StartClient()
    {
        serverManager.ClientStart(Code);
    }
    public void StopConnet()
    {
        
    }
    public void InputData()
    {
        IP = GetIP.text;
        Port = GetPort.text;
        Code = GetCode.text;
        UserName = GetIP.text;
    }
}
