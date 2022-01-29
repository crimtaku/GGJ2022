using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletResource;
    public float cdTimer;
    public float activeTimer;
    public bool hasActiveTimer;

    public float bulletSpread;
    public int bulletsPerArray;
    public float rotation;

    public float spinSpeed;
    public float spinSpeedChange;
    public bool spinReversal;
    public float maxSpinSpeed;

    //Things needed for aiming
    public bool aim;
    public Transform target;
    private Vector3 targetPos;
    private Vector3 thisPos;
    private float angle;

    public float cooldown;
    public float bulletSpeed;

    public List<float> spread;

    

    // Start is called before the first frame update
    void Start()
    {
        UpdateSpread();
    }

    // Update is called once per frame
    void Update()
    {
        if (aim)
        {
            targetPos = target.position;
            thisPos = transform.position;
            targetPos.x = targetPos.x - thisPos.x;
            targetPos.y = targetPos.y - thisPos.y;
            angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        //active timer for stopping the bullet spawns after desired time
        if (activeTimer>0 || !hasActiveTimer)
        {
            activeTimer -= Time.deltaTime;
            cdTimer-= Time.deltaTime;

            //check for spin speed and reverse it if needed
            if ((spinSpeed > maxSpinSpeed && spinSpeed >= 0) || (spinSpeed < -maxSpinSpeed && spinSpeed <= 0))
            {
                if (spinReversal)
                {
                    spinSpeedChange = spinSpeedChange * -1;
                }
                else
                {
                    spinSpeedChange = 0;
                }
            }
            spinSpeed += spinSpeedChange * Time.deltaTime;
            rotation += spinSpeed*Time.deltaTime;

            //spawn bullets
            if (cdTimer <= 0)
            {
                SpawnBullets();
                cdTimer += cooldown;
            }
        }        
    }

    public void SpawnBullets()
    {
        
        for (int i = 0; i< bulletsPerArray; i++)
        {
            //this really should be using pooling, specially for players and all the big shitstroms but not gonna take time to do it properly
            GameObject bullet=Instantiate<GameObject>(bulletResource);
            Bullet bulletdata = bullet.GetComponent<Bullet>();

            //offset would be either here or in the bullet prefab
            bullet.transform.position = this.transform.position;
            bulletdata.rotation = spread[i] + rotation +gameObject.transform.rotation.eulerAngles.z;
            bulletdata.speed = bulletSpeed;
        }
    }

    public void UpdateSpread()
    {
        spread.Clear();
        if (bulletsPerArray > 1)
        {
            float increment = bulletSpread / (bulletsPerArray - 1);

            float bulletrotation;
            for (int i = 0; i < bulletsPerArray; i++)
            {
                bulletrotation = bulletSpread / 2;
                bulletrotation += increment * i;
                bulletrotation -= bulletSpread;
                spread.Add(bulletrotation);
            }
        }

        //if only one bullet
        else
        {
            spread.Add(0);
        }
    }
}
