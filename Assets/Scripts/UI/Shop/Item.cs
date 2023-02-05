using UnityEngine.UI;
using UnityEngine;
using Game.Data;
using System;
using TMPro;

namespace Game.View
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private Button _buy;

        public event Action<Data.Shop.ShopItem<Skill>> Buy;

        [HideInInspector]
        public Data.Shop.ShopItem<Skill> GetItem { get; private set; }

        public void Init(Data.Shop.ShopItem<Skill> item)
        {
            _sprite.sprite = item.Item.Sprite;
            _name.SetText($" {item.Item.Name}");
            //_description.SetText($" {item.Item.Description}");
            _price.SetText($" {item.Price}");
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