using NUnit.Framework;
using Unity.Netcode;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    [SerializeField]private float playerSpeed;
    [SerializeField]private float playerRotSpeed;

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMoveController(playerSpeed,playerRotSpeed);
    }

    private void PlayerMoveController(float fSpeed,float rSpeed)
    {
        if(!IsOwner)
            return;
            
        float VAxis = Input.GetAxisRaw("Vertical");
        float HAxis = Input.GetAxisRaw("Horizontal");

        gameObject.transform.Rotate(new Vector3(0,HAxis,0) * rSpeed * Time.deltaTime);
        gameObject.transform.position += transform.rotation * Vector3.forward * -VAxis * fSpeed * Time.deltaTime;

        //gameObject.transform.position += new Vector3(HAxis,0,VAxis) * speed;
    }
}
