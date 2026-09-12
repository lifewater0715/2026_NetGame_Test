using NUnit.Framework;
using Unity.Netcode;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField]private float PlayerSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveController(PlayerSpeed);
    }

    private void PlayerMoveController(float speed)
    {
        if(!IsOwner)
            return;
            
        float VAxis = Input.GetAxisRaw("Vertical");
        float HAxis = Input.GetAxisRaw("Horizontal");

        gameObject.transform.position += new Vector3(HAxis,0,VAxis) * speed;
    }
}
