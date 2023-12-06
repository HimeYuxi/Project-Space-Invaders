using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 2;
    public int healthPoints = 3;
    public bool playerIsDead = false;

    public Sprite[] animationSprites;
    public SpriteRenderer spriteRender;

    public Projectile laserPrefab;
    private bool laserActive;
    public GameObject LeftBoundary;
    public GameObject RightBoundary;
    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        //logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Bounds LeftBounds = LeftBoundary.GetComponent<BoxCollider2D>().bounds;
        Bounds RightBounds = RightBoundary.GetComponent<BoxCollider2D>().bounds;

        Vector3 leftEdge = LeftBounds.max;
        Vector3 rightEdge = RightBounds.min;

        float horizontalInput = Input.GetAxis("Horizontal");

        if (!LogicScript.gameIsOver)
        {
            // Si l'entrée horizontale est positive, déplace l'objet vers la droite
            if (horizontalInput > 0.1f && transform.position.x <= rightEdge.x - 1)
            {
                this.transform.position += Vector3.right * this.speed * Time.deltaTime;
            }
            // Si l'entrée horizontale est négative, déplace l'objet vers la gauche
            else if (horizontalInput < -0.1f && transform.position.x >= leftEdge.x + 1)
            {
                this.transform.position += Vector3.left * this.speed * Time.deltaTime;
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !LogicScript.gameIsPaused)
            {
                Shoot();
            }
        }

    }

    void Shoot()
    {
        if (!laserActive)
        {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            laserActive = true;
        }
    }

    void LaserDestroyed()
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!LogicScript.gameIsOver)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Missile")){
            
                if (healthPoints > 1)
                {
                    TakeDamage();
                }
                else
                {
                    TakeDamage();
                    Death();
                }

            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
            {
                healthPoints = 0;
                Death();
            }
        }

    }

    public void TakeDamage()
    {
        healthPoints -= 1;
        Debug.Log("oof" + healthPoints);
    }

    public void Death()
    {
        playerIsDead = true;
        spriteRender.sprite = animationSprites[0];
        StartCoroutine(logic.GameOver());
    }

}
