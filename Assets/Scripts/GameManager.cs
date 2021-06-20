using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance
    { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
