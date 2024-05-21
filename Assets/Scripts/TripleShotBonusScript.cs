using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotBonusScript : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;

    float timer;
    float bonusDuration = 5;

    public bool tripleShotActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tripleShotActive && timer <= bonusDuration)
        {
            timer += Time.deltaTime;
        }

        if (timer >= bonusDuration)
        {
            TripleShotBonusReset();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TripleShotBonus"))
        {
            tripleShotActive = true;

            gun1.gameObject.SetActive(true);
            gun2.gameObject.SetActive(true);

            Destroy(other.gameObject);
        }

    }

    void TripleShotBonusReset()
    {
        tripleShotActive = false;

        gun1.gameObject.SetActive(false);
        gun2.gameObject.SetActive(false);

        timer = 0;
    }
}
