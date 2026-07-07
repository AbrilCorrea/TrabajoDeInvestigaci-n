using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetrics : MonoBehaviour
{
    public static GameMetrics Instance;

    private float startTime;

    public float neutralTime;
    public int neutralErrors;

    public float spiderTime;
    public int spiderErrors;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }
    public void StartNeutralTimer()
    {
        startTime = Time.time;
    }

    public void FinishNeutral()
    {
        neutralTime = Time.time - startTime;
    }

    public void FinishSpider()
    {
        spiderTime = Time.time - startTime;
    }

    public void AddNeutralError()
    {
        neutralErrors++;
    }

    public void AddSpiderError()
    {
        spiderErrors++;
    }
}