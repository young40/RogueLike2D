﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BoardManager boardManager;

    public int playerFoodPoint = 100;

    [HideInInspector] public bool playerTurn = true;

    private int level = 1;

    public float turnDelay = 0.1f;
    private List<Enemy> enemies;
    private bool enemiesMoving;

    public float levelStartDelay = 2f;

    private Text levelText;
    private GameObject levelImage;
    private GameObject gameOver;
    private bool doingSetup = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        enemies = new List<Enemy>();

        boardManager = GetComponent<BoardManager>();
        InitGame();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log(arg0.name);

        if (arg0.name != "SampleScene")
        {
            return;
        }

        instance.level++;
        instance.InitGame();
    }

    void InitGame()
    {
        doingSetup = true;

        enemies.Clear();
        boardManager.SetUpLevel(level);

        levelImage = GameObject.Find("LevelImage");
        gameOver = GameObject.Find("TextGameOver");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();

        levelText.text = "Day " + level;
        gameOver.SetActive(false);

        Invoke("HideLevelImage", levelStartDelay);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        enabled = false;

        levelImage.SetActive(true);
        GameObject.Find("LevelText").SetActive(false);
        gameOver.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn || enemiesMoving || doingSetup)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playerTurn = true;
        enemiesMoving = false;
    }
}
