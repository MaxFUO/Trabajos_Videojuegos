using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class ControladorNave : MonoBehaviour
{
    [SerializeField] private float VelocidadMovimiento;
    [SerializeField] private GameObject bala;
    public Transform posicionBala;
    public Transform posicionBala1;
    public Transform posicionBala2;
    public AudioSource audioSource;
    public AudioClip audioExplosion;
    public AudioClip audioChoque;
    public Sprite nuevoSprite;

    private SpriteRenderer sr;
    private bool detenerGeneracion = false;

    private float Segundos;
    private int contadorSegundos = 5;

    public GameManager manager;

    private int vidas = 3;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        manager.temporizador = contadorSegundos;
        if (detenerGeneracion)
        {
            Segundos += Time.deltaTime;
            if (Segundos >= 1f)
            {
                Segundos = 0;
                contadorSegundos--;               
            }
        }
        if (contadorSegundos <= 0)
        {
            contadorSegundos = 5;
            manager.contadorChoquesparaBuf = 0;
            detenerGeneracion = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dispararBala();           
            audioSource.Play();
            if (detenerGeneracion)
            {
                Buff1();
                audioSource.Play();
                audioSource.Play();
            }
        }
        if (manager.numerodestruidos >= 20)
        {
            sr.sprite = nuevoSprite;
        }        
    }

    private void dispararBala()
    {
        //mediante paso valor interface
        //en el script NaveControl se socie bala y posisicionbala
        if (!detenerGeneracion)
        {
            Instantiate(bala, posicionBala.position, Quaternion.identity);
        }
        if (manager.contadorChoquesparaBuf >= 5)
        {
            DetenerGeneracion();         
        }
    }

    private void Buff1()
    {
        Instantiate(bala, posicionBala1.position, Quaternion.identity);
        Instantiate(bala, posicionBala2.position, Quaternion.identity);
    }

    void FixedUpdate()
    {
        float MavHorizontal = Input.GetAxis("Horizontal");
        float MovVertical = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + new Vector2(MavHorizontal, MovVertical) * VelocidadMovimiento * Time.deltaTime);      
    }
    private void OnCollisionEnter2D(Collision2D resivirdano)
    {
        if(resivirdano.gameObject.tag == "Asteroide")
        {           
            Destroy(resivirdano.gameObject);
            manager.numeroVidas--;
            AudioSource.PlayClipAtPoint(audioChoque, gameObject.transform.position);
            if (vidas <= 0)
            {
                AudioSource.PlayClipAtPoint(audioExplosion, gameObject.transform.position);
            }
        }
        
    }
    private void DetenerGeneracion()
    {
        detenerGeneracion = true;
    }
}
