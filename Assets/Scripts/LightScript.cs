using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject globalLight;
    // Start is called before the first frame update
    void Start()
    {
        globalLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
