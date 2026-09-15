using UnityEngine;
using Unity.Netcode;
using System;
using System.Collections;

public class PlayerWeaponManager : NetworkBehaviour
{
    //Bullet Prefab
    [SerializeField] private GameObject Bullet;

    // FireDlay
    [SerializeField] private float FireDlay;
    private float DlayTimer;
    [SerializeField] private bool CanFire = true;

    //GetPlayerFire Info
    [SerializeField] private Vector3 FireDir;   // 발사각
    [SerializeField] private GameObject Muzzle; // 총구 위치

    //Animtion
    [SerializeField] private Animator AnimTrg;
    //FireEffect
    [SerializeField] private ParticleSystem FireParticleSystemFire;
    [SerializeField] private ParticleSystem FireParticleSystemSmoke;


    void Awake()
    {
        AnimTrg = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!CanFire) BulletFireDlayRpc();

        if(Input.GetKey(KeyCode.Space) && IsOwner && CanFire)
        {
            CanFire = false; // 발사 초기화
            StartCoroutine(AnimationPlayer()); //에니메이션 트리거 ON
            BulletFireRpc(Muzzle.transform.position, Muzzle.transform.rotation.eulerAngles);
        }
    }

    [Rpc(SendTo.Server)] // 발사 딜레이 연산 (server)
    private void BulletFireDlayRpc() 
    {
        DlayTimer += Time.deltaTime;
        if(FireDlay <= DlayTimer)
        {
            DlayTimer = 0f;
            BulletDlaySanderRpc();
        }

        return;
    } 

    [Rpc(SendTo.ClientsAndHost)] //발사 딜레이 클라이언트로 전송 (client)
    private void BulletDlaySanderRpc()
    {
        if (!IsOwner) return;

        Debug.Log("발사 가능!");
        CanFire = true;
        return;
    }

    [Rpc(SendTo.Server)] // 탄환 전송 (host)
    private void BulletFireRpc(Vector3 BulletPos,Vector3 BulletRot)
    {
        Debug.Log("FirePos : "+ transform.position +"/FireDir : "+ BulletRot);
        DrawBulletRpc(BulletPos,BulletRot);
    }

    [Rpc(SendTo.ClientsAndHost)] // 탄환 드로우 (host/Client)
    private void DrawBulletRpc(Vector3 BulletPos,Vector3 BulletRot)
    {
        Debug.Log("DrawPos : "+ transform.position);
        GameObject bullet = Instantiate(Bullet, BulletPos, Quaternion.Euler(BulletRot));
    }

    [Rpc(SendTo.ClientsAndHost)] // 이팩트 실행 (host/Client)
    private void EffectTrigerRpc()
    {
        FireParticleSystemSmoke.Play();
        if(IsOwner!) return;
    }

    private IEnumerator AnimationPlayer()
    {
        AnimTrg.SetBool("IsFire",true);

        EffectTrigerRpc();

        yield return new WaitForSeconds(0.1f);

        AnimTrg.SetBool("IsFire",false);
    }
}
