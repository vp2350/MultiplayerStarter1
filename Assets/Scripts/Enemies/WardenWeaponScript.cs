using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenWeaponScript : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float roundsPerMinute = 290f;

    [SerializeField] float timeToShoot = .1f;
    float shotTimer;

    private void Start()
    {
        shotTimer = 60f / roundsPerMinute;
    }

    public bool CanShoot
    {
        get
        {
            return timeToShoot < 0f;
        }
    }

    public void UpdateShootingTimer()
    {
        if (timeToShoot > 0f)
            timeToShoot -= Time.deltaTime;

    }

    public void Attack()
    {
        WardenBullet Temp = Instantiate(projectile).GetComponent<WardenBullet>();
        Temp.transform.position = transform.position;
        if (Temp != null)
        {
            Temp.Initialize(PlayerInfo.Instance.playerPos.position - transform.position, projectileSpeed, false);
        }
        timeToShoot = shotTimer;

    }
}
