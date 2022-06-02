using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            float direction = (other.transform.position.x - transform.position.x)+5;
            player.Slide((int)Mathf.Sign(direction));
        }

        else if (other.CompareTag("Obstacle")) {
            this.gameObject.SetActive(false);
        
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }  
}
