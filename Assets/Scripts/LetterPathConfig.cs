using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Letter Path Config")]
public class LetterPathConfig : ScriptableObject
{
    [SerializeField] GameObject sentence;
    [SerializeField] GameObject path;
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] float moveSpeed;


    public GameObject GetSentence() { return sentence; }

    public List<Transform> GetPaths() 
    {
        var letterPaths = new List<Transform>();

        foreach(Transform c in path.transform)
            letterPaths.Add(c);

        return letterPaths;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawn; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public float GetMoveSpeed() { return moveSpeed; }



    public List<GameObject> GetWords()
    {
        var words = new List<GameObject>();

        foreach(Transform c in sentence.transform)
            words.Add(c.gameObject);

        return words;
    }

    public List<GameObject> GetLetters()
    {
        var letters = new List<GameObject>();

        foreach(Transform c in sentence.transform)
            if(c.CompareTag("Word"))
                foreach(Transform l in c)
                    letters.Add(l.gameObject);

        return letters;
    }

}
