using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPathing : MonoBehaviour
{
    LetterPathConfig pathConfig;
    
    [SerializeField] private List<Transform> letterPaths;
    int pathIndex = 0;
    void Start()
    {
        letterPaths = pathConfig.GetPaths();
        transform.position = letterPaths[0].position;
    }

    void Update()
    {
        PathFollow();
    }

    public void SetPathConfig(LetterPathConfig pathConfig)
    {
        this.pathConfig = pathConfig;
    }

    private void PathFollow()
    {
        if(pathIndex <= letterPaths.Count - 1)
        {
            var targetPos = letterPaths[pathIndex].transform.position;
            var movementPerFrame = pathConfig.GetMoveSpeed() * Time.deltaTime;
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPos, movementPerFrame);

            if(gameObject.transform.position == targetPos)
                pathIndex++;
        }
        else pathIndex = 0;

    }
}
