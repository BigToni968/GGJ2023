using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneExplorer : MonoBehaviour
{
    public void Next() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void Back() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    public void Reload() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void Goto(string scene) => SceneManager.LoadScene(scene);
    public void Exit() => Application.Quit();
}
