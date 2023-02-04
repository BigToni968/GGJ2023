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

    public GameEntity GetHero => _data.Hero;
    public IEnumerable GetMonoEntity => _entities;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        if (_data.Hero == null)
            EntityInit(_gameEntity, _entities);
        else
        {
            LoadEntity(_data.Hero, _entities[0]);
            for (int i = 0; i < _gameEntity.Length; i++)
                EntityInit(_gameEntity[i], _entities.Length > 1 ? _entities[i + 1] : _entities[i]);
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
        monoEntity.Init(dublicategameEntity);
        if (dublicategameEntity.GEType == GEType.Hero)
            _data.Hero = dublicategameEntity;

        return dublicategameEntity;
    }

    public void LoadEntity(GameEntity entity, MonoEntity monoEntity)
    {
        Game.Data.RootMan hero = entity as Game.Data.RootMan;
        hero.Parammeters.Score.Value = -hero.Parammeters.Score.Value;
        hero.Parammeters.Score.Value = -_data.Score;
        hero.Owner = monoEntity;
        hero.Init();
        monoEntity.Init(hero);
    }
}