using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerWeaponManager : NetworkBehaviour
{
    [SerializeField] private GameObject Bullet;

    [SerializeField] private float FireDlay;

    [SerializeField] private Vector3 FireDir;   // 발사각
    [SerializeField] private GameObject Muzzle; // 총구 위치

    [SerializeField] private GameObject AnimTrg;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsOwner)
        {
            // 권한 분산
            BulletFireRpc(Muzzle.transform.position, Muzzle.transform.rotation.eulerAngles);
        }
    }

    [Rpc(SendTo.Server)]
    private void BulletFireRpc(Vector3 BulletPos,Vector3 BulletRot)
    {
        Debug.Log("FirePos : "+ transform.position +"/FireDir : "+ BulletRot);
        DrawBulletRpc(BulletPos,BulletRot);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void DrawBulletRpc(Vector3 BulletPos,Vector3 BulletRot)
    {
        Debug.Log("DrawPos : "+ transform.position);
        GameObject bullet = Instantiate(Bullet, BulletPos, Quaternion.Euler(BulletRot));
    }
}
