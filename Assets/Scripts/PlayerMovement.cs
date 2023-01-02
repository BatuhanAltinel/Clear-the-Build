using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]float moveSpeed = 3f;
    [SerializeField] float jumpPower = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        MoveInputs();
    }

    void MoveInputs()
    {
        if(Input.GetKey("a"))
        {
            rb.velocity = new Vector3((-moveSpeed *100)* Time.deltaTime,rb.velocity.y ,0);
        }
        else if(Input.GetKey("d"))
        {
            rb.velocity = new Vector3((moveSpeed * 100) * Time.deltaTime,rb.velocity.y ,0);
        }

        if(Input.GetKey("space"))
        {
            rb.velocity = new Vector3(rb.velocity.x ,(jumpPower * 100) * Time.deltaTime,0);
        }
    }
}
