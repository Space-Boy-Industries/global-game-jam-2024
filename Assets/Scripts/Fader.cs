using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FadeDirection
{
    IN,
    OUT
}

public class Fader : MonoBehaviour
{
    [SerializeField] private bool fadeOnStart = false;
    [SerializeField] private Image fadeImage;
    
    [SerializeField] public FadeDirection fadeDirection = FadeDirection.OUT;
    [SerializeField] public float fadeTime = 1f;
    [SerializeField] public Color inColor = new(0, 0, 0, 0);
    [SerializeField] private Color outColor = new(0, 0, 0, 1);
    
    private bool _isFading = false;

    private void Start()
    {
        if (fadeOnStart)
        {
            Fade();
        }
    }

    public void Fade()
    {
        if (_isFading) return;
        
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        _isFading = true;
        var startColor = (fadeDirection == FadeDirection.IN) ? outColor : inColor;
        var endColor = (fadeDirection == FadeDirection.IN) ? inColor : outColor;
        
        var startTime = Time.time;
        var endTime = startTime + fadeTime;
        
        while (Time.time < endTime)
        {
            var t = (Time.time - startTime) / fadeTime;
            fadeImage.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }
        
        fadeImage.color = endColor;
        _isFading = false;
    }
}
