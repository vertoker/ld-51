using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Features.Character.Service;
using Zenject;

public class PlaceholderSpawner : MonoBehaviour
{
    [Inject]
    private CharacterSpawnService service;
    // Start is called before the first frame update
    void Start()
    {
        service.SpawnCharacter(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxisRaw("Mouse Y"));
    }
}
