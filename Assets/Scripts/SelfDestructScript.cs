using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour
{
    [SerializeField] float lifetime;
    void Start()
    {
        Destroy(gameObject, lifetime);    
    }
}
