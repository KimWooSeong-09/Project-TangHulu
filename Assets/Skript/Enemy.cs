using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // 이동속도
    [SerializeField] private float attackCooldown = 2f; // 공격 시간
    [SerializeField] private float health = 100; // 체력
    [SerializeField] private CharacterController _cc;
    private float lastAttackTime; // 마지막 공격 시간
    private float damage = 10f;
    private PlayerStatHandler _playerStat;
    public GameObject experienceOb;
    public float GoldAmount;
    public PlayerStatHandler _playerStatHandler;
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _playerStat = GameManager.singleton.player.GetComponent<PlayerStatHandler>();
    }
    void Update()
    {
        fixY();
        Vector3 moveDir = (GameManager.singleton.player.transform.position - transform.position); // 이동할 위치
        if (moveDir.magnitude > 1.4f) // 거리가 1.4이상이면 따라감
        {
            transform.LookAt(GameManager.singleton.player.transform.position);
            moveDir.Normalize();
            _cc.Move(moveDir * (speed * Time.deltaTime));
            fixY();
        }
        else // 아니면 공격
        {
            _cc.Move(Vector3.zero);
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                _playerStat.TakeDamage(damage);
                fixY();
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
        GoldManager mg = GameManager.singleton.goldManager;
        health -= damage;
        fixY();
        if (health <= 0)
        {
            Vector3 SpawnLocation = new Vector3(transform.position.x, 0.5f, transform.position.z);
            GameObject experience = Instantiate(experienceOb, SpawnLocation, Quaternion.identity);
            experience.transform.SetParent(GameManager.singleton.projFolder.transform);
            // 터지는 효과 추가 예정
            Destroy(gameObject);
            
            GoldAmount = UnityEngine.Random.Range(10f, 101f);
            mg.IncreaseGold(GoldAmount, false);

            _playerStat.Increase();
        }
    }

    public void fixY()
    {
        Vector3 Pos = transform.position;
        Pos.y = 0f;
        transform.position = Pos;
    }
}
