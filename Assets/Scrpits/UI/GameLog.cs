using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour
{
    public TextMeshProUGUI logTMP;

    public void SetLog(string content) {
        logTMP.text = content;
    }

    public void Update() {
        if (logTMP == null) return;

        float alpha = Mathf.PingPong(Time.time, 1f);

        Color c = logTMP.color;
        c.a = alpha;
        logTMP.color = c;
    }
}
