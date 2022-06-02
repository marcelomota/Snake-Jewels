using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Text pointsText;
    public Text pickupsCollectedText;
    public Text levelAtualText;
    public Text levelAtualTextMenu;
    public Text nextLevelTextMenu;
    

    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    // variavel purchased para compra e selectBall para selecionar

    public GameObject storePanel;

    public GameObject nextLevelMenuPanel;


    public Obstacles[] conjuntoObstaclesDesativar;

    public GameObject jogador;

    // Variável para progresso de barra
    public Slider progressBar;
    public GameObject fillProgressBar;

    public bool gameOver = true;

    public static LevelController instance;
    public float gameSpeed = 15;

    private float percursoBolaProgressBar = 1;

    public float damageTime = 0.1f;
    public float obstacleDistance = 50;
    public int obstaclesAmount = 5;
    public ObjectPool pickupPool;
    public Pickup pickUp;
    public GameObject qtdPickupStoragePanel;

    public float multiplier = 1;
    public float cicleTime = 6;

    public bool continueLife = true;
    private bool ativarNovoLevel = true;

    public Vector2 xLimit;
    public SpriteRenderer[] sprites = new SpriteRenderer[5];

    private Transform player;
    

    public AudioClip clickSound;

    //variaveis que serão salvas no arquivo
    private int points;
    private int pickupsCollected;
    public int ballIndex;
    
    //usado para armazenar a distância percorrida e se caso continue for verdadeiro incrementar 
    
    //level atual
    private int levelAtual;

    public SpriteRenderer[] spritesPlayer;
    public GameObject[] texturesPickup;

    //TESTE
    public Material[] texturasTeste;
    
    private void Awake()
    {

        instance = this;
        
        
        

        if (progressBar.maxValue <= 500)
        {
             
            
            
            
            //carrega os pontos, bolinha e efeito escolhido, além do dinheiro e level
            points = PlayerPrefs.GetInt("points");
            pickupsCollected = PlayerPrefs.GetInt("pickups");
            ballIndex = PlayerPrefs.GetInt("ballIndex");
            levelAtual = PlayerPrefs.GetInt("levelAtual");
            
            pointsText.text = points.ToString();
            pickupsCollectedText.text = pickupsCollected.ToString();
            levelAtualText.text = levelAtual.ToString();
            levelAtualTextMenu.text = levelAtualText.text;

            progressBar.maxValue = 500 + levelAtual;

            TrocarImagemPlayer();
        }

        else
        {
            //carrega os pontos, bolinha e efeito escolhido, além do dinheiro e level
            points = PlayerPrefs.GetInt("points");
            pickupsCollected = PlayerPrefs.GetInt("pickups");
            ballIndex = PlayerPrefs.GetInt("ballIndex");
            levelAtual = PlayerPrefs.GetInt("levelAtual");
            
            pointsText.text = points.ToString();
            pickupsCollectedText.text = pickupsCollected.ToString();
            levelAtualText.text = levelAtual.ToString();
            levelAtualTextMenu.text = levelAtualText.text;
            progressBar.maxValue += levelAtual;

            


            TrocarImagemPlayer();
        }

    }
    // Start is called before the first frame update
    //talvez dê pra usar no CONTINUE
    IEnumerator Start()
    {
        player = FindObjectOfType<Player>().transform;
        while (gameOver)
        {
            CancelInvoke("IncreaseDifficulty");
            yield return null;
        }

        

        SpwanPickups();
        InvokeRepeating("IncreaseDifficulty", cicleTime, cicleTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (continueLife==false) {
            
            //atualiza o valor de progresso da barra
                progressBar.value = FindObjectOfType<Player>().gameObject.transform.position.y;
            //atualiza a posição da bolinha na barra de progresso

            fillProgressBar.transform.localPosition= new Vector3(percursoBolaProgressBar++/5.5f,3,0);
            
            
        }
        //preenche a barra com a distância

        //se ultrapassou a distância sobe de level
        if (FindObjectOfType<Player>()!=null)
        {
            if (progressBar.maxValue <= progressBar.value)//FindObjectOfType<Player>().gameObject.transform.position.y)
            {
                NextLevel();
                percursoBolaProgressBar = 0;
                //fillProgressBar.transform.localPosition = new Vector3(progressBar.minValue, 3, 0);

                //  ativarNovoLevel = false;
            }
        }
        


    }
    public void StartGame() {
        //TRATAR ESSA CONDIÇÃO AQUI
        
        // ESSE LINHA DE CIMA
        AudioSource.PlayClipAtPoint(clickSound, Camera.main.transform.position);
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameOver = false;
    
    }

    public void GameOver() {

        continueLife = true;
       
        gameSpeed = 0;
        gameOver =true;
        

        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("pickups", pickupsCollected);
        PlayerPrefs.SetInt("levelAtual", levelAtual);


        // conjuntoObstaclesDesativar[0].obstaclesGroup.transform.localPosition = player.localPosition + new Vector3(0, 100, 0);
        // conjuntoObstaclesDesativar[1].obstaclesGroup.transform.localPosition = player.localPosition + new Vector3(0, 150, 0);
        //conjuntoObstaclesDesativar[2].obstaclesGroup.transform.localPosition = player.localPosition + new Vector3(0, 200, 0);
        //
        jogador.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
       
    }

    public void ContinueGameOver() {
        
        continueLife = true;
        gameOver = false;
        gameSpeed = 10;
        jogador.SetActive(true);
        

        conjuntoObstaclesDesativar[0].obstaclesGroup.transform.localPosition = player.localPosition + new Vector3(0, 50, 0);
        
       
       
        conjuntoObstaclesDesativar[1].obstaclesGroup.transform.localPosition = player.localPosition + new Vector3(0, 100, 0);
      
       
       
        conjuntoObstaclesDesativar[2].obstaclesGroup.transform.localPosition = player.localPosition + new Vector3(0, 150, 0);
        
        
        

        
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);

        
    }
    public void SetFalseContinueLife() {

        continueLife = false;
    }
    public void SetActiveContinueLife() {
        continueLife = true;
    }

    public void Storage() {

        startPanel.SetActive(false);
        storePanel.SetActive(true);
        
    
    }

    public void Menu() {
        PlayerPrefs.SetInt("ballIndex",ballIndex);
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("pickups", pickupsCollected);
        PlayerPrefs.SetInt("levelAtual", levelAtual);
        
        storePanel.SetActive(false);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        ReloadScene();

    }
    //RELOAD SCENE É CHAMADO APENAS EM MENU PRECISA SALVAR ANTES DE CHAMAR SCENE MANAGER!
    public void ReloadScene() {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    // incrementa o score
    public void Score(int amount) {
        points += amount;

        pointsText.text = points.ToString();

        
    
    }
    //Aumenta a dificuldade do jogo 
    void IncreaseDifficulty() {

        obstaclesAmount += 2;
        multiplier *= 1.1f;

    }
        
    //Gera os objetos que serão coletados
    void SpwanPickups() {
        pickupPool.GetObject().transform.position = new Vector2(Random.Range(xLimit.x, xLimit.y), player.position.y+73);
        Invoke("SpwanPickups", Random.Range(1f, 2.5f));
    }
    //altera o valor dos pickups que forem coletados.
    public void ContadorPickups(int pickups) {
        pickupsCollected += pickups;
        pickupsCollectedText.text = pickupsCollected.ToString();
    }

    public int GetPickUps() {

        return pickupsCollected;
    }
    private void NextLevel() {
        //necessário para acrescentar apenas 1 no level
        if (ativarNovoLevel==false)
        {
            return;
        }
        gameOver = true;
        gameSpeed = 0;

        //progressBar.gameObject.SetActive(false);
        gamePanel.SetActive(false);
        nextLevelMenuPanel.SetActive(true);
        //PRINTAR O PROGRESS BAR AQUI E MAXVALUE

        
        levelAtual++;
        levelAtualText.text = levelAtual.ToString();
        levelAtualTextMenu.text = levelAtualText.text;
        nextLevelTextMenu.text = levelAtualText.text;
        PlayerPrefs.SetInt("levelAtual", levelAtual);

      
        // continueLife = false;
        /*TROCAR WHILE POR IF DENTRO DESSE METODO PARA VER SE FUNCIONA
         INSERIR UM WHILE OU IF SEMELHANTE DENTRO DE 'OBSTACLE' PARA SAIR APÓS O CONTINUE
         */
        ativarNovoLevel = false;
        jogador.SetActive(false);
        //ContinueLevel();
    }

    public void ContinueLevel() {
        progressBar.minValue = progressBar.maxValue;
        progressBar.maxValue = progressBar.maxValue+ 500 + levelAtual;
        ativarNovoLevel = true;
        gameOver = false;
        gameSpeed = 15;
        multiplier = 1;

       // jogador.transform.localPosition = new Vector3(0, -10, 0);
        jogador.SetActive(true);

        conjuntoObstaclesDesativar[0].obstaclesGroup.transform.localPosition = jogador.transform.localPosition + new Vector3(0, 50, 0);
        conjuntoObstaclesDesativar[1].obstaclesGroup.transform.localPosition = jogador.transform.localPosition + new Vector3(0, 100, 0);
        conjuntoObstaclesDesativar[2].obstaclesGroup.transform.localPosition = jogador.transform.localPosition + new Vector3(0, 150, 0);


        gamePanel.SetActive(true);
        nextLevelMenuPanel.SetActive(false);
    }
    public void TrocarImagemPlayer() {
        //pega a imagem no vetor e altera de player SÓ FUNCIONA SE TIVER UM OBJETO JA NA CENA

        FindObjectOfType<Pickup>().spriteAtual = texturesPickup[ballIndex].GetComponent<SpriteRenderer>();
       // for (int i = 0; i < FindObjectOfType<ObjectPool>().prefabs.Length; i++)
        //{
         //   FindObjectOfType<ObjectPool>().prefabs[i].gameObject.GetComponent<SpriteRenderer>().sprite = spritesPlayer[ballIndex].sprite;
       // }
       
        FindObjectOfType<Pickup>().gameObject.GetComponent<SpriteRenderer>().sprite = spritesPlayer[ballIndex].sprite;


        FindObjectOfType<Player>().gameObject.GetComponent<SpriteRenderer>().sprite = spritesPlayer[ballIndex].sprite;
       //altera o corpo da cobra
        FindObjectOfType<Pickup>().bodyPrefab.GetComponent<SpriteRenderer>().sprite = spritesPlayer[ballIndex].sprite;
        //altera apenas a bolinha que surge pra consumir
 //altera a particula do player
       // FindObjectOfType<Player>().particleBlocks = texturesParticles[ballIndex];
        FindObjectOfType<Player>().particleBlocks.GetComponent<Renderer>().material = texturasTeste[ballIndex];
        // FindObjectOfType<Player>().materialParticle.SetTexture("", texturesParticles[ballIndex]);
        qtdPickupStoragePanel.GetComponent<Image>().sprite = spritesPlayer[ballIndex].sprite;
        fillProgressBar.GetComponent<Image>().sprite = spritesPlayer[ballIndex].sprite;
        //pickUp.bodyPrefab.gameObject.GetComponent<SpriteRenderer>().sprite = spritesPlayer[ballIndex].sprite;
        
        //PRECISA RECARREGAR A CENA PARA QUE A LINHA ABAIXO FUNCIONE
        //pickupPool.prefab.gameObject.GetComponent<SpriteRenderer>().sprite = spritesPlayer[ballIndex].sprite;
        //TRABALHANDO NELE
        //FindObjectOfType<Effects>().gameObject.GetComponent<Effects>().SetParticles();
    }
}
