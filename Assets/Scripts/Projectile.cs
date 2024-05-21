using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 direction;

    public System.Action destroyed;
    public WeaponScript weaponScript;
    public Rigidbody2D rb;
    public Transform target;
    public HomingShotBonusScript homingShotBonusScript;

    Transform closestEnemy;
    public float speed;
    float rotateSpeed = 700f;

    public bool hasBeenDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        weaponScript = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponScript>();
        homingShotBonusScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HomingShotBonusScript>();
        rb = GetComponent<Rigidbody2D>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Invader");
        closestEnemy = FindClosestEnemy(enemies);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (gameObject.layer == LayerMask.NameToLayer("Laser") && homingShotBonusScript.homingShotActive)
        {
            target = closestEnemy;
            Vector2 homingDirection = (Vector2)target.position - rb.position;

            homingDirection.Normalize();

            float rotateAmount = Vector3.Cross(homingDirection, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Invader");

            closestEnemy = FindClosestEnemy(enemies);
        }
        else
        {
            this.transform.position += this.direction * this.speed * Time.deltaTime;
        }
    }

    public Transform FindClosestEnemy(GameObject[] enemies)
    {
        // Initialize the variable to stock the closest enemy
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            // this checks if the distance to the current enemy in the loop is smaller 
            // than the current closest distance, if so, it updates the closestDistance value
            // and assign the transform of this enemy to the closestEnemy variable
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        ProjectileDestroy();
    }

    private void OnBecameInvisible()
    {
        ProjectileDestroy();
    }

    private void ProjectileDestroy()
    {
        // This boolean prevents the code to destroy.Invoke twice
        if (!hasBeenDestroyed)
        {
            hasBeenDestroyed = true;

            if (destroyed != null)
            {
                destroyed.Invoke();
                //Debug.Log("DESTROY INVOKE");
            }

            Destroy(this.gameObject);
        }
    }


}
