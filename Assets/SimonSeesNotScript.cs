using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class SimonSeesNotScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;
    public KMSelectable[] SquareSels;
    public TextMesh SimonText;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private bool _fadeAnimationPlaying;
    private bool _isFadingIn;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        SimonText.color = new Color(SimonText.color.r, SimonText.color.g, SimonText.color.b, 0f);
        for (int i = 0; i < SquareSels.Length; i++)
        {
            int j = i;
            SquareSels[i].OnInteract += delegate ()
            {
                if (!_moduleSolved)
                    SquarePress(j);
                return false;
            };
        }
    }

    private void SquarePress(int sq)
    {
        if (!_fadeAnimationPlaying)
            _isFadingIn = !_isFadingIn;
        Debug.LogFormat("presssed {0}", sq);
        StartCoroutine(FadeText(_isFadingIn));
    }

    private IEnumerator FadeText(bool fadeIn)
    {
        if (!_fadeAnimationPlaying)
        {
            _fadeAnimationPlaying = true;
            var fadeInFirst = fadeIn ? 1f : 0f;
            var fadeInSecond = fadeIn ? 0f : 1f;
            SimonText.color = new Color(SimonText.color.r, SimonText.color.g, SimonText.color.b, fadeInFirst);
            var duration = 2f;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                if (fadeIn)
                    SimonText.color = new Color(SimonText.color.r, SimonText.color.g, SimonText.color.b, fadeInSecond + (elapsed / duration));
                else
                    SimonText.color = new Color(SimonText.color.r, SimonText.color.g, SimonText.color.b, fadeInSecond - (elapsed / duration));
                yield return null;
                elapsed += Time.deltaTime;
            }
            SimonText.color = new Color(SimonText.color.r, SimonText.color.g, SimonText.color.b, fadeInFirst);
            _fadeAnimationPlaying = false;
        }
    }
}
