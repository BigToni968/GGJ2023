using System.Collections;
using UnityEngine;
using Game.Data;
using System;
using static UnityEngine.EventSystems.EventTrigger;
using System.Collections.Generic;

public class PreLoader : MonoBehaviour
{
    [SerializeField] private GameSessionData _data;
    [SerializeField] private Sources _sources;
    [SerializeField] private MonoEntity[] _entities;
    [SerializeField] private PoolItem[] _pool;

    public event Action Init;
    public Sources Sources => _sources;

    public IEnumerable GetGameEntity => _data.GameEntities;
    public IEnumerable GetMonoEntity => _entities;
    [HideInInspector] public List<MonoEntity> GetPoolEntity;

    private void Awake()
    {
        foreach (GameEntity entity in _sources.GetEntityData.GetEntity)
        {
            GameEntity gameEntity = Instantiate(entity);
            _data.GameEntities.Add(gameEntity);
        }

        for (int i = 0; i < _entities.Length; i++)
        {
            if (i >= _sources.GetEntityData.Count) return;
            _data.GameEntities[i].Owner = _entities[i];
            _data.GameEntities[i].Init();
            _entities[i].Init(_data.GameEntities[i]);
        }
        int length = _entities.Length - 1;
        foreach (PoolItem item in _pool)
        {
            GameEntity entity = item.GetGameEntity;
            GameEntity gameEntity = Instantiate(entity);
            _data.GameEntities.Add(gameEntity);
        }

        for (int i = length; i < _data.GameEntities.Count - 1; i++)
        {
            _data.GameEntities[i].Owner = _pool[i - length].GetMonoEntity;
            GetPoolEntity.Add(_data.GameEntities[i].Owner);
            _data.GameEntities[i].Init();
            _entities[i].Init(_data.GameEntities[i]);
        }

        //foreach (PoolItem item in _pool)
        //{
        //    GameEntity entity = item.GetGameEntity;
        //    GameEntity gameEntity = Instantiate(entity);
        //    _data.GameEntities.Add(gameEntity);
        //    GameEntity dataEntity = _data.GameEntities[_data.GameEntities.Count - 1];

        //    dataEntity.Owner = item.GetMonoEntity;
        //    GetPoolEntity.Add(item.GetMonoEntity);
        //    dataEntity.Init();
        //    item.GetMonoEntity.Init(dataEntity);
        //}

        Init?.Invoke();
    }

    private void OnDestroy()
    {
        _data.ResetAll();
    }
}

//[Serializable]
//public struct PoolItem
//{
//    [SerializeField]
//    private MonoEntity monoEntity;
//    [SerializeField]
//    private GameEntity gameEntity;
//    [SerializeField]
//    private int count; 
//}