using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float speedMin = 100;
    public float speedAverage = 150;
    public float speedMax = 200;

    private float initialHeight;
    // Start is called before the first frame update
    void Start()
    {
        initialHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(target.position.x - transform.position.x);
        float newSpeed;
        float pct = (distance/2);

        newSpeed = (speedAverage * pct) + (speedMin * pct);
        if (distance>2)
        {
            newSpeed = speedMax;
        }

        Vector2 newPos = new Vector2(target.position.x, transform.position.x+pct);
        transform.position = Vector2.MoveTowards(transform.position, newPos, newSpeed*Time.deltaTime*5);

        transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, initialHeight, initialHeight + pct));
    }
}
