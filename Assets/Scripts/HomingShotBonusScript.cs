using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingShotBonusScript : MonoBehaviour
{
    public WeaponScript weaponScript;

    public float timer = 0;
    float bonusDuration = 6f;

    public bool homingShotActive;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (timer < bonusDuration && homingShotActive)
        {
            timer += Time.deltaTime;
        }
        else
        {
            HomingShotReset();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("HomingShotBonus"))
        {
            homingShotActive = true;

            Destroy(other.gameObject);
        }


    }


    public void HomingShotReset()
    {
        homingShotActive = false;
        timer = 0;
    }
}
