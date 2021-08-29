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
    [SerializeField] float directionUpdateFrequency = 1.0f;
    private float timeLeft;
    float timeTracker = 0f;
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = this.transform.position;
        if (direction == -1)
        {
            direction = Random.Range(0, 360);
        }
    }

    // Update is called once per frame
    void Update()
    {

        timeTracker += Time.deltaTime;
        
        if (timeTracker >= directionUpdateFrequency)
        {
            
            timeTracker = timeTracker % directionUpdateFrequency;
            if (Random.Range(0, 1000) / 10 < probabilityToChangeDirection)
                ChangeDirection();
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + Mathf.Cos(direction * Mathf.Deg2Rad) * driftSpeed * Time.deltaTime, gameObject.transform.position.y + Mathf.Sin(direction * Mathf.Deg2Rad) * driftSpeed * Time.deltaTime, 0.0f);        
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        Player player = otherObject.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Blind();
        }
    }

    void OnTriggerExit2D(Collider2D otherObject)
    {
        Player player = otherObject.gameObject.GetComponent<Player>();
        if (player != null)
            player.UnBlind();
    }

    Vector3 GetLeashPosition()
    {
        if (leashObject)
        {
            return leashObject.transform.position;
        }

        return spawnPosition;
    }

    private void ChangeDirection()
    {
        Vector3 leashPosition = GetLeashPosition();

        if(GetLeashDistance() > leashSlack)
        {
            direction = Mathf.Atan2(leashPosition.y - gameObject.transform.position.y, leashPosition.x - gameObject.transform.position.x) * 180 / Mathf.PI;
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
        Vector3 leashPosition = GetLeashPosition();
        return Mathf.Abs(gameObject.transform.position.x - leashPosition.x) + Mathf.Abs(gameObject.transform.position.y - leashPosition.y);
    }

    private float GetLeashDirection()
    {
        Vector3 leashPosition = GetLeashPosition();
        Vector2.Angle(gameObject.transform.position, leashPosition);
        
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
