using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pickup : MonoBehaviour
{
    public Text amountText;
    private int amount;

    public AudioClip pickupSound;

    public GameObject bodyPrefab;
    public SpriteRenderer spriteAtual;

   
    // Start is called before the first frame update
    private void OnEnable()
    {
        amount = Random.Range(1,6);
        amountText.text = amount.ToString();
        GetComponent<SpriteRenderer>().sprite = spriteAtual.sprite;
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);
            LevelController.instance.ContadorPickups(1);
            for (int i = 0; i < amount; i++)
            {
                int index = other.transform.childCount;
                GameObject newBody = Instantiate(bodyPrefab, other.transform);
                newBody.transform.localPosition = new Vector3(0, -4*index, 0);

                FollowTarget followTarget = newBody.GetComponent<FollowTarget>();
                if (followTarget != null)
                {
                    followTarget.target = other.transform.GetChild(index-1);
                }
            }
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SetText(player.transform.childCount);
                LevelController.instance.Score(amount);
            }
            //LevelController.instance.SetActiveContinueLife();
            LevelController.instance.SetFalseContinueLife();

        }
        //desaparece se colidir com qualquer outro objeto
        gameObject.SetActive(false);
    }

}
