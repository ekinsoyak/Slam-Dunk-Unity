using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    [SerializeField] GameManager _GameManager;
    [SerializeField] AudioSource TopSesi;

    private void OnTriggerEnter(Collider other) {

        TopSesi.Play();
        
        if(other.CompareTag("Basket")){

            _GameManager.Basket(transform.position);

        }else if(other.CompareTag("OyunBitti")){

            _GameManager.Kaybettin();

        }

    }

    private void OnCollisionEnter(Collision other) {
        TopSesi.Play();       
    }
}
