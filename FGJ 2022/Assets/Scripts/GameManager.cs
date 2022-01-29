using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public GameObject playerDown=null;

    public float spawntimer = 0;
    public bool spawntimeractive=true;

    public List<EnemySpawn> enemies;
    public EnemySpawn nextEnemy=null;

    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (spawntimeractive)
        {
            spawntimer += Time.deltaTime;
            
            if (nextEnemy == null&&enemies.Count>0)
            {
                nextEnemy = enemies[0];
                enemies.RemoveAt(0);
            }

            if (nextEnemy != null)
            {
                if (spawntimer >= nextEnemy.spawntime)
                {
                    Instantiate<GameObject>(nextEnemy.enemy);
                    
                }
            }

        }


    }
    public void PlayerDead(GameObject player)
    {
        if (playerDown != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            playerDown = player;
            player.SetActive(false);
            
        }
    }
}
