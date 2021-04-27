using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoAtivar : MonoBehaviour
{
    public GameObject luz;
    private LuzAtiva luz2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
         {

            luz.gameObject.SetActive(true);
            luz2.ativa = true;

        }
    }
}
