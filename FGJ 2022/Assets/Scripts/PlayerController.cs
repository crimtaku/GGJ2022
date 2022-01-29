using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Vector2 move;

    public float speed;
    public float focusedSpeed;
    public float life=2.1f;

    public float invulnerabilityTimer=0;

    public bool isPlayer1;

    public GameObject playerGlow;

    public GameObject gameManagerObject;
    private GameManager gameManager;

    private PlayerInput playerInput;

    public SpriteRenderer spriterenderer;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (invulnerabilityTimer > 0)
        {
            invulnerabilityTimer -= Time.deltaTime;
        }
        else
        {
            spriterenderer.color = Color.white;
        }

        move = playerInput.actions["Move"].ReadValue<Vector2>();
               
        if (move.x > 0 && transform.position.x > gameManager.maxX)
        {
            move.x = 0;
        }
        else if (move.x < 0 && transform.position.x < gameManager.minX)
        {
            move.x = 0;
        }
        if (move.y > 0 && transform.position.y > gameManager.maxY)
        {
            move.y = 0;
        }
        else if (move.y < 0 && transform.position.y < gameManager.minY)
        {
            move.y = 0;
        }

        transform.Translate(move.x*Time.deltaTime*speed, move.y * Time.deltaTime*speed,0);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //ei optimi mutta toimii testaus vaiheessa, lataa uudestaan skenen
        Bullet collidingBullet = other.gameObject.GetComponent<Bullet>();
        
        //pelaaja ei ota damagea omista luodeista
        if (!collidingBullet.isplayerbullet&&invulnerabilityTimer<=0)
        {
            //annetaan yhden sekunnin kuolemattomuus
            invulnerabilityTimer = 1;
            spriterenderer.color = Color.gray;

            //Menetetään yksi elämä
            life--;

            //jos elämät on loppu niin kutsutaan gamemanagerin scriptiä jossa joko muutetaan player active falseksi tai hävitään peli jos toinenkin on alhaalla
            if (life <= 0)
            {
                gameManager.PlayerDead(this.gameObject);                
            }           
        }

        /*Bullet collidingBullet = other.gameObject.GetComponent<Bullet>();
        if (collidingBullet.hurtsBlue = false && isblue)
        {
            collidingBullet.DestroyBullet();
        }
        else if (collidingBullet.hurtsRed = false && isred)
        {
            collidingBullet.DestroyBullet();
        }*/
    }
}
