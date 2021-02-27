using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ProgressBar rechargeProgBar;
    public GameObject smoke;
    public ParticleSystem smokeParticleSystem;
    public Transform bulletSpawnPos;
    private LineRenderer lr;
    private bool canShoot;
    private int overload = 0;
    public int shootEnergy = 5;
    public bool recharging = false;
    private float rechargeTime = 1;
    private AudioSource m_AudioSource;
    private bool isShooting;
    private MeshRenderer gunMesh;
    public GameObject crosshair;
    private SpriteRenderer crosshairSpriteRenderer;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        canShoot = true;
        rechargeProgBar.BarValue = 0;
        smokeParticleSystem = smoke.GetComponent<ParticleSystem>();
        recharge();
        m_AudioSource = GetComponent<AudioSource>();
        isShooting = false;
        gunMesh = GetComponent<MeshRenderer>();
        crosshairSpriteRenderer = crosshair.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, bulletSpawnPos.position);
        if (isShooting)
        {
            lr.SetPosition(1, transform.forward * 5000);
        }
        if (overload >= 100)
        {
            canShoot = false;
            recharging = true;
            handleSmoke(true);

        }
        rechargeProgBar.BarValue = overload;

        RaycastHit target;
        bool status = Physics.Raycast(bulletSpawnPos.position, bulletSpawnPos.forward, out target, 100);
        if (status)
        {
            Enemy enemy = target.collider.GetComponent<Enemy>();
            if (enemy && !enemy.dead)
            {
                crosshairSpriteRenderer.color = Color.red;
            }
            else
            {
                crosshairSpriteRenderer.color = Color.white;
            }
        }
        else
        {
            crosshairSpriteRenderer.color = Color.white;
        }
    }

    public void shoot()
    {
        if (canShoot)
        {
            lr.enabled = true;
            m_AudioSource.Play();
            RaycastHit target;
            bool status = Physics.Raycast(bulletSpawnPos.position, bulletSpawnPos.forward, out target, 100);
            if (status && target.collider.GetComponent<shootable>())
            {
                Debug.Log(target.collider.gameObject.name);
                lr.SetPosition(1, target.point);
                isShooting = true;
                Enemy enemy = target.collider.gameObject.GetComponent<Enemy>();
                if (enemy)
                {
                    enemy.hit();
                }
            }
            else
            {
                lr.SetPosition(1, transform.forward * 5000);
            }
            canShoot = false;
            overload += shootEnergy;
            Invoke("stopLaser", 0.1f);
        }

    }

    private void stopLaser()
    {
        isShooting = false;
        lr.enabled = false;
        canShoot = true;
    }

    private void recharge()
    {
        if (overload > 0)
        {
            overload -= shootEnergy;
        }
        if (recharging && overload == 0)
        {
            recharging = false;
            canShoot = true;
            handleSmoke(false);
        }
        Invoke("recharge", rechargeTime);
    }

    public void handleSmoke(bool state)
    {

        smoke.SetActive(state);
    }

    public void setVisibility(bool state)
    {
        gunMesh.enabled = state;
        crosshair.SetActive(state);
        if (recharging)
        {
            handleSmoke(state);
        }
    }

}
