using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Game.Data;

namespace Game.View
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private PreLoader _loader;
        [SerializeField] private Data.Shop _data;
        [SerializeField] private Item _prefab;

        public void Init()
        {
            foreach (Data.Shop.Item<Data.Shop.ShopItem<Skill>> item in _data.GetBuy.GetSkills.GetItems)
            {
                Item tempItem = Instantiate(_prefab, transform);
                tempItem.Init(item.GetItem);
                tempItem.Buy += Buy;
            }
        }

        private void Buy(Data.Shop.ShopItem<Skill> buySkill)
        {
            foreach (MonoEntity hero in _loader.GetMonoEntity)
                if (hero.GameEntity.GEType == GEType.Hero)
                {
                    Data.RootMan rootMan = hero.GameEntity as Data.RootMan;
                    Skills.SetSkills setSkills = _loader.Sources.GetGameSkills.Find(buySkill.Item);
                    setSkills.Init(hero);
                    rootMan.Skills.Add(setSkills.Clone() as Skills.SetSkills);
                }
        }

        private void OnEnable()
        {
            if (_loader != null)
                _loader.Init += Init;
        }

        private void OnDisable()
        {
            if (_loader != null)
                _loader.Init -= Init;
        }
    }
}