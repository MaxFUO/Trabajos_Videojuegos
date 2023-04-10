using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class GenradorAsteroides : MonoBehaviour
{
    public GameObject Asteroideprefab;
    public Transform[] generadorPuntos;
    public float VelocidadGeneracion;

    void Start()
    {
        InvokeRepeating("GeneradorAsteroide", VelocidadGeneracion, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GeneradorAsteroide()
    {
        Instantiate(Asteroideprefab, generadorPuntos[Random.Range(0,generadorPuntos.Length)].position, Quaternion.identity);
    }
}
