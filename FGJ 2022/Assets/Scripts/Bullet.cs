using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float timeToLive=10;

    //0 on sininen, 1 on punainen, 2 on joku muu väri ja satuttaa molempia
    public bool hurtsBlue;
    public bool hurtsRed;

    public float maxX=4.8f;
    public float minX=-4.8f;
    public float maxY=5;
    public float minY=-5;

    public bool isplayerbullet;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * speed*Time.deltaTime);

        /*timeToLive -= Time.deltaTime;
        if (timeToLive < 0)
        {
            DestroyBullet();
        }*/

        if (transform.position.x > maxX)
        {
            DestroyBullet();
        }
        else if (transform.position.x < minX)
        {
            DestroyBullet();
        }
        if (transform.position.y > maxY)
        {
            DestroyBullet();
        }
        else if (transform.position.y < minY)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
