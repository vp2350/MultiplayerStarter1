using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponScript : MonoBehaviour
{
    [SerializeField] GameObject meleeController;
    [SerializeField] float roundsPerMinute = 250f;


    float timeToShoot;

    // Update is called once per frame
    void Update()
    {
        if (timeToShoot > 0f)
            timeToShoot -= Time.deltaTime;
    }

    public void Attack()
    {
        if (timeToShoot <= 0f)
        {
            meleeController.GetComponent<MeleeControllerScript>().Attack();

            timeToShoot += 60f / roundsPerMinute;

            GetComponent<PlayerWeaponController>().firing = false;
        }
    }
}
