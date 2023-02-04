using System.Collections;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour, IWaveCallback
{
    [SerializeField]
    PreLoader loader;
    [SerializeField] private Game.Data.Wave _waveData;
    [SerializeField] private float _radiosGizmo;

    public event Action<WaveType> CallBack;

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
            InitEnemy(_waveData.GetGameEntity, _waveData.GetMonoEntity);
            yield return _wait;
        }

        CallBack?.Invoke(WaveType.FinishSpawnBaseEnemy);

        yield return new WaitForSeconds(_waveData.DoStartTime + _waveData.BetweenSpawnsTime);

        CallBack?.Invoke(WaveType.StartSpawnBossEnemy);

        if (_waveData.GetBossGameEntity != null && _waveData.GetBossMonoEntity != null)
        {
            IDead boss = InitEnemy(_waveData.GetBossGameEntity, _waveData.GetBossMonoEntity) as IDead;

            while (boss.IsDead == false)
                yield return _wait;
        }

        CallBack?.Invoke(WaveType.StartSpawnBossEnemy);

        yield return null;
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
    StartSpawnBossEnemy,
    FinishSpawnBossEnemy
}

public interface IWaveCallback
{
    public event Action<WaveType> CallBack;
}