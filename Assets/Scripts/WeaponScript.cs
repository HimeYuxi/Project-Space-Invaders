using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public TripleShotBonusScript tripleShotBonusScript;
    public AudioManagerScript audioManager;

    public Player player;
    public Projectile laserPrefab;
    public AltWeaponScript gun1;
    public AltWeaponScript gun2;

    public List<Projectile> laserAmount = new List<Projectile>();

    public float fireRate;
    public float fireDelay;
    public float timer;

    public bool fireRateBonusActive;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();


        // without this we'd have to wait a bit before firing the first laser
        timer = fireDelay;

        direction = (transform.localRotation * Vector2.up).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // setting the fire rate 
        fireDelay = 1 / fireRate;

        if (timer < fireDelay)
        {
            // This makes the timer increase it's value until it reaches the FireDelay value which allows the player to shoot
            timer += Time.deltaTime;
        }

        // Allowing to shoot with keys only if the game isn't paused or over
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !LogicScript.gameIsPaused && !LogicScript.gameIsOver)
        {
            if (timer >= fireDelay || laserAmount.Count == 0 && !fireRateBonusActive)
            {
                Shoot();
                // Reseting the timer so we can shoot again later
                timer = 0;
            }

        }
    } 

    public void Shoot()
    {
        AudioManagerScript.instance.PlayOneShot(FMODEventsScript.instance.PlayerShootSound, transform.position);
        Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
        projectile.direction = direction;
        laserAmount.Add(projectile);

        if (tripleShotBonusScript.tripleShotActive)
        {
            gun1.Shoot();
            gun2.Shoot();
        
        }

        projectile.destroyed += LaserDestroyed;
    }

    void LaserDestroyed()
    {
        //if (!fireRateBonusActive)
        {
            laserAmount.Remove(laserAmount[laserAmount.Count -1]);
        }
    }
}
