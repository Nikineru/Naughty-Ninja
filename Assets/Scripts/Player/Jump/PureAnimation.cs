using System;
using System.Collections;
using UnityEngine;

public class PureAnimation  
{
    public Vector2 LastChanges { get; private set; }

    private Coroutine _lastAnimation;
    private MonoBehaviour _context;

    public PureAnimation(MonoBehaviour context) 
    {
        _context = context;
    }

    public void Play(float duration, Func<float, Vector2> body) 
    {
        if (_lastAnimation != null)
            _context.StopCoroutine(_lastAnimation);

        _lastAnimation = _context.StartCoroutine(GetAnimation(duration, body));
    }

    public IEnumerator GetAnimation(float duration, Func<float, Vector2> body) 
    {
        float expiredTime = 0f;
        float progress = 0f;

        while (progress < 1)
        {
            expiredTime += Time.deltaTime;
            progress += 0.01f;

            LastChanges = body.Invoke(progress);
            
            yield return new WaitForSeconds(duration / 100);
        }
    }
}
