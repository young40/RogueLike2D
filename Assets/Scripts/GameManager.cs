using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BoardManager boardManager;

    public int playerFoodPoint = 100;

    [HideInInspector] public bool playerTurn = true;

    private int level = 3;

    public float turnDelay = 0.1f;
    private List<Enemy> enemies;
    private bool enemiesMoving;

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

        enemies = new List<Enemy>();

        DontDestroyOnLoad(gameObject);
        boardManager = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        enemies.Clear();
        boardManager.SetUpLevel(3);
    }

    public void GameOver()
    {
        enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTurn || enemiesMoving)
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
