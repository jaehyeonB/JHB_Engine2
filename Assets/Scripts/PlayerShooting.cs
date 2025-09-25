using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefabs;
    public Transform firePoint;
    Camera cam;

    [Header("ÃÑ¸ðµå")]
    public int gunMode = 0;

    void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Z))        //
        {
            gunMode++;
            Debug.Log("!");
            if (gunMode > 1)
                gunMode = 0;

        }
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        //projectile  ¼³Á¤
        GameObject proj = Instantiate(projectilePrefabs, firePoint.position, Quaternion.LookRotation(direction));
    }


}
