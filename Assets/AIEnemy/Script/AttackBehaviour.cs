using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public float Speed = 5;
    public float jumpForce = 20;
    public float ChaseMultiplier = 1.5f;
    public float DetectionDistance = 10;
    public float shootDistance = 5;
    [SerializeField, Range(0, 360)]
    public float FOV = 60;

    Transform _player;

    public LayerMask WhatIsGround;
    public LayerMask WhatIsWall;
    public float rayCastOrigin = 0.75f;
    public float wallDistance = 0.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (WallDetected(animator))
        //{
        //    Jump(animator);
        //}
        if (CheckShoot(_player, animator.transform))
        {
            Debug.Log("El poli dispara");
            animator.SetTrigger("Shoot");
            //animator.SetBool("IsChasing", false);
            //animator.SetBool("IsPatrolling", false);
        }else
        {
            CheckTriggers(animator);
            Execute(animator);
        }
        
    }
    private void CheckTriggers(Animator animator)
    {
        if (CheckShoot(_player, animator.transform))
        {
            Debug.Log("El poli dispara");
            animator.SetTrigger("Shoot");
            //animator.SetBool("IsChasing", false);
            //animator.SetBool("IsPatrolling", false);
        }else
        {
            bool isPlayerClose = IsPlayerClose(_player, animator.transform);
                    animator.SetBool("IsChasing", isPlayerClose);
        }
        
    }

    private bool CheckShoot(Transform player, Transform mySelf)
    {
        float dist = Vector3.Distance(player.position, mySelf.position);
        return dist < shootDistance;
    }

    private bool IsPlayerClose(Transform player, Transform mySelf)
    {
        Vector3 direction = _player.position - mySelf.transform.position;
        return Vector3.Distance(player.position, mySelf.position) < DetectionDistance && Vector3.Angle(mySelf.transform.forward, direction) < FOV / 2;
    }
    private void Execute(Animator animator)
    {
        Vector3 posPlayer = new Vector3(_player.position.x, animator.transform.position.y, _player.position.z);
        animator.transform.LookAt(posPlayer);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, posPlayer, Speed * ChaseMultiplier * Time.deltaTime);
    }
    private void Jump(Animator animator)
    {
    }
    private bool EdgeDetected(Animator animator)
    {
        Vector3 originPoint = animator.transform.position + animator.transform.forward * rayCastOrigin;
        return Physics.Raycast(originPoint, animator.transform.TransformDirection(-Vector3.up), WhatIsGround);
    }
    private bool WallDetected(Animator animator)
    {
        RaycastHit hit;
        Vector3 originPoint = animator.transform.position + animator.transform.forward * rayCastOrigin;
        Physics.Raycast(originPoint, animator.transform.TransformDirection(Vector3.forward), out hit, WhatIsWall);
        if (hit.distance <= wallDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
