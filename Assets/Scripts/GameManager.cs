using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
                Debug.LogError("Game Manager is NULL.");
            return _instance;
        }
    }

    [SerializeField] List<GameObject> npcs = new List<GameObject>();
    [SerializeField] List<GameObject> fightAreas = new List<GameObject>();
    [SerializeField] Transform fightAreaPos;
    [SerializeField] Transform lapSpawnPos;
    [SerializeField] GameObject activeFightArea;
    [SerializeField] GameObject izoPlayer;
    int activeIndex = 0;
    Vector3 originalPlayerPos;

    private void Awake()
    {
        _instance = this;
        izoPlayer.SetActive(false);
        originalPlayerPos = izoPlayer.transform.position;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeFightArea(int fightAreaIndex)
    {
        Camera.main.transform.position = fightAreaPos.position;
        izoPlayer.SetActive(false);
        Debug.Log("Player Despawned");
        GameObject fightArea = Instantiate(fightAreas[fightAreaIndex], new Vector3(0, -22, -1), Quaternion.identity);
        activeFightArea = fightArea;
        MusicChanger.Instance.LoadMusic(6);
    }

    public void DestroyActiveFightArea()
    {
        Destroy(activeFightArea);
    }

    public void LostFight()
    {
        DestroyActiveFightArea();
        izoPlayer.SetActive(true);
        SFXChanger.Instance.PlayBattleLost();
        MusicChanger.Instance.LoadMusic(1);
        //lost shot
        izoPlayer.transform.position = originalPlayerPos;
    }

    public void WonFight()
    {
        DestroyActiveFightArea();
        SFXChanger.Instance.PlayBattleWon();
        Destroy(npcs[activeIndex]);
        activeIndex++;
        //won shot
        CameraToLab();
    }

    public void FollowPlayer()
    {
        Camera.main.transform.position = new Vector3(izoPlayer.transform.position.x, izoPlayer.transform.position.y, -10);
    }


    public void CameraToLab()
    {
        izoPlayer.SetActive(true);
        FollowPlayer();
        MusicChanger.Instance.LoadMusic(1);

    }

    public int GetNPCIndex()
    {
        return activeIndex;
    }

    

}
