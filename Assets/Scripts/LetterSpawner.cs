using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField] List<LetterPathConfig> pathConfigs;
    [SerializeField] List<GameObject> letters;
    int startingPath = 0;


    void Start()
    {
        var currentPath = pathConfigs[startingPath];
        StartCoroutine(SpawnSkill(currentPath));
    }

    
    void Update()
    {

    }

    IEnumerator SpawnSkill(LetterPathConfig pathConfig)
    {
        for(int i = 0; i < pathConfig.GetLetters().Count; i++)
        {
            var letter = Instantiate(pathConfig.GetLetters()[i], pathConfig.GetPaths()[0].transform.position, Quaternion.identity, this.gameObject.transform);
            letter.GetComponent<LetterPathing>().SetPathConfig(pathConfig);
            yield return new WaitForSeconds(pathConfig.GetTimeBetweenSpawns());
        }
                

    }

    
}
