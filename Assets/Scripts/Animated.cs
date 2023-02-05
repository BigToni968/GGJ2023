using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Animated : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MonoBehaviour _mono;

    private IAnimate _animate;

    private void OnEnable()
    {
        _animate = _mono as IAnimate;

        if (_animate != null)
        {
            _animate.Play += Play;
            _animate.PlaySkill += PlaySkill;
        }
    }

    private void OnDisable()
    {
        if (_animate != null)
        {
            _animate.Play -= Play;
            _animate.PlaySkill -= PlaySkill;
        }
    }

    public void Play(AnimaionType type)
    {
        _animator.SetInteger("State", type switch
        {
            AnimaionType.Move => 2,
            AnimaionType.Attack => 1,
            AnimaionType.None => 0,
            _ => 0
        });
    }

    public void PlaySkill(int number)
    {
        _animator.SetInteger("State", number);
    }
}

public interface IAnimate
{
    public event Action<AnimaionType> Play;
    public event Action<int> PlaySkill;
}

public enum AnimaionType
{
    None,
    Move,
    Attack
}