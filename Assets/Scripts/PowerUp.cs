using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Health Settings")]
    public bool healthPowerUp;
    public int healthAmount = 1;

    [Header("Bullet Settings")]
    public bool bulletpowerUp;
    public int bulletAmount = 5;
    [Header("Transform Settings")]
    public Vector3 turnVector = Vector3.zero;

    [Header("Scale Settings")]
    [SerializeField] float period = 2f;
    [SerializeField] Vector3 scaleVector;
    [SerializeField] float scaleFactor;
    Vector3 startScale;  

    void Start()
    {
        startScale = transform.localScale;

        if(healthPowerUp && bulletpowerUp)
        {
            healthPowerUp = false;
            bulletpowerUp = false;
        }
        else if(healthPowerUp)
            bulletpowerUp = false;
        else if(bulletpowerUp)
            healthPowerUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(turnVector);
        SinusWave();
    }

    private void SinusWave()
    {
        if(period < 0)
        {
            period = 0.1f;
        }
        float cycle = Time.timeSinceLevelLoad / period;
        const float tau = Mathf.PI * 2;

        float sinusWave = Mathf.Sin(cycle * tau);

        scaleFactor = sinusWave/2 + 0.5f;

        Vector3 offset = scaleVector * scaleFactor;

        transform.localScale = startScale + offset;


    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player"))
            return;

        if(healthPowerUp)
        {
            other.gameObject.GetComponent<Target>().GetHealth += healthAmount;
        }
        else if(bulletpowerUp)
        {
            other.gameObject.GetComponent<Attack>().GetBullet += bulletAmount;
        }
        Destroy(gameObject);
    }
}
