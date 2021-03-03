using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCollision : MonoBehaviour
{
    bool onGround = false;

    private void Start()
    {
        Destroy(this.gameObject, 4.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Colision Trigger")
            this.gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
    }

    private void Update()
    {
        if(gameObject.transform.localScale.magnitude <= 0)
            Destroy(this.gameObject);
        if(onGround == true)
        {
            this.gameObject.transform.localScale.Set(gameObject.transform.localScale.x -
            Time.deltaTime, gameObject.transform.localScale.y - Time.deltaTime,
            gameObject.transform.localScale.z - Time.deltaTime);
        }

        

    }

    private void LateUpdate()
    {
        if(this.gameObject.transform.parent.childCount == 0)
            Destroy(this.gameObject.transform.parent);
    }



}
