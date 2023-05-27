using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    public Transform _player;
    float _timer;
    public float DetectionDistance = 10;
    public float WaitTime = 2;
    public float shootDistance = 5;

    [SerializeField, Range(0, 360)]
    public float FOV = 60;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _timer = 0; 
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckTriggers(animator);
        Execute();

    }
    private void Execute()
    {
        _timer += Time.deltaTime;
    }
    private void CheckTriggers(Animator animator)
    {
            bool isPlayerClose = IsPlayerClose(_player, animator.transform);
            animator.SetBool("IsChasing", isPlayerClose);

            bool timeUp = IsTimeUp();
            animator.SetBool("IsPatrolling", timeUp);
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
    private bool CheckShoot(Transform player, Transform mySelf)
    {
        float dist = Vector3.Distance(player.position, mySelf.position);
        return dist < shootDistance;
    }
    private Vector2 PointForAngle(float medioFOV, float detectionDistance)
    {
        return new Vector2(Mathf.Cos(medioFOV * Mathf.Deg2Rad), Mathf.Sin(medioFOV * Mathf.Deg2Rad)) * detectionDistance;
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
