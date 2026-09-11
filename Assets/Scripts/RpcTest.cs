using UnityEngine;
using Unity.Netcode;
using NUnit.Framework;
using System.Diagnostics;


public class RpcTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        //이 NetworkBehaviour 인스턴스의 NetworkObject를 소유한 클라이언트만 서버로 RPC를 보낼 수 있습니다
        //서버에서만 작동
        if(!IsServer && IsOwner)
        {
            ServerOnlyRpc(0, NetworkObjectId);
        }
    }

    [Rpc(SendTo.Server)]
    private void ServerOnlyRpc(int val, ulong sourceNetworkObjectId)
    {
        UnityEngine.Debug.Log("서버 수신함 :" + val + "넷 오브제 " + sourceNetworkObjectId + "# 로부터");
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void ClientAndHostRpc(int val, ulong sourceNetworkObjectId)
    {
        UnityEngine.Debug.Log("클라이언트 수신함 :" + val + "넷 오브제 " + sourceNetworkObjectId + "# 로부터");

        if(IsOwner)
        {
            ServerOnlyRpc(val + 1, sourceNetworkObjectId);
        }
    }
}
