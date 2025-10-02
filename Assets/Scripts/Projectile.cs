using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;

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
            damage = 1;
        }

        if(gunMode == 1)
        {
            speed = 10f;
            damage = 3;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            
            Destroy(gameObject);
        }
    }
}
