using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bunker : MonoBehaviour
{
    public GameObject[] bunkerStates;
    private int damageTaken;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            this.gameObject.SetActive(false);
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Missile") && !CompareTag("LastState")){
            BunkerDegradation();
            damageTaken += 1;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void BunkerDegradation()
    {
        damageTaken += 1;

        damageTaken = Mathf.Clamp(damageTaken, 0, bunkerStates.Length - 1);

        GameObject newBunker = Instantiate(bunkerStates[damageTaken], transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
