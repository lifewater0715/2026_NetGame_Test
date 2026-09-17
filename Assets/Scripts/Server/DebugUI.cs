using System;
using Mono.Cecil.Cil;
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

    private String IP;
    private String Port;
    private String Code;
    private String UserName;

    void Awake()
    {
        serverManager = gameObject.GetComponent<ServerManager>();
        //SetDrbugUI
    }

    public void StartHost()
    {
        serverManager.HostStart(IP,Port);
    }
    public void StartClient(String code)
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
