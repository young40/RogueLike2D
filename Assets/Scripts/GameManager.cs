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
        boardManager = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
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
        
    }
}
