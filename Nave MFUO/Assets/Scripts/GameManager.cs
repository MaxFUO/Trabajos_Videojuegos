using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum EstadosdelJuego { PREPARADO, JUGANDO, GAMEOVER}
    public EstadosdelJuego estado;

    public int numeroVidas = 3;
    public int numerodestruidos = 0;
    public int contadorChoquesparaBuf = 0;
    public float temporizador = 5;

    public GameObject player;
    public GameObject generadorAsteroides;

    public Text mensajestxt;
    public Text destruidostxt;
    public Text temporizadortxt;

    void Start()
    {
        estado = EstadosdelJuego.PREPARADO;        
    }

    void Update()
    {
        switch (estado)
        {
            case EstadosdelJuego.PREPARADO:
                mensajestxt.text = "Presione la tecla Espaciadora para Iniciar";
                player.SetActive(false); 
                generadorAsteroides.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    player.SetActive(true);
                    generadorAsteroides.SetActive(true);
                    estado = EstadosdelJuego.JUGANDO;
                }
                break;
            case EstadosdelJuego.JUGANDO:
                mensajestxt.text = "Vidas : " + numeroVidas;
                destruidostxt.text = "Asteroides Destruidos : " + numerodestruidos;
                temporizadortxt.text = "Time : " + temporizador;
                if (numeroVidas <= 0)
                {
                    player.SetActive(false);
                    estado = EstadosdelJuego.GAMEOVER;
                }
                break;
            case EstadosdelJuego.GAMEOVER:
                mensajestxt.text = "GAMEOVER Presione la tecla Espaciadora para volver a Jugar";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(0);
                }               
                break;
            default:
                break;
        }
    }
}
