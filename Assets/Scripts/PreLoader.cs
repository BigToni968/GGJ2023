using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Data;
using System;

public class PreLoader : MonoBehaviour
{
    [SerializeField] private GameSessionData _data;
    [SerializeField] private Sources _sources;
    [SerializeField] private GameEntity[] _gameEntity;
    [SerializeField] private MonoEntity[] _entities;

    public event Action Init;
    public Sources Sources => _sources;

    public IEnumerable GetGameEntity => _data.GameEntities;
    public IEnumerable GetMonoEntity => _entities;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        if (_data.GameEntities.Count == 0)
        {
            EntityInit(_gameEntity, _entities);
        }
        else
        {
            for (int i = 0; i < _data.GameEntities.Count; i++)
                if (i < _entities.Length) LoadEntity(_data.GameEntities[i], _entities[i]);
        }

        Init?.Invoke();
    }

    public GameEntity[] EntityInit(GameEntity[] gameEntities, MonoEntity[] monoEntities)
    {
        List<GameEntity> dublicategameEntities = new List<GameEntity>(gameEntities.Length);

        foreach (GameEntity entity in gameEntities)
            dublicategameEntities.Add(Instantiate(entity));

        for (int i = 0; i < monoEntities.Length; i++)
        {
            if (i >= dublicategameEntities.Count) continue;
            EntityInit(dublicategameEntities[i], monoEntities[i]);
        }

        return dublicategameEntities.ToArray();
    }

    public GameEntity EntityInit(GameEntity gameEntity, MonoEntity monoEntity)
    {

        GameEntity dublicategameEntity = Instantiate(gameEntity);

        dublicategameEntity.Owner = monoEntity;
        dublicategameEntity.Init();
        Game.Data.RootMan hero = dublicategameEntity as Game.Data.RootMan;
        hero.Parammeters.Score.Value = -hero.Parammeters.Score.Value;
        hero.Parammeters.Score.Value = -_data.Score;
        monoEntity.Init(dublicategameEntity);
        _data.GameEntities.Add(dublicategameEntity);
        return dublicategameEntity;
    }

    public void LoadEntity(GameEntity entity, MonoEntity monoEntity)
    {
        entity.Owner = monoEntity;
        entity.Init();
        monoEntity.Init(entity);
    }

    public void Goto()
    {
        _data.ResetTrash();
    }

    public void Exit()
    {
        _data.ResetAll();
    }
}