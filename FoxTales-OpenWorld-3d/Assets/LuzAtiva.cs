using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzAtiva : MonoBehaviour
{
    public bool ativa;
    public GameObject luz;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ativa == true)
        {
            luz.gameObject.SetActive(true);
            
        }
    }
}
