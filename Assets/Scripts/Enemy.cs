using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("ü��")]
    public float Health = 5;

    public float moveSpeed = 2f;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        //�÷��̾������ ���� ���ϱ�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    public void ProjectileHit(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject);
    }
}
