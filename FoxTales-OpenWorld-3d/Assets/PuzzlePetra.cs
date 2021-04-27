using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePetra : MonoBehaviour
{
    public GameObject luzPetra;

    public static bool luz3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (luz3 == true)
            luzPetra.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pedra")
        {

            luz3 = true;
        }

    }
}
