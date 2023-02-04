using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(GameSessionData))]
    public sealed class GameSessionData : ScriptableObject
    {
        public List<GameEntity> GameEntities = new List<GameEntity>();

        private void OnDisable()
        {
            ResetAll();
        }

        public void ResetAll()
        {
            GameEntities.Clear();
        }

        public void ResetTrash()
        {
            GameEntity hero = null;

            foreach (GameEntity entity in GameEntities)
                if (entity.GEType == GEType.Hero) hero = entity;

            ResetAll();

            if (hero != null) GameEntities.Add(hero);
        }
    }
}