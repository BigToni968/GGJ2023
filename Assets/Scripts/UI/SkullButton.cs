using UnityEngine.UI;
using UnityEngine;
using Game.Data;
using System;

public class SkullButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;

    public event Action<GEType, Skill> Click;

    private Game.Data.RootMan _root;
    private Skill _skill;

    public void Init(GameEntity gameEntity, Skill skill)
    {
        _root = gameEntity as Game.Data.RootMan;
        _text.text = skill.Name;
        _skill = skill;
        _button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick() => Click?.Invoke(_root.GEType, _skill);
}