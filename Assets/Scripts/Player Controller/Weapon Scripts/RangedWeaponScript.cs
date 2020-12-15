using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponScript : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float roundsPerMinute = 290f;
    [SerializeField] AudioClip soundEffect;
    [SerializeField] GameObject muzzleFlash;

    float timeToShoot;
    // Update is called once per frame
    void Update()
    {
        if (timeToShoot > 0f)
        {
            timeToShoot -= Time.deltaTime;
        }

    }

    /// <summary>
    /// Attacks with a ranged weapon
    /// </summary>
    public void Attack()
    {
        if (timeToShoot <= 0f)
        {
            muzzleFlash.SetActive(true);

            BulletScript Temp = Instantiate(projectile).GetComponent<BulletScript>();
            Temp.transform.position = transform.position;
            if (Temp != null)
            {
                Temp.Initialize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, projectileSpeed, true);
                GetComponent<AudioSource>().PlayOneShot(soundEffect, AudioManager.Instance.sfxVolume);
            }
            timeToShoot += 60f / roundsPerMinute;
            GetComponent<PlayerWeaponController>().firing = false;
        }
    }
}
