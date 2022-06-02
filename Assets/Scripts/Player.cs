using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float speed = 25;
    private float mouseDistance;
    private Rigidbody2D rb;
    

    public ParticleSystem particleBlocks;
    public MaterialPropertyBlock materialParticle;

    private bool sliding;
    private int dir;

    public Text livesText;
    //ultima posição para reposicionar com o continue
    private float lastPosY;

    //captura o componente que for instanciado
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        

    }

    // Start is called before the first frame update
    void Start()
    {
        lastPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xPos = worldPoint.x;

        mouseDistance = Mathf.Clamp(xPos - transform.position.x, -1, 1);

        if (transform.position.y > lastPosY + 15) {

            lastPosY = transform.position.y;
        }
    }

    private void FixedUpdate() {
        //se game over for verdadeiro, ta no menu
        if (LevelController.instance.gameOver)
        {
            //LevelController.instance.SetActiveContinueLife();
            return;
        }

        if (!sliding)
        {
            rb.velocity = new Vector2(mouseDistance * speed, LevelController.instance.gameSpeed * LevelController.instance.multiplier);
        }
        else
            rb.velocity = new Vector2(dir * 5, LevelController.instance.gameSpeed * LevelController.instance.multiplier);

    }

    public void TakeDamage() {

        if (LevelController.instance.gameOver && LevelController.instance.continueLife==false)
        {
            
            return;
        }
        //gerar particula


        int child = transform.childCount;
        //zera o contador de continue caso seja necessario



        //se continue for false aparece o game over, se não segue o baba
        if (child <= 1 || LevelController.instance.gameOver==true)
        {

            LevelController.instance.GameOver();

            
        }
        else {
            
            GenerationParticle();
            Destroy(transform.GetChild(child - 1).gameObject);

        }
        SetText(child - 1);

    }

    public void SetText(int amount) {

        livesText.text = amount.ToString();
    }
    //atribui valores para alterar a direção quando houver colisão com as barras
    public void Slide(int direction) {

        sliding = true;
        dir = direction;
        Invoke("SetSlideToFalse", 0.5f);
    }
    //altera o valor de sliding para falso

    public void SetSlideToFalse() {

        sliding = false;
    }

    private ParticleSystem GenerationParticle() {

        ParticleSystem newParticleSystem = Instantiate(particleBlocks, transform.position, Quaternion.identity) as ParticleSystem;

        Destroy(newParticleSystem, newParticleSystem.main.startLifetimeMultiplier);

        return particleBlocks;
    }
    //altera o valor da imagem do jogador
   
}
