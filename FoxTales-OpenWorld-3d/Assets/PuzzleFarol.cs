using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFarol : MonoBehaviour
{
    public GameObject luz;
    
    public static bool luzAtiva;
    // Start is called before the first frame update

    public void Awake()
    {
       //DontDestroyOnLoad(this.gameObject); nunca mais uso essa porra na vida KKKKKKKKKKKK
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(luzAtiva==true)
        {
            luz.SetActive(true);
            
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            luzAtiva = true;
    }
}
