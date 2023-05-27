using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderWaypoint : MonoBehaviour
{
    public float Speed = 4;
    public float rotateSpeed = 5;
    public float DetectionDistance = 10f;
    public float ChaseMultiplier = 1.3f;
    public float shootDistance = 5;
    [SerializeField, Range(0, 360)]
    public float FOV = 60;

    public Transform _player;

    [SerializeField]
    Transform[] waypoints;
    public int targetWaypoint;
    public Animator animator;
    public PlayerMoveJump Shoot;

    [Header("Shoot")]
    public GameObject bullet;
    public Transform spawnBullet;

    public float shootForce = 1500;
    public float shootRate = 0.5f;

    public float range = 100f;

    private float shootRateTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        targetWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerClose(_player, animator.transform))
        {
            if (CheckShoot(_player, animator.transform))
            {
                Debug.Log("El poli dispara");
                animator.SetTrigger("Shoot");
                Shoot.Shoot();
            }else
            {
                Execute(animator);
                CheckTriggers(animator);
            }
            
        }
        else
        {
            NextWaypoint(animator);
            ReachedWaypoint(animator);
            CheckTriggers(animator);
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
        return Vector3.Distance(player.position, mySelf.position) < DetectionDistance && Vector3.Angle(mySelf.transform.forward, direction) < FOV / 2;
    }
    private bool CheckShoot(Transform player, Transform mySelf)
    {
        float dist = Vector3.Distance(player.position, mySelf.position);
        return dist < shootDistance;
    }
    private void NextWaypoint(Animator animator)
    {
        Quaternion lookRotation = Quaternion.LookRotation(waypoints[targetWaypoint].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        //transform.LookAt(waypoints[targetWaypoint]);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[targetWaypoint].position, Speed * Time.deltaTime);
    }

    private void ReachedWaypoint(Animator animator)
    {
        if (transform.position == waypoints[targetWaypoint].position)
        {
            targetWaypoint++;
        }
        if (targetWaypoint == waypoints.Length)
        {
            targetWaypoint = 0;
        }

    }
    private void Execute(Animator animator)
    {
        Vector3 posPlayer = new Vector3(_player.position.x, animator.transform.position.y, _player.position.z);
        animator.transform.LookAt(posPlayer);
        animator.transform.position = Vector3.MoveTowards(animator.transform.position, posPlayer, Speed * ChaseMultiplier * Time.deltaTime);
    }

}
