using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTemplo : MonoBehaviour
{
    public GameObject luzAnubis;

    public static bool luz2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(luz2 == true)
            luzAnubis.SetActive(true);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coracao")
        {
            
            luz2 = true;
        }        

    }
}
