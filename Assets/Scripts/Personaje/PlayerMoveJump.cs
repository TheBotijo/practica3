using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMoveJump : MonoBehaviour
{
    //Rigidbody del player = rb
    Rigidbody rb;

    //Definim variables de Moviment
    [Header("Movement")]
    public float moveSpeed;

    float horizontalInput;
    float verticalInput;
    public Transform orientation;

    Vector3 moveDirection;

    //Variables de Detecci� Ground
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;
    public float groundDrag;

    [Header("Particulas")]
    public ParticleSystem sprint;

    //Variables de Jump
    /*[Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;*/

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float jumpTime;
    bool readyToJump;

    public Animations scriptAnimations;

    [Header("Weapons")]
    public GameObject Palo;
    public GameObject Pistola;
    public int Armas = 0;
    public float cooldown;
    public float lastChange;

    [Header("Shoot")]
    public GameObject bullet;
    public Transform spawnBullet;
    public Camera fpsCam;

    public float shootForce = 1500;
    public float shootRate = 0.5f;

    public float range = 100f;

    private float shootRateTime = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;

        readyToJump = true;
    }

    private void Update()
    {
        //per comprovar si toca terra amb un vector de la meitat de l'altura del personatge + un marge
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.1f, Ground);

        UserInput();
        SpeedControl();

        //comprovem si toca el terra per aplicar un fregament al player
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }
    private void FixedUpdate()
    {
        if (scriptAnimations.Stop == false)
        {
            PlayMove();
        }

    }
    private void UserInput()
    {
        //recollir inputs de moviment en els eixos
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void PlayMove()
    {

        
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //moure's seguint el empty orientaci� endavant el eix vertical i orientaci� dreta el eix horitzontal
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
            //apliquem una for�a al moviment quan esta tocant al terra
            if(grounded && Input.GetButton("Run"))
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 40f, ForceMode.Force);
            sprint.Play();
        }
            else if (grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 15f, ForceMode.Force);
            }
            else if (!grounded && flatVel.magnitude > (moveSpeed *16)) //a l'aire
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 40f * airMultiplier, ForceMode.Force);
            
        }
            else if (!grounded)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 15f * airMultiplier, ForceMode.Force);
            }        
            //Cambio de Arma
            if (Input.GetButton("CambioArma"))
            {
                if (Time.time - lastChange >= cooldown)
                {
                    lastChange = Time.time;
                    Armas++;
                    if (Armas == 3) { Armas = 0; }
                    if (Armas <= 2)
                    {
                        switch (Armas)
                        {//Puño
                            case 0:
                                Palo.SetActive(false);
                                Pistola.SetActive(false);
                                break;
                            //Palo
                            case 1:
                                Palo.SetActive(true);
                                Pistola.SetActive(false);
                                break;
                            //Pistola
                            case 2:
                                Palo.SetActive(false);
                                Pistola.SetActive(true);
                                
                            break;
                        }
                    }
                    else
                    {//vuelve a empezar
                        Armas = 0;
                    }
                }

            }
        

    }
    public void Shoot()
    {
        if (Time.time > shootRateTime)
        {
            //Debug.Log("Disparar2");
            GameObject newBullet;
            Raycast();
            newBullet = Instantiate(bullet, spawnBullet.position, spawnBullet.rotation);

            newBullet.GetComponent<Rigidbody>().AddForce(spawnBullet.forward * shootForce);

            shootRateTime = Time.deltaTime * shootRate;

            Destroy(newBullet, 2);
        }
    }

    public void Raycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log("Disparar3");
            //Debug.Log(hit.transform.name);
        }
    }



    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limitar la velocitat si aquesta es mes gran del que volem aconseguir
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            //tornem a aplicar aquesta nova velocitat al player
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // resetejar la velocitat de y per saltar sempre igual
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z) * Time.deltaTime;
        jumpTime += Time.deltaTime;
        //apliquem impulse perque es nomes una vegada
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    
}