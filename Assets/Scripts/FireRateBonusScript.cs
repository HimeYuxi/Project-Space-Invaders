using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateBonusScript : MonoBehaviour
{
    public WeaponScript weaponScript;

    public float timer = 0;
    float bonusDuration = 6f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < bonusDuration && weaponScript.fireRateBonusActive)
        {
            timer += Time.deltaTime;
        }
        else if (weaponScript.fireRateBonusActive)
        {
            FireRateReset();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireRateBonus"))
        {
            
            FireRateUp();
            Destroy(other.gameObject);

        }

    }

    void FireRateUp()
    {
        weaponScript.fireRateBonusActive = true;

        if (weaponScript != null)
        {
            weaponScript.fireRate = 7;
        }
        else
        {
            Debug.LogError("WeaponScript not found on the player");
        }
    }
    
    void FireRateReset()
    {
        weaponScript.fireRateBonusActive = false;
        weaponScript.fireRate = 1;
        timer = 0;
    }
}
