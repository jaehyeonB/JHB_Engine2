using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Trace, Attack, Runaway
    }
    public EnemyState state = EnemyState.Idle;

    public float moveSpeed = 2f;            //이동속도
    public float traceRange = 15f;          //추격 범위
    public float attackRange = 1f;          //공격사거리
    public float attackCooldown = 1.5f;     //공격딜레이

    public GameObject projectilePrefabs;
    public Transform firePoint;

    private Transform player;
    private float lastAttackTime;

    public int maxHP = 5;
    private int currentHP;

    public Slider hpSilder;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = attackCooldown;
        currentHP = maxHP;
        hpSilder.value = 1f;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        //몬스터 행동 상태 전환 모듈
        switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;

            case EnemyState.Attack:
                AttackPlayer();
                if (dist > attackRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Runaway:
                RunAway();
                if (dist > traceRange)
                    state = EnemyState.Idle;
                break;
        }
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);

        if(currentHP <= 2)
        {
            state = EnemyState.Runaway;
        }
    }

    void RunAway()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position -= dir * moveSpeed * Time.deltaTime;
        transform.LookAt(-player.position);
    }

    void AttackPlayer()
    {
        if(Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if(projectilePrefabs != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefabs, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if(ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpSilder.value = (float)currentHP / maxHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
