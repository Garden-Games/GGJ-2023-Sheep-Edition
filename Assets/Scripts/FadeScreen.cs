using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{

    public static void FadeOut(Image image, float fadeTimeSeconds)
    {
        image.CrossFadeAlpha(1, fadeTimeSeconds, true);
    }

    public static void FadeIn(Image image, float fadeTimeSeconds)
    {
        image.CrossFadeAlpha(0, fadeTimeSeconds, true);
    }
}
