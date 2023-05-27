using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderEdgeDetection : StateMachineBehaviour
{
    public float Speed = 4;
    public float rotateSpeed = 3;
    public float DetectionDistance = 10f;
    public float ChaseMultiplier = 0.5f;
    [SerializeField, Range(0, 360)]
    public float FOV = 60;

    public Transform _player;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsWall;
    public float rayCastOrigin = 0.75f;
    public float wallDistance = 0.5f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!EdgeDetected(animator) || WallDetected(animator))
        {
            Flip(animator);
        }
        Move(animator);
        CheckTriggers(animator);


    }
    private void Flip(Animator animator)
    {
        animator.transform.Rotate(0, Random.Range(90, 270), 0);
        //animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation, Quaternion.Euler(0, 180, 0), rotateSpeed * Time.deltaTime);
    }

    private void Move(Animator animator)
    {
        animator.transform.Translate(animator.transform.forward * Time.deltaTime * Speed, Space.World);
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
        }else
        {
            return false;
        }
    }
    private void CheckTriggers(Animator animator)
    {
        bool isPlayerClose = IsPlayerClose(_player, animator.transform);
        animator.SetBool("IsChasing", isPlayerClose);
    }
    private bool IsPlayerClose(Transform player, Transform mySelf)
    {
        Vector3 direction = _player.position - mySelf.transform.position;
        return Vector3.Distance(player.position, mySelf.position) < DetectionDistance && Vector3.Angle(mySelf.transform.forward, direction) < FOV/2;
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
