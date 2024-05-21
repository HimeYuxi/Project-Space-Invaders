using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderScript : MonoBehaviour
{
    public LogicScript logic;
    public AudioManagerScript audioManager;

    //public GameObject ground;

    public GameObject invadeath;
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public System.Action killed;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    public int invaderScore = 10;
    bool hasBeenDestroyed;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerScript>();
    }

    private void AnimateSprite()
    {
        animationFrame++;

        if (animationFrame >= this.animationSprites.Length)
        {
            animationFrame = 0;
        }

        spriteRenderer.sprite = this.animationSprites[animationFrame];
    }

    void Invadeath()
    {
        AudioManagerScript.instance.PlayOneShot(FMODEventsScript.instance.InvadeathSound, transform.position);
        this.killed.Invoke();
        logic.AddScore(invaderScore);
        this.gameObject.SetActive(false);
        Instantiate(invadeath, new Vector3 (transform.position.x, transform.position.y), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            if (!hasBeenDestroyed)
            {
                hasBeenDestroyed = true;
                Invadeath();
            }

        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(logic.GameOver());
        }
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
