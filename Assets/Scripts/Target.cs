using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] int maxHealth = 2;
    int currenHealth;
    public int GetHealth
    {
        get {return currenHealth;}
        set {currenHealth = value;
                if(currenHealth > maxHealth)
                    currenHealth = maxHealth;       
            }
    }
    void Start()
    {
        currenHealth = maxHealth;
    }

   
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        BulletMovement bullet = other.gameObject.GetComponent<BulletMovement>();

        if(bullet)
        {
            if(bullet.owner != gameObject)
            {
                Destroy(other.gameObject);
                currenHealth--;
                Debug.Log(gameObject.name +"hp : "+currenHealth);
                Die();    
            }
            
        }
    }

    void Die()
    {
        if(currenHealth <= 0)
            Destroy(gameObject);
    }
}
