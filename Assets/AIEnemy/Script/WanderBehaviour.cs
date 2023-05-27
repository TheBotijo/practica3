using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderBehaviour : StateMachineBehaviour
{
    public float Speed = 4;
    float _timer;
    public float WaitTime = 2;
    public float DetectionDistance = 10;
    public float rotateSpeed = 3;
    [SerializeField, Range(0, 360)]
    public float FOV = 60;
    public Transform _player;
    Vector3 _dir;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _dir = new Vector3();
        _dir.x = Random.Range(-1f, 1f);
        _dir.y = 0;
        _dir.z = Random.Range(-1f, 1f);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _timer = 0;
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Execute(animator);
        CheckTriggers(animator);
    }
    private void CheckTriggers(Animator animator)
    {
        bool isPlayerClose = IsPlayerClose(_player, animator.transform);
        animator.SetBool("IsChasing", isPlayerClose);

        bool timeUp = IsTimeUp();
        animator.SetBool("IsPatrolling", !timeUp);
    }
    private bool IsTimeUp()
    {
        return _timer > WaitTime;
    }
    private bool IsPlayerClose(Transform player, Transform mySelf)
    {
        Vector3 direction = _player.position - mySelf.transform.position;
        return Vector3.Distance(player.position, mySelf.position) < DetectionDistance && Vector3.Angle(mySelf.transform.forward, direction) < FOV/2;
    }
    private void Execute(Animator animator)
    {
        //Quaternion lookRotation = Quaternion.LookRotation(_dir - animator.transform.position);
        //animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        //Vector3 rotateDirection = Vector3.RotateTowards(animator.transform.position, _dir, Speed * Time.deltaTime, 1);
        //animator.transform.rotation = Quaternion.LookRotation(rotateDirection);
        //Vector3 rotateDirection = new Vector3(_dir.x, 0, _dir.z);
        _timer += Time.deltaTime;
        Vector3 displacement = _dir * Speed * Time.deltaTime;
        animator.transform.Translate(displacement);
        //animator.transform.forward = rotateDirection;
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
