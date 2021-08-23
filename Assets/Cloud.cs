using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float driftSpeed;
    [SerializeField] float probabilityToChangeDirection;
    [SerializeField] float direction;
    [SerializeField] GameObject leashObject;
    [SerializeField] float leashSlack;
    [SerializeField] float GaussSigmaFactor = 6.0f;
    private float timeLeft;
    float timeTracker = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeTracker += Time.deltaTime;
        
        if (timeTracker >= 1f)
        {
            
            timeTracker = timeTracker % 1f;
            if (Random.Range(0, 1000) / 10 < probabilityToChangeDirection)
                ChangeDirection();
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + Mathf.Cos(direction * Mathf.Deg2Rad) * driftSpeed * Time.deltaTime, gameObject.transform.position.y + Mathf.Sin(direction * Mathf.Deg2Rad) * driftSpeed * Time.deltaTime, 0.0f);        
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {        
        Player player = otherObject.gameObject.GetComponent<Player>();
        if (player != null)
            player.Blind();
    }

    void OnTriggerExit2D(Collider2D otherObject)
    {
        Player player = otherObject.gameObject.GetComponent<Player>();
        if (player != null)
            player.UnBlind();
    }

    private void ChangeDirection()
    {
        if(GetLeashDistance() > leashSlack)
        {
            direction = Mathf.Atan2(leashObject.transform.position.y - gameObject.transform.position.y, leashObject.transform.position.x - gameObject.transform.position.x) * 180 / Mathf.PI;
            if (direction < 0)
                direction += 360;                        
        }
        float s = RandomGaussian(0, 360);
        direction = direction - 180 + s;
        if (direction < 0)
            direction += 360;
    }


    private float GetLeashDistance()
    {
        return Mathf.Abs(gameObject.transform.position.x - leashObject.transform.position.x) + Mathf.Abs(gameObject.transform.position.y - leashObject.transform.position.y);
    }

    private float GetLeashDirection()
    {
        Vector2.Angle(gameObject.transform.position, leashObject.transform.position);
        
        return 0.0f;
    }

    public float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / GaussSigmaFactor;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

}
