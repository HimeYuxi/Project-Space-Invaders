using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvadeathScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DisplayTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DisplayTime()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
