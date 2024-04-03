using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public void Awake()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        this.score = 0;
    }

    public void SetScore(int score) => this.score += score;

    public int GetScore() => this.score;
}
