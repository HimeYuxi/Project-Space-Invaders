using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject leftBoundary;
    public GameObject rightBoundary;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        leftBoundary = GameObject.FindGameObjectWithTag("LeftBoundary");
        rightBoundary = GameObject.FindGameObjectWithTag("RightBoundary");
    }

    // Update is called once per frame
    void Update()
    {
        Bounds LeftBounds = leftBoundary.GetComponent<BoxCollider2D>().bounds;
        Bounds RightBounds = rightBoundary.GetComponent<BoxCollider2D>().bounds;

        Vector3 leftEdge = LeftBounds.max;
        Vector3 rightEdge = RightBounds.min;

        if (transform.position.x <= leftEdge.x + 1)
        {
            transform.position = new Vector2(leftEdge.x + 1, transform.position.y);
        }

        if (transform.position.x >= rightEdge.x - 1)
        {
            transform.position = new Vector2(rightEdge.x - 1, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
