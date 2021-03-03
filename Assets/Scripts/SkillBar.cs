using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBar : MonoBehaviour
{
    [SerializeField] GameObject fightScene;
    [SerializeField] GameObject dialogueBox;
    bool canUseSkill = true;
    [SerializeField] List<GameObject> sentences = new List<GameObject>();
    [SerializeField] float waitForColorChange = 2.0f;
    float colorTimer = 0;
    float skilltimer = 5;
    int oldOne = 0;
    private GameObject activeSkill;
    int index = 0;

    void Start()
    {
        dialogueBox.SetActive(false);
        foreach(Transform c in gameObject.transform)
        {
            if(c.tag == "Sentence")
            {
                sentences.Add(c.gameObject);
                c.gameObject.GetComponent<Sentence>().GetText().SetActive(false);
            }
        }
        SpawnFirstSkill();
    }

    // Update is called once per frame
    void Update()
    {
        if(sentences.Count == 0)
        {
            GameManager.Instance.WonFight();
        }
        colorTimer -= Time.deltaTime;
        skilltimer -= Time.deltaTime;
        if(colorTimer < 0 && canUseSkill)
        {
            colorTimer = waitForColorChange;
            int i = Random.Range(0, sentences.Count);
            if(oldOne == i)
            {
                if(i == 0)
                    i += 1;
                else i -= 1;
            }
          
            sentences[i].GetComponent<Sentence>().ChangeColor();
            StartCoroutine(WaitForColorChange(sentences[i]));   
            oldOne = i;
            index = i;
            UseSkill(sentences[i]);
            canUseSkill = false;
            StartCoroutine(DestroySkill(activeSkill, sentences[i]));
        }

    }


    public void UseSkill(GameObject sentence)
    {
        ShowText(sentence);
        Sentence comp = sentence.GetComponent<Sentence>();
        GameObject newSkill = Instantiate(comp.GetSkill(), comp.GetSkill().transform.position, comp.GetSkill().transform.rotation);
        activeSkill = newSkill;    }

    public GameObject GetActiveSkill()
    {
        return activeSkill;
    }

    IEnumerator WaitForColorChange(GameObject sentence)
    {
        yield return new WaitForSeconds(waitForColorChange);
        sentence.GetComponent<Sentence>().ReturnColor();
    }

    IEnumerator DestroySkill(GameObject skill, GameObject sentence)
    {
        yield return new WaitForSeconds(sentences[index].GetComponent<Sentence>().GetWaitTime());
        HideText(sentence);
        Destroy(skill);
        canUseSkill = true;
    }

    private void SpawnFirstSkill()
    {
        sentences[0].GetComponent<Sentence>().ChangeColor();
        StartCoroutine(WaitForColorChange(sentences[0]));
        UseSkill(sentences[0]);
        canUseSkill = false;
        StartCoroutine(DestroySkill(activeSkill, sentences[0]));
    }

    //IEnumerator ShowText(GameObject sentence)
    //{
    //    var box = Instantiate(dialogueBox);
    //    var dialogue = Instantiate(sentence.GetComponent<Sentence>().GetText());
    //    yield return new WaitForSeconds(2.0f);
    //    HideText(dialogue, box);
    //}

    //private void HideText(GameObject dialogue, GameObject box)
    //{
    //    Destroy(dialogue);
    //    Destroy(box);
    //}

    private void ShowText(GameObject sentence)
    {
        dialogueBox.SetActive(true);
        sentence.GetComponent<Sentence>().GetText().SetActive(true);
    }

    private void HideText(GameObject sentence)
    {
        dialogueBox.SetActive(false);
        sentence.GetComponent<Sentence>().GetText().SetActive(false);

    }

    public void ChangeColorBlue(int i)
    {
        sentences[i].GetComponent<Sentence>().ChangeColorBlue();
        sentences.Remove(sentences[i]);

    }

}
