using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharContr : MonoBehaviour
{
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = Vector3.zero;
        var inputt = Input.GetAxis("Vertical");

        controller.Move(transform.forward * inputt * 120 * Time.deltaTime);
    }
}
