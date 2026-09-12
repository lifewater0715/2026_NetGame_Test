using Unity.Netcode;
using UnityEngine;

public class PlayerRPC : NetworkBehaviour
{
    public NetworkVariable<Vector3> PlayerPos = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            PlayerMove();
        }
    }

    private void PlayerMove()
    {
        SubmitPositionRequestRpc();
    }

    [Rpc(SendTo.Server)]
    private void SubmitPositionRequestRpc(RpcParams rpcParams = default)
    {
        var randomPosition = GetRandomPositionOnPlane();
        transform.position = gameObject.transform.position;
        PlayerPos.Value = gameObject.transform.position;
    }

    [ContextMenu("Move")]
    public Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }

    void Update()
    {
        //transform.position = PlayerPos.Value;
        SubmitPositionRequestRpc();
    }
}
