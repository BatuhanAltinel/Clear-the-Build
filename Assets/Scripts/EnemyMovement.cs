using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform[] patrols;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float shootRange = 10f;
    [SerializeField] float reloadTime = 5f;
    [SerializeField] LayerMask shootLayer;
    [SerializeField] Transform aimTransform;
    int moveTransformIndex;
    bool canMoveRight;
    bool isReloaded = false;
    Animator anim;
    Attack attack;
    void Start()
    {
        attack = GetComponent<Attack>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyShoot();
        CheckCanMoveRight();
        MoveTowards();
        Aim();
    }

    void Reload()
    {
        attack.GetBullet = attack.GetMaxBulletAmount;
        isReloaded = false;
    }
    private void EnemyShoot()
    {
        if(attack.GetBullet <= 0 && !isReloaded)
        {
            Invoke(nameof(Reload),reloadTime);
            isReloaded = true;
        }

        if(attack.GetCurrentFireRate <= 0f && attack.GetBullet > 0 && Aim())
        {
            attack.Fire();
        }
    }

    private bool Aim()
    {
        int directionNum = 0;

        if(canMoveRight)
            directionNum = 1;
        else
            directionNum = -1;

        Vector3 dir = new Vector3(directionNum,0,0);
        bool hit = Physics.Raycast(aimTransform.position,dir,shootRange,shootLayer);

        Debug.DrawRay(aimTransform.position,dir * shootRange,Color.blue);
        return hit;
    }

    void MoveTowards()
    {
        if(Aim() && attack.GetBullet > 0)
        {
            anim.SetBool("IsWalk",false);
            return;
        }
            
        if(canMoveRight)
        {
            moveTransformIndex = 1;
        }else
            moveTransformIndex = 0;

        anim.SetBool("IsWalk",true);

        Vector3 newTargetPos = new Vector3(patrols[moveTransformIndex].position.x,transform.position.y,patrols[moveTransformIndex].position.z);

        transform.position = 
            Vector3.MoveTowards(transform.position,newTargetPos,moveSpeed*Time.deltaTime);
        LookTarget(patrols[moveTransformIndex].position);
    }

    void CheckCanMoveRight()
    {
        if(Vector3.Distance(transform.position,patrols[0].position) <= 1f)
        {
            canMoveRight = true;
        }
        else if(Vector3.Distance(transform.position,patrols[1].position) <= 1f)
        {
            canMoveRight = false;
        }
    }

    void LookTarget(Vector3 newTarget)
    {
        // solution for Y Axis issue
        Vector3 newTargetPos = new Vector3(newTarget.x,transform.position.y,newTarget.z);
        // take amount of difference quaternion
        Quaternion targetRotation = Quaternion.LookRotation(newTargetPos - transform.position);
        // turning with lerp amount of target quaternion
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,moveSpeed*Time.deltaTime);
    }
}
