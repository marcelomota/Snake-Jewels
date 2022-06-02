using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Obstacle : MonoBehaviour
{
    public AudioClip impactSound;
    public Text amountText;


    private int amount;

    private Player player;

    private float nextTime;

    private SpriteRenderer spriteR;

    private void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (player != null && nextTime < Time.time)
        {
            PlayerDammage();
            
            
        }
    }

    public void setAmount() {

        gameObject.SetActive(true);
        amount = Random.Range(0, LevelController.instance.obstaclesAmount);
        if (amount <= 0)
        {
            gameObject.SetActive(false);
        }

        setAmountText();
        setSprite();
    }

    public void setAmountText() {
        amountText.text = amount.ToString();
    }

    public void setSprite() {

        int playerLives= FindObjectOfType<Player>().transform.childCount;
        SpriteRenderer newSprite;
        if (amount > playerLives && playerLives > 20 || amount>20)
        {
            newSprite = LevelController.instance.sprites[4];
        }
        else if (amount <= 20 && amount > 15 )
        {
            newSprite = LevelController.instance.sprites[3];
        }
        else if (amount <= 15 && amount > 10)
        {
            newSprite = LevelController.instance.sprites[2];
        }
        else if (amount <= 10 && amount > 2)
        {
            newSprite = LevelController.instance.sprites[0];
        }
        else {
            newSprite = LevelController.instance.sprites[1];
        }
        spriteR.sprite = newSprite.sprite;
        
    }
    void PlayerDammage() {


        if (LevelController.instance.gameOver)
        {
           
            return;
        }
        

        AudioSource.PlayClipAtPoint(impactSound, Camera.main.transform.position);
        nextTime = Time.time + LevelController.instance.damageTime;
        player.TakeDamage();
        amount--;
        setAmountText();
        if (LevelController.instance.continueLife==true|| amount <= 0)
        {
           
            gameObject.SetActive(false);
            player = null;

           // LevelController.instance.SetFalseContinueLife();
        }
        else {
            //parar as corrotinas pra não dar merda e sobressair e depois iniciar a que a gente quer.
           
            StopAllCoroutines();
            StartCoroutine(DamageColor());
           

        }

       
    }
    IEnumerator DamageColor() {

        float timer = 0;
        float t = 0;

        while (timer<LevelController.instance.damageTime)
        {
            spriteR.color = Color.Lerp(Color.white, Color.black, t);
            timer += Time.deltaTime;
            t = Time.deltaTime / LevelController.instance.damageTime;

            yield return null;
        }

        spriteR.color = Color.white;
    }

    //se colidir causa dano
    private void OnCollisionEnter2D(Collision2D other)
    {
        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            player = otherPlayer;
        }
        //se for diferente de player eu desativo pra n colidir com outras coisas
       

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Player otherPlayer = other.gameObject.GetComponent<Player>();
        if (otherPlayer != null)
        {
            player = otherPlayer;
        }
    }


}
