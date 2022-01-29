using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float spawntime;
    public float timeToLive;

    public bool usesTimeToLive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (usesTimeToLive)
        {
            timeToLive -= Time.deltaTime;


            if (timeToLive <= 0)
            {
                DestroyEnemy();
            }
        }
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
