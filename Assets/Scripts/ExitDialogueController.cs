using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDialogueController : MonoBehaviour
{
    public bool isOpen = false;
    private float previousTimeScale = 1f;

    // Spawn automatically in every scene so no Editor wiring is required.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AutoInit()
    {
        if (FindObjectOfType<ExitDialogueController>() != null) return;
        var go = new GameObject("ExitDialogueController");
        go.AddComponent<ExitDialogueController>();
        DontDestroyOnLoad(go);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = previousTimeScale == 0f ? 1f : previousTimeScale;
        }
    }

    void OnGUI()
    {
        if (!isOpen) return;

        // Dim background
        var prevColor = GUI.color;
        GUI.color = new Color(0f, 0f, 0f, 0.6f);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        GUI.color = prevColor;

        const float boxW = 380f;
        const float boxH = 180f;
        float boxX = (Screen.width - boxW) / 2f;
        float boxY = (Screen.height - boxH) / 2f;

        GUI.Box(new Rect(boxX, boxY, boxW, boxH), "Exit Ancient Sssnakes?");

        if (GUI.Button(new Rect(boxX + 30f, boxY + 100f, 150f, 50f), "Resume"))
        {
            Toggle();
        }

        if (GUI.Button(new Rect(boxX + 200f, boxY + 100f, 150f, 50f), "Quit"))
        {
            Time.timeScale = 1f;
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
