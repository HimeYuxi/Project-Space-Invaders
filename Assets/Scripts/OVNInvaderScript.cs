using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVNInvaderScript : MonoBehaviour
{
    public LogicScript logic;
    public GameObject fireRateBonus;
    public GameObject shieldBonus;
    public GameObject tripleShotBonus;
    public GameObject homingShotBonus;


    public int score = 300;
    public float speed = 5;
    public float edgePosition = 9.3f;

    public bool motionless = true;

    Vector3 direction = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        StartCoroutine(OVNIStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (!motionless && !LogicScript.gameIsOver)
        {
        this.transform.position += direction * this.speed * Time.deltaTime;
        }

        if (!motionless)
        {
            if (this.direction == Vector3.right && this.transform.position.x >= edgePosition)
            {
                StartCoroutine(OVNIMovement());
            }
            else if (this.direction == Vector3.left && this.transform.position.x <= -edgePosition)
            {
                StartCoroutine(OVNIMovement());
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            logic.AddScore(this.score);

            GameObject[] bonusArray = { fireRateBonus, shieldBonus, tripleShotBonus, homingShotBonus};
            GameObject selectedBonus = bonusArray[Random.Range(0, bonusArray.Length)];

            Instantiate(selectedBonus, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

    }

    IEnumerator OVNIMovement()
    {
        motionless = true;
        direction *= -1;
        yield return new WaitForSeconds(5f);
        motionless = false;
    }

    IEnumerator OVNIStart()
    {
        motionless = true;
        yield return new WaitForSeconds(5f);
        motionless = false;
    }
}
