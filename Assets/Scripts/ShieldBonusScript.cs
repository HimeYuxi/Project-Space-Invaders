using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonusScript : MonoBehaviour
{

    public Transform player;
    public Rigidbody2D rb;
    public GameObject shield;

    float timer;
    float bonusDuration = 15;

    bool shieldActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shieldActive && timer <= bonusDuration)
        {
            timer += Time.deltaTime;
        }

        if (timer >= bonusDuration)
        {
            ShieldReset();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShieldBonus"))
        {
            ShieldActivation();
            Destroy(other.gameObject);
        }
    }

    void ShieldActivation()
    {
        // Activates the Shield GameObject that is constantly on the player.
        shieldActive = true;
        shield.SetActive(true);
    }

    void ShieldReset()
    {
        shieldActive = false;
        shield.SetActive(false);
        timer = 0;
    }

}
