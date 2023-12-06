using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderScript : MonoBehaviour
{
    public LogicScript logic;
    //public GameObject ground;

    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public System.Action killed;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    public int invaderScore = 10;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            this.killed.Invoke();
            logic.AddScore(invaderScore);
            this.gameObject.SetActive(false);
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
