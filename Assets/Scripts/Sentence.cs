using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentence : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] float waitTime = 15.0f;
    [SerializeField] private GameObject skill;
    SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color brightRed;
    private Color brightBlue;
    
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        brightRed = new Color(255, 0, 0);
        brightBlue = new Color(0, 0, 255);
    }

    void Update()
    {
      
    }


    public void ChangeColor()
    {
        spriteRenderer.color = brightRed;
    }

    public void ReturnColor()
    {
        spriteRenderer.color = originalColor;
    }

    public float GetWaitTime()
    {
        return waitTime;
    }

    public GameObject GetSkill()
    {
        return skill;
    }

    public GameObject GetText()
    {
        return text;
    }

    public void ChangeColorBlue()
    {
        spriteRenderer.color = brightBlue;
    }

}
