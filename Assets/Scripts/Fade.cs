using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    void Start()
    {
        ToTransparent(0);
    }

    public Coroutine ToTransparent(float duration) {
        return StartCoroutine(FadeToColorRoutine(duration, new Color(0, 0, 0, 0)));
    }
    public Coroutine ToWhite(float duration) {
        return StartCoroutine(FadeToColorRoutine(duration, new Color(1, 1, 1)));
    }
    public Coroutine ToBlack(float duration) {
        return StartCoroutine(FadeToColorRoutine(duration, new Color(0, 0, 0)));
    }

    private IEnumerator FadeToColorRoutine(float duration, Color color) {
        var screen = GetComponent<Image>();
        while (duration > 0) {
            screen.color = Color.Lerp(screen.color, color, 0.25f);

            duration -= Time.deltaTime;
            yield return null;
        }
        screen.color = color;
    }
}
