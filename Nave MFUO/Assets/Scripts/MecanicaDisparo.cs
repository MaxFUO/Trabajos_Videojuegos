using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MecanicaDisparo : MonoBehaviour
{
    [SerializeField] private float Velocidad_Bala = 3f;
    public GameManager manager;
    public AudioClip audioChoqueAsteroide;

    void Start()
    {
        Destroy(gameObject, 5f);
        manager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        transform.position += Vector3.up * Velocidad_Bala * Time.deltaTime;       
    }

    private void OnCollisionEnter2D(Collision2D otroGameObject)
    {       
        if (otroGameObject.gameObject.tag=="Asteroide")
        {            
            //destruir asteroide
            Destroy(otroGameObject.gameObject);

            manager.numerodestruidos++;
            manager.contadorChoquesparaBuf++;

            //destruir bala
            Destroy(gameObject);    
            //sonido de choque
            AudioSource.PlayClipAtPoint(audioChoqueAsteroide, gameObject.transform.position);         
        }
    }
}
