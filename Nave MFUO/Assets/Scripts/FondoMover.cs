using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMover : MonoBehaviour
{
    [SerializeField] private float velocidad;
    public Renderer fondo;
    public GameManager manager;
    public MeshRenderer meshRenderer;

    void Start()
    {
        fondo = GetComponent<Renderer>();
        manager = FindObjectOfType<GameManager>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        Vector2 desplazamiento = new Vector2(0, Time.time * velocidad);
        fondo.material.mainTextureOffset = desplazamiento;
        if (manager.numerodestruidos >= 20)
        {
            foreach (Material material in meshRenderer.materials)
            {
                // Aquí puede modificar el estilo de cada material individual
                material.name.Contains("Fondo2");
                material.mainTextureOffset = desplazamiento;
            }
        }
    }
}
