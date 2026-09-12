using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRigidbody;
    [SerializeField] private float bulletSpeed;

    void Awake()
    {
        bulletRigidbody = gameObject.GetComponent<Rigidbody>();
        bulletRigidbody.linearVelocity = new Vector3(0, 0, bulletSpeed);

        Destroy(gameObject,5f); //selfDestroy
    }

    void OnCollisionEnter(Collision hitTrg)
    {
        if (hitTrg.transform.tag == "Player")
        {
            Debug.Log(hitTrg.gameObject.name + "/ 명중됨" + gameObject.transform.position + " 명중 좌표");
        }

        Destroy(gameObject,1f);
    }
}
