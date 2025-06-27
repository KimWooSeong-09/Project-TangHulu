using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class OneEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectile; // 투사체
    [SerializeField] private float speed = 5f; // 이동속도
    [SerializeField] private float attackCooldown = 2f; // 공격 시간
    [SerializeField] private float health = 100; // 체력
    [SerializeField] private CharacterController _cc;
    private float shootForce = 15f;
    private float lastAttackTime; // 마지막 공격 시간
    public GameObject experienceOb;
    public float GoldAmount;
    public PlayerStatHandler _playerStatHandler;

    public void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }
    void Update()
    {

        Vector3 Pos = transform.position;
        Pos.y = 0f;
        transform.position = Pos;
        Vector3 moveDir = (GameManager.singleton.player.transform.position - transform.position); // 이동할 위치
        Vector3 targetPos = GameManager.singleton.player.transform.position;
        targetPos.y = transform.position.y;
        if (moveDir.magnitude > 10f) // 거리가 10이상이면 따라감
        { 
            transform.LookAt(targetPos);
            moveDir.Normalize();
            _cc.Move(moveDir * (speed * Time.deltaTime));
            // y축 고정 방지
            Pos = transform.position;
            Pos.y = 0;
            transform.position = Pos;
        }
        else // 아니면 공격
        {
            _cc.Move(Vector3.zero);
            Pos = transform.position;
            Pos.y = 0f;
            transform.position = Pos;
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                AttackProjectile();
            }
        }

        // 이건 그냥 테스트
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
    }

    // 데미지 받기
    public void TakeDamage(float damage)
    {
        health -= damage;
        GoldManager mg = GameManager.singleton.goldManager;
        if (health <= 0)
        {
            GameObject experience = Instantiate(experienceOb, transform.position, Quaternion.identity);
            experience.transform.SetParent(GameManager.singleton.projFolder.transform);
            
            // 터지는 효과 추가 예정
            Destroy(gameObject);

            GoldAmount = UnityEngine.Random.Range(10f, 101f);
            mg.IncreaseGold(GoldAmount, false);
            
            _playerStatHandler.Increase();
        }
        Vector3 Pos = transform.position;
        Pos.y = 0f;
        transform.position = Pos;
    }

    private void AttackProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        newProjectile.transform.SetParent(GameManager.singleton.projFolder.transform);
        
        Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 dir = (GameManager.singleton.player.transform.position - transform.position).normalized;
            rb.useGravity = false;
            rb.linearVelocity = dir * shootForce;
        } 
    }
}
