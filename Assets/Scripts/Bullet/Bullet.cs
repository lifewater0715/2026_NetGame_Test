using UnityEngine;
using Unity.Netcode;


public class Bullet : NetworkBehaviour
{
    [SerializeField] private Rigidbody bulletRigidbody;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletForceLifetime;


    void Awake()
    {
        bulletRigidbody = gameObject.GetComponent<Rigidbody>();

        Destroy(gameObject,bulletLifetime); //selfDestroy
    }

    void FixedUpdate()
    {
        gameObject.transform.position += transform.rotation * Vector3.forward *  -bulletSpeed * Time.deltaTime;

        if(bulletLifetime <= Time.fixedDeltaTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision hitTrg)
    {
        if(!IsServer) return; //서버에서만 충돌 확인

        if (hitTrg.transform.tag == "Player")
        {
            //Debug.Log(hitTrg.gameObject.name + "/ 명중됨" + gameObject.transform.position + " 명중 좌표");
        }

        Destroy(gameObject,1f);
    }

    private void Hit()
    {
        
    }
}
