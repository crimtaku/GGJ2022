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
    public int enemyIndex = 0;

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

            if (enemyIndex < enemies.Count)
            {
                if (spawntimer >= enemies[enemyIndex].spawntime)
                {
                    //Instantiate<GameObject>(enemies[enemyIndex].enemy);
                    enemyIndex++;
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
