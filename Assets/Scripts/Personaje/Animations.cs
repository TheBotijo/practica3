using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Animations : MonoBehaviour
{
    [Header("Weapons")]
    public float delayDamage = 2f;
    public float delayAttackMelee;
    public float delayAttackPalo;
    public float delayShoot;
    public GameObject ColliderMano;
    public GameObject ColliderPalo;

    [Header("Player")]
    public bool isMoving;
    public PlayerMoveJump player;
    public AudioSource ClipDamage;
    public AudioSource ClipDeath;
    public AudioSource ClipMano;
    public AudioSource ClipBate;
    public AudioSource ClipPistolaPlayer;
    public Animator Animator;
    public bool Stop=false;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    [Header("Particulas")]
    public ParticleSystem blood;
    

    public HealthSystem Vida;


    void Start()
    {
        Animator = GetComponent<Animator>();
        Invoke("StopFalse", delayDamage);
    }
    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.1f + 0.0f, Ground);
    }
    private void FixedUpdate()
    {
        //--------------------------------------------------
        //Animaci� de Moure's
        //comprovem si s'esta movent amb els eixos
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
            
        } 
        else if (Input.GetAxis("Horizontal") == 0 || Input.GetAxis("Vertical") == 0)
        {
            isMoving = false;
        }
        //si s'esta movent apliquem la animacio corresponent
        if (isMoving && grounded)
        {
            
            Animator.SetBool("Walking", true);
        } 
        else if (!isMoving || !grounded)
        {
            Animator.SetBool("Walking", false);
        }

        //--------------------------------------------------
        //Animaci� de Saltar
        if (Input.GetButton("Jump") && grounded && isMoving)
        {
            Animator.SetBool("JumpMove", true);
            
        }
        else{
            Animator.SetBool("JumpMove", false);
        }
        if (Input.GetButton("Jump") && grounded && !isMoving)
        {
            Animator.SetBool("JumpStatic", true);
           
        }
        else{
            Animator.SetBool("JumpStatic", false);            
        }
        //--------------------------------------------------
        //Animaciones de Ataques
        //Melee
        if (Input.GetButton("Hit") && player.Armas == 0)
        {
            ColliderMano.SetActive(true);
            Stop = true;
            Animator.SetBool("AttackMelee", true);            
            ClipMano.Play();
            Invoke("StopFalse", delayAttackMelee);
        }
        else{
            Animator.SetBool("AttackMelee", false);
        }
        //Palo
        if (Input.GetButton("Hit") && player.Armas == 1)
        {
            ColliderPalo.SetActive(true);
            Stop = true;
            Animator.SetBool("AttackPalo", true);            
            ClipBate.Play();
            Invoke("StopFalse", delayAttackPalo);
        }
        else{
            Animator.SetBool("AttackPalo", false);
        }
        //Arma
        if (Input.GetButtonDown("Hit") && player.Armas == 2)
        {
            Stop = true;
            Animator.SetBool("Shoot", true);
            ClipPistolaPlayer.Play();
            player.Shoot();
            Invoke("StopFalse", delayShoot);
        }
        else{
            Animator.SetBool("Shoot", false);
        }
        //--------------------------------------------------
        //Animacio de Correr
        if (Input.GetButton("Run") && grounded)
        {
            //isRunning = true;
            Animator.SetBool("Running", true);

        }
        else
        {
            Animator.SetBool("Running", false);
            //isRunning = false;
        }     
        
    }
    //--------------------------------------------------
    //Animaci� de daño y muerte
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Colision")
        {
            if (Vida.health<=0){
            ClipDeath.Play();
            //isDeath = true;
            Animator.SetTrigger("Death");
            Stop = true;
            player.GetComponent<PlayerMoveJump>().enabled = false;
                //Se acaba el juego

            }
            else{
            ClipDamage.Play();
            //Recibe daño
            Animator.SetTrigger("Damage");
            blood.Play();
            Stop = true;
            Vida.Damage(10);
            Invoke("StopFalse", delayDamage);
        }    

        }
    }
    public void StopFalse()
    {
        Stop = false;
        ColliderMano.SetActive(false);
        ColliderPalo.SetActive(false);
    }
}
