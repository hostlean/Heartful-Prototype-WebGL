using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardSpawner : MonoBehaviour
{
    [SerializeField] GameObject _shardPrefab;
    [SerializeField] List<Transform> spawnPos;
    [SerializeField] float spawnTime;
    [SerializeField] float destroyTime;
    float timer;
    void Start()
    {
        timer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = spawnTime;
            int i = Random.Range(0, 3);
            var hopeShard = Instantiate(_shardPrefab, spawnPos[i].position, Quaternion.identity, this.gameObject.transform.parent);
            if(hopeShard != null)
                Destroy(hopeShard, destroyTime);
        }
    }
}
