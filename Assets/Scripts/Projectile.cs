using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public System.Action destroyed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LaserDestroy();
    }

    private void OnBecameInvisible()
    {
        LaserDestroy();
    }

    private void LaserDestroy()
    {
        if (destroyed != null)
        {
            this.destroyed.Invoke();
        }

        Destroy(this.gameObject);
    }


}
