using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    [SerializeField]float moveSpeed = 3f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float turnSpeed = 15f;
    [SerializeField] Transform[] groundCheckPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        MoveInputs();
        print(IsOnGround());
    }

    void MoveInputs()
    {
        if(Input.GetKey("space") && IsOnGround())
        {
            rb.velocity = new Vector3(rb.velocity.x ,Mathf.Clamp((jumpPower * 100) * Time.deltaTime,0f,15f),0);
            anim.SetBool("IsWalk",false);
            anim.SetBool("IsJump",true);
            StartCoroutine(StopJumpAnimation());
        }
        if(Input.GetKey("a"))
        {
            rb.velocity = new Vector3(Mathf.Clamp((-moveSpeed *100)* Time.deltaTime,-15f,0f),rb.velocity.y ,0);
            // transform.rotation = Quaternion.Euler(0f,-90f,0f);
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,-90.1f,0),turnSpeed*Time.deltaTime);
            anim.SetBool("IsWalk",true);
        }
        else if(Input.GetKey("d"))
        {
            rb.velocity = new Vector3(Mathf.Clamp((moveSpeed * 100) * Time.deltaTime,0f,15f),rb.velocity.y ,0);
            // transform.rotation = Quaternion.Euler(0f,90f,0f);
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,90.1f,0),turnSpeed*Time.deltaTime);
            anim.SetBool("IsWalk",true);
        }
        else
        {
            anim.SetBool("IsWalk",false);
            rb.velocity = new Vector3(0f,rb.velocity.y,0f);
        }
        
    }

    bool IsOnGround()
    {
        bool hit = false;
        for (int i = 0; i < groundCheckPoints.Length; i++)
        {
            hit = Physics.Raycast(groundCheckPoints[i].position,-groundCheckPoints[i].transform.up,0.5f);
            Debug.DrawRay(groundCheckPoints[i].position,-groundCheckPoints[i].transform.up*0.5f,Color.red);
        }
        if(hit)
        {
            return true;
        }
        return false;
    }

    IEnumerator StopJumpAnimation()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("IsJump",false);
    }
}
