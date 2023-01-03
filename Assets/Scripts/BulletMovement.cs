using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += -transform.right * bulletSpeed*Time.deltaTime;
        transform.Translate(Vector3.left * bulletSpeed*Time.deltaTime);
    }
}
