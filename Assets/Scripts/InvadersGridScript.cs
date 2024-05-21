using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvadersGridScript : MonoBehaviour
{
    // This just below is an Array, not a List
    public InvaderScript[] prefabs;
    public int rows = 5;
    public int columns = 9;
    public AnimationCurve speed;
    public float missleAttackRate = 1f;
    public Projectile missilePrefab;
    public GameObject leftBoundary;
    public GameObject rightBoundary;

    public LogicScript logic;
    public AudioManagerScript audioManager;

    public InvaderScript invader;

    public int amountKilled { get; private set; }
    public float amountAlive => this.totalInvaders - amountKilled;
    public int totalInvaders => this.rows * this.columns;
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

    private Vector3 direction = Vector2.right;


    private void Awake()
    {
        // Creating the Invaders grid with a loop
        for (int row = 0; row < this.rows; row++)
        {
            float width = 1.5f * (this.columns - 1);
            float height = 1.3f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.3f), 0f);

            for (int col = 0; col < this.columns; col++)
            {
                // Setting the first slot of 4 invaders
                // (the 4 of them will be on top of each other idk I didn't understand all of the tutorial)
                InvaderScript invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKilled;

                // Setting the position of the next invader
                Vector3 position = rowPosition;
                position.x += col * 1.5f;
                invader.transform.localPosition = position;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();

        InvokeRepeating(nameof(MissileAttack), this.missleAttackRate, this.missleAttackRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LogicScript.gameIsOver)
        {
            this.transform.position += direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

            // get the camera edges as coordonates points
            //Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
            //Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

            Bounds LeftBounds = leftBoundary.GetComponent<BoxCollider2D>().bounds;
            Bounds RightBounds = rightBoundary.GetComponent<BoxCollider2D>().bounds;

            // max means roughly the right side of the collider and the opposite goes for min
            Vector3 leftEdge = LeftBounds.max;
            Vector3 rightEdge = RightBounds.min;


            foreach (Transform invader in this.transform)
            {
                if (!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if(direction == Vector3.right && invader.position.x >= (rightEdge.x -1))
                {
                    AdvanceRow();
                }
                else if (direction == Vector3.left && invader.position.x <= (leftEdge.x +1))
                {
                    AdvanceRow();
                }
            }
        }

    }

    private void AdvanceRow()
    {
        direction.x *= -1f;

        Vector3 position = this.transform.position;
        position.y -= 1f;
        this.transform.position = position;
    }

    private void MissileAttack()
    {
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (Random.value < (1.0 / (float)this.amountAlive) && !LogicScript.gameIsOver)
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                AudioManagerScript.instance.PlayOneShot(FMODEventsScript.instance.InvaShootSound, transform.position);
                break;
            }
        }
    }

    private void InvaderKilled()
    {
        this.amountKilled++;

        if (this.amountKilled >= this.totalInvaders)
        {
            StartCoroutine(logic.Victory());
        }
    }


}
