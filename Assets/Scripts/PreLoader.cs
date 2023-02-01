using UnityEngine;
using Game.Data;


public class PreLoader : MonoBehaviour
{
    [SerializeField] private GameSessionData _data;
    [SerializeField] private GameEntity[] _gameEntities;
    [SerializeField] private MonoEntity[] _entities;

    private void Awake()
    {
        foreach (GameEntity entity in _gameEntities)
        {
            GameEntity gameEntity = Instantiate(entity);
            _data.GameEntities.Add(gameEntity);
        }

        for (int i = 0; i < _entities.Length; i++)
        {
            if (i >= _gameEntities.Length) return;
            _data.GameEntities[i].Owner = _entities[i];
            _data.GameEntities[i].Init();
            _entities[i].Init(_data.GameEntities[i]);
        }
    }

    private void OnDestroy()
    {
        _data.ResetAll();
    }
}
