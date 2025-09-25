using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;

    public float speed = 20f;
    public float lifeTime = 2f;
    public int gunMode;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
        gunMode = FindObjectOfType<PlayerShooting>().gunMode;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(gunMode == 0)
        {
            speed = 30f;
            damage = 1f;
        }

        if(gunMode == 1)
        {
            speed = 10f;
            damage = 3f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.ProjectileHit(damage);
            
            Destroy(gameObject);
        }
    }
}
