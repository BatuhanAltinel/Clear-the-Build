using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] patrols;
    [SerializeField] float moveSpeed = 5f;
    int moveTransformIndex;
    bool canMoveRight;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckCanMoveRight();
        MoveTowards();
    }

    void MoveTowards()
    {
        if(canMoveRight)
        {
            moveTransformIndex = 1;
        }else
            moveTransformIndex = 0;

        anim.SetBool("IsWalk",true);
        transform.position = 
            Vector3.MoveTowards(transform.position,patrols[moveTransformIndex].position,moveSpeed*Time.deltaTime);
        LookTarget(patrols[moveTransformIndex].position);
    }

    void CheckCanMoveRight()
    {
        if(Vector3.Distance(transform.position,patrols[0].position) <= 0.5f)
        {
            canMoveRight = true;
            Debug.Log("Move to right");
        }
        else if(Vector3.Distance(transform.position,patrols[1].position) <= 0.5f)
        {
            canMoveRight = false;
            Debug.Log("Move to left");
        }
    }

    void LookTarget(Vector3 newTarget)
    {
        // take amount of difference quaternion
        Quaternion targetRotation = Quaternion.LookRotation(newTarget - transform.position);
        // turning with lerp amount of target quaternion
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,moveSpeed*Time.deltaTime);
    }
}
