using System.Collections;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour, IWaveCallback, IDead
{
    [SerializeField]
    PreLoader loader;
    [SerializeField] private Game.Data.Wave _waveData;
    [SerializeField] private float _radiosGizmo;

    private Queue _deadList;

    public int Max => _waveData.GetBossGameEntity == null ? _waveData.NumberEnemy : _waveData.NumberEnemy + 1;

    public bool IsDead => false;

    public event Action<WaveType> CallBack;
    public event Action Dead;

    private WaitForSeconds _wait;

    private void OnEnable()
    {
        if (loader != null)
            loader.Init += Init;
    }

    private void OnDisable()
    {
        if (loader != null)
            loader.Init -= Init;
    }

    private void Init()
    {
        _wait ??= new WaitForSeconds(_waveData.BetweenSpawnsTime);
        _deadList = new Queue(Max);
        StartCoroutine(SpawnTask());
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiosGizmo);
    }
#endif

    private IEnumerator SpawnTask()
    {
        yield return new WaitForSeconds(_waveData.DoStartTime);

        CallBack?.Invoke(WaveType.StartSpawnBaseEnemy);

        for (int i = 0; i < _waveData.NumberEnemy; i++)
        {
            GameEntity eneme = InitEnemy(_waveData.GetGameEntity, _waveData.GetMonoEntity);
            IDead dead = (eneme.Owner as IDead);
            dead.Dead += DeadEnemy;
            _deadList.Enqueue(dead);
            yield return _wait;
        }

        CallBack?.Invoke(WaveType.FinishSpawnBaseEnemy);

        yield return new WaitForSeconds(_waveData.DoStartTime + _waveData.BetweenSpawnsTime);

        CallBack?.Invoke(WaveType.StartSpawnBossEnemy);

        if (_waveData.GetBossGameEntity != null && _waveData.GetBossMonoEntity != null)
        {
            IDead boss = InitEnemy(_waveData.GetBossGameEntity, _waveData.GetBossMonoEntity).Owner as IDead;
            _deadList.Enqueue(boss);
            CallBack?.Invoke(WaveType.SpawnEnemy);
            while (boss.IsDead == false)
                yield return _wait;
        }

        CallBack?.Invoke(WaveType.StartSpawnBossEnemy);

        yield return null;
    }

    private void DeadEnemy()
    {
        IDead dead = _deadList.Dequeue() as IDead;
        dead.Dead -= DeadEnemy;
        Dead?.Invoke();
    }

    private GameEntity InitEnemy(GameEntity data, MonoEntity body)
    {
        MonoEntity enemy = Instantiate(body);
        enemy.transform.position = transform.position;
        return loader.EntityInit(data, enemy);
    }
}

public enum WaveType
{
    StartSpawnBaseEnemy,
    FinishSpawnBaseEnemy,
    SpawnEnemy,
    StartSpawnBossEnemy,
    FinishSpawnBossEnemy
}

public interface IWaveCallback
{
    public event Action<WaveType> CallBack;
}