using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADamage : MonoBehaviour
{
    public int lives = 3;
    public IAStarFPS iastar;
    public GameObject pontoFraco;
    public GameObject estagioFinal;

    // Start is called before the first frame update






    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (lives == 0)
        {
            iastar.Dead();
            Destroy(gameObject, 1);
        }
        if (lives <= 50)
            pontoFraco.SetActive(true);
        if (lives <= 25)
            estagioFinal.SetActive(true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            lives--;
            iastar.Damage();
        }
        
    }

    public void ExplosionDamage()
    {
        lives--;
    }
}
