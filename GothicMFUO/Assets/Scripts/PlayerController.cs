using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;
    private Rigidbody2D rb;
    private float velx, vely;
    [SerializeField] private float alturaSalto;

    [SerializeField] private Transform groundcheck;       //piso verifica
    [SerializeField] private bool isGround;               //Bool de confirmacion si es suelo
    [SerializeField] private float radioDeteccion;        //Detector del suelo
    [SerializeField] private LayerMask WhatsGround;       //Referencia al layer de Suelo

    private AudioSource audioSource;                      //Referencia al AudioSurce
    [SerializeField] private AudioClip SaltoSound;             //Audio de Salto
    [SerializeField] private AudioClip GolpeSound;            //Audio de Correr

    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundcheck.position, radioDeteccion, WhatsGround);
        if (isGround)
        {
            animator.SetBool("IsJump", false);
        }
        else
        {
            animator.SetBool("IsJump", true);           
            animator.SetBool("IsIdle", false);
        }
        Attack();
    }

    private void FixedUpdate()
    {        
        Movimiento();
        Salto();
        Rotar();     
    }

    private void Salto()
    {
        if (Input.GetButton("Jump") && isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, alturaSalto);
            audioSource.PlayOneShot(SaltoSound);
        }
    }
    private void Movimiento()
    {
        velx = Input.GetAxisRaw("Horizontal");
        vely = rb.velocity.y;
        rb.velocity = new Vector2(velx * Speed, vely);
        if (Input.GetButtonDown("Horizontal"))
        {
            //audioSource.Play();
        }
        else
        {
            //audioSource.Pause();
        }
        if (rb.velocity.x != 0)
        {
            animator.SetBool("IsRun", true);         
            animator.SetBool("IsIdle", false);
        }
        else
        {
            animator.SetBool("IsRun", false);
            animator.SetBool("IsIdle", true);
        }
    }
    private void Rotar()
    {
        if (rb.velocity.x > 0) { transform.localScale = new Vector3(1, 1, 1); }
        else if (rb.velocity.x < 0) { transform.localScale = new Vector3(-1, 1, 1); } 
    }

    public void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("IsAttack", true);
            audioSource.PlayOneShot(GolpeSound);
        }
        else animator.SetBool("IsAttack", false);
    }
}
