using UnityEngine.UI;
using UnityEngine;
using Game.Data;
using System;
using TMPro;

public class SkullButton : MonoBehaviour
{
    [SerializeField] private Image _sprite;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;

    public event Action<GEType, Skill> Click;

    private Game.Data.RootMan _root;
    private Skill _skill;
    private SetSkill _dataSkill;

    public void Init(GameEntity gameEntity, Skill skill)
    {
        _root = gameEntity as Game.Data.RootMan;
        _skill = skill;
        _sprite.sprite = _skill.Sprite;
        _button.onClick.AddListener(OnClick);

        foreach (Skills.SetSkills shell in _root.Skills.Get)
            if (shell.SetSkill.Skill == skill)
                _dataSkill = shell.SetSkill;

        if (_dataSkill != null)
        {
            _dataSkill.Count += UpdateDataSkill;
            UpdateDataSkill(_dataSkill.Value);
        }
    }

    private void UpdateDataSkill(int count) => _text.SetText($"{count}");

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
        if (_dataSkill != null)
            _dataSkill.Count -= UpdateDataSkill;

    }

    private void OnClick() => Click?.Invoke(_root.GEType, _skill);
}