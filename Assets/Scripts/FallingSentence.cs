using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSentence : MonoBehaviour
{
    [SerializeField] int count = 3;
    [SerializeField] GameObject prefab;
    [SerializeField] float scaleMult = 2.0f;
    [SerializeField] float waitForNextObject = 1.0f;
    [SerializeField] float xValue, minY, maxY;
    
    Vector2 pos;
    


    void Start()
    {
        StartCoroutine(CreateSentence());
    }


    void Update()
    {
        
    }


    IEnumerator CreateSentence()
    {
        for(int i = 0; i < count; i++)
        {
            float xRandom = Random.Range(-xValue, xValue);
            float yRandom = Random.Range(minY, maxY);
            var sentence = Instantiate(prefab, new Vector2(xRandom, yRandom), Quaternion.identity, gameObject.transform);
            foreach(Transform l in sentence.transform)
            {
                l.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            }
                
            yield return new WaitForSeconds(waitForNextObject);
        }
    }
}
