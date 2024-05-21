using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltWeaponScript : MonoBehaviour
{
    public Projectile laserPrefab;

    public bool tripleShotActive;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = (transform.localRotation * Vector2.up).normalized;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
        projectile.direction = direction;
    }
}
