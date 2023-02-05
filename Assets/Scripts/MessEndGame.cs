using UnityEngine;
using TMPro;

public class MessEndGame : MonoBehaviour
{
    [SerializeField] private GameObject _self;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _text;

    public void Call(string title, string text)
    {
        _self.SetActive(true);

        if (string.IsNullOrEmpty(title))
            _title.SetText(title);

        if (string.IsNullOrEmpty(text))
            _text.SetText(text);
    }
}
