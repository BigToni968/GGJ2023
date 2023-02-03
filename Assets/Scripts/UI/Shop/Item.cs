using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Game.Data;
using System;

namespace Game.View
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;
        [SerializeField] private Text _price;
        [SerializeField] private Button _buy;

        public event Action<Data.Shop.ShopItem<Skill>> Buy;

        [HideInInspector]
        public Data.Shop.ShopItem<Skill> GetItem { get; private set; }

        public void Init(Data.Shop.ShopItem<Skill> item)
        {
            _name.text += $" {item.Item.Name}";
            _description.text += $" {item.Item.Description}";
            _price.text += $" {item.Price.ToString()}";
            GetItem = item;
            _buy.onClick.AddListener(OnClick);
        }

        private void OnClick() => Buy?.Invoke(GetItem);

        private void OnDestroy()
        {
            _buy.onClick.RemoveListener(OnClick);
        }
    }
}