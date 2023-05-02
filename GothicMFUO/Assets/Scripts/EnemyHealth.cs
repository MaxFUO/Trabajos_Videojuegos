using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    ControlEnemy enemy;
    public GameObject enemy_death;
    public bool isDamaged = false;

    private AudioSource audioSource;

    SpriteRenderer sprite;
    Efectos material;

    void Start()
    {
        enemy= GetComponent<ControlEnemy>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Efectos>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            //Ejecucion en paralelo
            StartCoroutine(Damager());
            enemy.healthPoints -= 1;
            if (enemy.healthPoints <= 0)
            {                
                Instantiate(enemy_death, this.transform.position, Quaternion.identity);              
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        
    }

    IEnumerator Damager()
    {
        isDamaged = true;
        sprite.material = material.blick;      
        audioSource.Play();
        yield return new WaitForSeconds(0.4f);
        isDamaged = false;
        sprite.material = material.original;
    }
}
