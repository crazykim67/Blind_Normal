using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyStep : MonoBehaviour
{
    public SpriteRenderer ren;
    public Light spotLight;

    [SerializeField]
    private float alpha = 0f;
    [SerializeField]
    private float lightIntensity = 0f;
    [SerializeField]
    private float showSpeed = 0.2f;
    [SerializeField]
    private float hideSpeed = 0.2f;

    private void Awake()
    {
        OnStep();
    }

    public void OnStep()
    {
        StartCoroutine(ShowStep());
    }

    public void ReturnStep()
    {
        StartCoroutine(HideStep());
    }

    private IEnumerator ShowStep()
    {
        // 초기화
        alpha = 0f;
        lightIntensity = 0f;
        ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
        spotLight.intensity = lightIntensity;

        while (alpha < 1f || lightIntensity < 1f)
        {
            alpha += showSpeed * Time.deltaTime;
            ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
            
            lightIntensity += showSpeed * Time.deltaTime;
            spotLight.intensity = lightIntensity;
            
            yield return null;
        }

        ReturnStep();
    }

    private IEnumerator HideStep()
    {
        // 초기화
        alpha = 1f;
        lightIntensity = 1f;
        ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
        spotLight.intensity = lightIntensity;

        while (alpha > 0f || lightIntensity > 0f)
        {
            alpha -= hideSpeed * Time.deltaTime;
            ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);

            lightIntensity -= hideSpeed * Time.deltaTime;
            spotLight.intensity = lightIntensity;

            yield return null;
        }

        OnStep();
    }
}
