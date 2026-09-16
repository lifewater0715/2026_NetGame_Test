using UnityEngine;
using Unity.Netcode;
using System;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;

public class PlayerWeaponManager : NetworkBehaviour
{
    //Bullet Prefab
    [SerializeField] private GameObject Bullet;

    // FireDlay
    private float DlayTimer;
    private float DlayTimerServer;

    [SerializeField] private float FireDlay;
    [SerializeField] private bool CanFire = true;
    [SerializeField] private bool CanFireServer = true;

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
        if (!CanFire) DlayTimer += Time.deltaTime;
        if (FireDlay <= DlayTimer && !CanFire)
        {
            BulletFireDlayRpc(true);
            DlayTimer = 0f;
        }

        if (Input.GetKey(KeyCode.Space) && IsOwner && CanFire)
        {
            CanFire = false; // 발사 트리거 초기화

            StartCoroutine(AnimationPlayer()); //에니메이션 트리거 ON
            BulletFireRpc(Muzzle.transform.position, Muzzle.transform.rotation.eulerAngles);
        }
    }

    private IEnumerator AnimationPlayer() //에니메이션 플레이어
    {
        AnimTrg.SetBool("IsFire", true);

        yield return new WaitForSeconds(0.15f);

        AnimTrg.SetBool("IsFire", false);
    }

    [Rpc(SendTo.Server)] // 발사 딜레이 연산 (server)
    private void BulletFireDlayRpc(bool canFire)
    {
        BulletDlaySanderRpc(canFire);
        return;
    }

    [Rpc(SendTo.ClientsAndHost)] //발사 딜레이 클라이언트로 전송 (host/client)
    private void BulletDlaySanderRpc(bool canFire)
    {
        if (!IsOwner) return;

        CanFire = true;
        return;
    }

    [Rpc(SendTo.Server)] // 발사 입력 전송 (host)
    private void BulletFireRpc(Vector3 BulletPos, Vector3 BulletRot)
    {
        //Debug.Log("FirePos : "+ transform.position +"/FireDir : "+ BulletRot);
        CanFireServer = false; // 서버 발사 트리거 초기화

        EffectTrigerRpc();
        BulletDrawRpc(BulletPos, BulletRot);
    }

    [Rpc(SendTo.ClientsAndHost)] // 탄환 드로우 (host/Client)
    private void BulletDrawRpc(Vector3 BulletPos, Vector3 BulletRot)
    {
        //Debug.Log("DrawPos : "+ transform.position);
        GameObject bullet = Instantiate(Bullet, BulletPos, Quaternion.Euler(BulletRot));
    }

    [Rpc(SendTo.ClientsAndHost)] // 이팩트 실행 각각의 클라이언트로 전송 (host/Client)
    private void EffectTrigerRpc()
    {
        FireParticleSystemSmoke.Play();
        return;
    }
}