using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokioGhoulGame : MonoBehaviour
{
    int num = 1000;
    int dec = 7;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ghoul());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    IEnumerator ghoul() 
    {
        while (true) 
        {
            if (num <= 0)
            {
                num = 0;
                Debug.LogError("GHOULLLL");
                break;
            }
            Debug.Log($"{num -= dec}");
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
