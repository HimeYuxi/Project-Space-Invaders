using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEventsScript : MonoBehaviour
{
    [field: Header("PlayerShoot SFX")]
    [field: SerializeField] public EventReference PlayerShootSound { get; private set; }

    [field: Header("PlayerHit SFX")]
    [field: SerializeField] public EventReference PlayerHitSound { get; private set; }

    [field: Header("PlayerDeath SFX")]
    [field: SerializeField] public EventReference PlayerDeathSound { get; private set; }

    [field: Header("InvaShoot SFX")]
    [field: SerializeField] public EventReference InvaShootSound { get; private set; }

    [field: Header("Invadeath SFX")]
    [field: SerializeField] public EventReference InvadeathSound { get; private set; }

    public static FMODEventsScript instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
