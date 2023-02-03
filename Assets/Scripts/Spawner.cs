using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    List<QueueMember> queue;
    [SerializeField]
    PreLoader loader;

    List<List<GameObject>> waves = new List<List<GameObject>>();

    private float timer = -1;
    private float waveTimer = 0;
    private int wave = -1;
    private int count = -1;

    private MonoEntity obj;
    private int maxCount;
    private float delay;
    private float interval;

    private List<MonoEntity> pool;
    private void Start()
    {
        pool = loader.GetPoolEntity;
        foreach(var entity in pool)
        {
            Instantiate(entity);
        }
    }
    private MonoEntity PullEnemy(EnemyTypes type)
    {
        return null;
    }
    private void NewStep()
    {
        waves.Add(new List<GameObject>());
        obj = PullEnemy(queue[wave].GetObject());
        maxCount = queue[wave].GetCount();
        delay = queue[wave].GetDelay();
        interval = queue[wave].GetInterval();

        count = maxCount;
        timer = delay;
        waveTimer = 0;
    }
    private void Update()
    {
        if (wave > queue.Count) return;

        try
        {
            if (timer < 0)
            {
                wave++;
                NewStep();
            }

            if (waveTimer < 0 && count > 0)
            {
                waveTimer = interval;
                waves[wave].Add(Instantiate(obj.gameObject));
                count--;
            }

            waveTimer -= Time.deltaTime;
            timer -= Time.deltaTime;
        }
        catch { }

    }
}


public enum EnemyTypes { basic }

[Serializable]
public class QueueMember
{
    //[SerializeField]
    //private MonoEntity obj;
    [SerializeField]
    private EnemyTypes enemyType;
    [SerializeField]
    private int count;
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float delayLoose;

    public EnemyTypes GetObject() { return enemyType; }
    public int GetCount() { return count; }
    public float GetInterval() { return spawnInterval; }
    public float GetDelay() { return delay; }
}

