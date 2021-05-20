using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBucks2 : MonoBehaviour
{
    public GameObject estagio2;
    public IADamage vidas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(vidas.lives <= 25)
        estagio2.SetActive(true);
    }
}
