using System;
using System.Collections;
using UnityEngine;

public class SceneTransitionEffect : PostEffectsBase
{
    public Texture2D transitionTexture;
    [Range(0,1)]
    public float process=0;
    public float animTime = 0.5f;

    public float minWaitTime = 0.25f;
    
    private float currentTime = 0;
    
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_Process",process);
            material.SetTexture("_TransitionTexture",transitionTexture);
            Graphics.Blit(src,dest,material);
        }
        else
        {
            Graphics.Blit(src,dest);
        }
    }
    
    public void Play()
    {
        StartCoroutine(PlayCoroutine());
    }
    private IEnumerator PlayCoroutine()
    {
        process = 0;
        currentTime = 0;
        while (currentTime<animTime)
        {
            currentTime += Time.deltaTime;
            process += Time.deltaTime*1 / animTime;
            yield return 0;
        }
        process = 1;
        currentTime = 0;
        while (currentTime<minWaitTime)
        {
            currentTime += Time.deltaTime;
            yield return 0;
        }
        currentTime = 0;
        while (currentTime<animTime)
        {
            currentTime += Time.deltaTime;
            process -= Time.deltaTime*1 / animTime;
            yield return 0;
        }
        process = 0;
        currentTime = 0;
    }
}
