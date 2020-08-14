using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManger;
    public GameObject soundManager;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManger);
        }

        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }
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
