using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvester : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;
    public int toolDamage = 1;
    public float hitCooldown = 0.15f;
    private float nextHitTime;
    private Camera cam;
    public Inventory inventory;

    private void Awake()
    {
        cam = Camera.main;
        if (inventory == null)
            inventory = gameObject.AddComponent<Inventory>();
        
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && Time.time >= nextHitTime)
        {
            nextHitTime = Time.time + hitCooldown;

            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            if(Physics.Raycast(ray,out var hit,rayDistance, hitMask))
            {
                var block = hit.collider.GetComponent<Block>();
                if(block != null)
                {
                    block.Hit(toolDamage, inventory);
                }
            }
        }
    }
}
