using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBabilonia : MonoBehaviour
{
    public GameObject luz;
    public static bool luzAtivaBabilonia;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (luzAtivaBabilonia == true)
        {
            luz.SetActive(true);

        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            luzAtivaBabilonia = true;
    }
}
