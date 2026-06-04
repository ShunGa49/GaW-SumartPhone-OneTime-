using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    /// <summary>
    /// ゲーム開始
    /// </summary>
    public void OnStartButton()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    public void OnExitButton()
    {
        // Unityエディタ上なら再生停止
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ビルド後のゲームを終了
        Application.Quit();
#endif
    }
}
