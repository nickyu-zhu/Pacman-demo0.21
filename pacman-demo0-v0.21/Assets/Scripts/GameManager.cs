
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject pacman;
    public GameObject blinky;
    public GameObject clyde;
    public GameObject inky;
    public GameObject pinky;
    public GameObject inky1;
    public GameObject clyde1;

    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject StartCountDownPrefab;
    public GameObject gameoverPrefab;
    public GameObject winPrefab;
    public AudioClip startClip;
    public Text time;
    public int nowEat;
    public int GhostNum = 0;

    public List<int> usingIndex = new List<int>();
    //public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    public List<int> rawIndex = new List<int> { 0 };

    public List<Vector2> colorPath = new List<Vector2> { };
    public bool colorFlag = false;
    public Dictionary<UnityEngine.Vector2, int> coordinate = new Dictionary<UnityEngine.Vector2, int> { };

    private void Awake()
    {
        _instance = this;
        Screen.SetResolution(1366, 768, false);
        int tempCount = rawIndex.Count;
        //for(int i = 0;i< tempCount; i++)
        //{
        //    int tempIndex = Random.Range(0, rawIndex.Count);
        //    usingIndex.Add(rawIndex[tempIndex]);
        //    rawIndex.RemoveAt(tempIndex);
        //}

        int tempIndex = 0;
        usingIndex.Add(rawIndex[tempIndex]);
        rawIndex.RemoveAt(tempIndex);

    }

    public void Start()
    {
        SetGameState(false);
        nowEat = 0;
        // AudioSource.PlayClipAtPoint(startClip, Vector3.zero);
        
    }
   private  void Update()
    {
        if (nowEat == 6 && pacman.GetComponent<PacmanMove>().enabled != false)
        {
            gamePanel.SetActive(false);
            Instantiate(winPrefab);
            StopAllCoroutines();
            SetGameState(false);
        }
        if(nowEat == 6)
        {
            if(Input.anyKey)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    
    public void OnStartButton()
    {
        StartCoroutine(PlayStartCountDown());
        
        AudioSource.PlayClipAtPoint(startClip, Vector3.zero);
        startPanel.SetActive(false);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    IEnumerator PlayStartCountDown()
    {
        GameObject go = Instantiate(StartCountDownPrefab);
        yield return new WaitForSeconds(4f);
        Destroy(go);
        SetGameState(true);
        gamePanel.SetActive(true);
        GetComponent<AudioSource>().Play();
        
    }
    private void SetGameState(bool state)
    {
        pacman.GetComponent<PacmanMove>().enabled = state;
        blinky.GetComponent<GhostMove>().enabled = state;
        clyde.GetComponent<GhostMove>().enabled = state;
        inky.GetComponent<GhostMove>().enabled = state;
        pinky.GetComponent<GhostMove>().enabled = state;
        inky1.GetComponent<GhostMove>().enabled = state;
        clyde1.GetComponent<GhostMove>().enabled = state;
    }
}
