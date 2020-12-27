using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class RangedWeaponScript : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float roundsPerMinute = 290f;
    [SerializeField] AudioClip soundEffect;
    [SerializeField] GameObject muzzleFlash;
    Camera camera2;
    GameObject camera2Obj;
    PhotonView PV;

    void Start()
    {
        PV = this.transform.parent.GetComponent<PhotonView>();
        camera2Obj = (this.transform.parent).gameObject;
        camera2 = camera2Obj.transform.Find("Camera").GetComponent<Camera>();
    }
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
        if (timeToShoot <= 0f && PV.IsMine)
        {
            muzzleFlash.SetActive(true);

            BulletScript Temp = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerBullet"), new Vector3(0,0,0), Quaternion.identity).GetComponent<BulletScript>();
            Temp.transform.position = transform.position + (camera2.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized*1.5f;
            if (Temp != null)
            {
                Temp.Initialize(camera2.ScreenToWorldPoint(Input.mousePosition) - transform.position, projectileSpeed, true, this.gameObject);
                GetComponent<AudioSource>().PlayOneShot(soundEffect, AudioManager.Instance.sfxVolume);
            }
            timeToShoot += 60f / roundsPerMinute;
            GetComponent<PlayerWeaponController>().firing = false;
        }
    }
}
