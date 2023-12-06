using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public LogicScript logic;
    public Player player;

    public int displayValue;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HeartDisplay();
    }

    public void HeartDisplay()
    {
        if (displayValue > player.healthPoints - 1)
        {
            gameObject.SetActive(false);
        }
    }
}