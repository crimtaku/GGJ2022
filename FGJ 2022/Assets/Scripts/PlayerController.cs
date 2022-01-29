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

    public GameObject gameManager;

    private PlayerInput playerInput;

    public SpriteRenderer spriterenderer;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
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

            //Menetet��n yksi el�m�
            life--;

            //jos el�m�t on loppu niin kutsutaan gamemanagerin scripti� jossa joko muutetaan player active falseksi tai h�vit��n peli jos toinenkin on alhaalla
            if (life <= 0)
            {
                gameManager.GetComponent<GameManager>().PlayerDead(this.gameObject);                
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
