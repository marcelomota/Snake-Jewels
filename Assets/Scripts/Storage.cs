using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    //classe usada para selecionar as bolinahs  da loja
    public int ballIndex;

    private bool purchased = true;

    public int pickUpsCollecteds;
    public string ballName;
   // public Image spriteBlock;
    public Text ballCoust;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
            
       
        purchased = PlayerPrefs.HasKey(ballName);
        
        if (!purchased)
        {//se não tiver comprado imagem bloqueando
           
            ballCoust.text = pickUpsCollecteds.ToString();
           // spriteBlock.gameObject.SetActive(true);
        }
        else
        {

            //se tiver comprado ta tudo liberado
            //TA TUDO ERRADO NESSA DESGRAÇA ESSA LINHA ABAIXO 
            pickUpsCollecteds = 0;
            GetComponent<Image>().sprite = LevelController.instance.spritesPlayer[ballIndex].sprite;
            ballCoust.gameObject.SetActive(false);
           // spriteBlock.gameObject.GetComponent<Image>().gameObject.SetActive(false);
           // Destroy(spriteBlock.sprite);
            //spriteBlock.gameObject.SetActive(false);
            // LevelController.instance.TrocarImagemPlayer();
            ChoisePlayer();
        }
    }

    public void ChoisePlayer() {
       
        if (purchased)
        {
            
            PlayerPrefs.SetInt("ballIndex", ballIndex);
            LevelController.instance.ballIndex = ballIndex;
            
            ballCoust.gameObject.SetActive(false);
            // spriteBlock.sprite = ballPurchasedImage.sprite;
            GetComponent<Image>().sprite = LevelController.instance.spritesPlayer[ballIndex].sprite;
            pickUpsCollecteds = 0;
            // Destroy(spriteBlock.sprite);
            // spriteBlock.gameObject.GetComponent<Image>().gameObject.SetActive(false);
            // spriteBlock.gameObject.SetActive(false);
            LevelController.instance.TrocarImagemPlayer();

        }
        else if (LevelController.instance.GetPickUps()>=pickUpsCollecteds) {
            
            //salva a string
            PlayerPrefs.SetString(ballName, ballName);
            PlayerPrefs.SetInt("ballIndex", ballIndex);
            //armazena o valor da posição do vetor que ta a bola
            LevelController.instance.ballIndex = ballIndex;
            //troca a imagem

            //reduz o valor e ja troca a imagem
            GetComponent<Image>().sprite = LevelController.instance.spritesPlayer[ballIndex].sprite;
            ballCoust.gameObject.SetActive(false);
           // spriteBlock.sprite = ballPurchasedImage.sprite;
            LevelController.instance.ContadorPickups(-1 * pickUpsCollecteds);
            // Destroy(spriteBlock.sprite);
           // spriteBlock.gameObject.GetComponent<Image>().gameObject.SetActive(false);
            //spriteBlock.gameObject.SetActive(false);
            LevelController.instance.TrocarImagemPlayer();
            pickUpsCollecteds = 0;
        }
        
    }
    
}
