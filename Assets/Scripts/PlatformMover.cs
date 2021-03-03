using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] List<GameObject> platforms = new List<GameObject>();
    [SerializeField] float speed;
    GameObject obj;

    void Start()
    {
        platforms.AddRange(GameObject.FindGameObjectsWithTag("Turnable Platform"));
        
        int i = Random.Range(0, platforms.Count);
        obj = platforms[i];
        

    }


    void Update()
    {
        if(obj.transform.rotation.z <= 90) 
            obj.transform.Rotate(0, 0, 1 * Time.deltaTime * speed);

    }
}
