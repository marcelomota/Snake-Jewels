using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int amount;

    public GameObject[] prefabs;
    private int index;
    // Start is called before the first frame update
    void Awake()
    {
        prefabs = new GameObject[amount];
        
        for (int i = 0; i < amount; i++)
        {
            prefabs[i] = Instantiate(prefab, new Vector2(0, 15), Quaternion.identity);
           
            prefabs[i].SetActive(false);
            
        }
         GetObject(); //TESTANDO ELA OU GET
        //LevelController.instance.Invoke("SpawnPickups",0.05f);
       // prefab.gameObject.GetComponent<SpriteRenderer>().sprite = LevelController.instance.spritesPlayer[LevelController.instance.ballIndex].sprite;
      //  prefabs[index].gameObject.GetComponent<SpriteRenderer>().sprite = LevelController.instance.spritesPlayer[LevelController.instance.ballIndex].sprite;
    }

    public GameObject GetObject() {
        //prefab.gameObject.GetComponent<SpriteRenderer>().sprite = LevelController.instance.spritesPlayer[LevelController.instance.ballIndex].sprite;
       
            prefabs[index].GetComponent<SpriteRenderer>().sprite = LevelController.instance.spritesPlayer[LevelController.instance.ballIndex].sprite;
       
        
        index++;
       //prefabs[index].gameObject.GetComponent<SpriteRenderer>().sprite = LevelController.instance.spritesPlayer[LevelController.instance.ballIndex].sprite;
        if (index >=  amount)
        {
            index = 0;
        }

        /* FUNCIONANDO PORÉM TEM QUE RECARREGAR A CENA ESTÁ IMPLEMENTADO EM LEVEL CONTROLLER*/// prefab.gameObject.GetComponent<SpriteRenderer>().sprite = LevelController.instance.spritesPlayer[LevelController.instance.ballIndex].sprite;

        prefabs[index].SetActive(true);
        return prefabs[index];
    }
}
