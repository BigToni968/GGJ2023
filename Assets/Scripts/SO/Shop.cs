using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Data/" + nameof(Shop))]
    public class Shop : ScriptableObject
    {
        [SerializeField] private Buy _buy;

        public Buy GetBuy => _buy;

        [System.Serializable]
        public class Buy
        {
            [SerializeField] private Skills _skills;

            public Skills GetSkills => _skills;

            [System.Serializable]
            public class Skills
            {
                [SerializeField] private Item<ShopItem<Skill>>[] _items;

                public Item<ShopItem<Skill>>[] GetItems => _items;
            }


        }

        [System.Serializable]
        public class ShopItem<T>
        {
            [SerializeField] private T _item;
            [SerializeField] private int _price;
            [SerializeField] private int _quantity;

            public T Item => _item;
            public int Price => _price;
            public int Quantity => _quantity;
        }

        [System.Serializable]
        public class Item<T>
        {
            [SerializeField] private T _item;

            public T GetItem => _item;
        }
    }
}