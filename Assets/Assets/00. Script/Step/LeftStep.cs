using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftStep : Step
{
    public override void OnStep()
    {
        StartCoroutine(ShowStep());
    }

    public override IEnumerator ShowStep()
    {
        // 초기화
        alpha = 0f;
        intensity = 0f;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
        spotLight.intensity = intensity;

        while (alpha < 1f || intensity < 1f)
        {
            alpha += showSpeed * Time.deltaTime;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);

            intensity += showSpeed * Time.deltaTime;
            spotLight.intensity = intensity;

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        ReturnStep();
    }

    public override void ReturnStep()
    {
        StartCoroutine(HideStep());
    }

    public override IEnumerator HideStep()
    {
        // 초기화
        alpha = 1f;
        intensity = 1f;
        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
        spotLight.intensity = intensity;

        while (alpha > 0f || intensity > 0f)
        {
            alpha -= hideSpeed * Time.deltaTime;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);

            intensity -= hideSpeed * Time.deltaTime;
            spotLight.intensity = intensity;

            yield return null;
        }

        StepDisable();

        if (stepType == StepType.Player)
            StepPoolManager.Instance.ReturnStep(this);
        else
            StepPoolManager.Instance.ReturnEnemyLeftStep(this);
    }

    public override void StepDisable()
    {
        // 초기화
        alpha = 0f;
        intensity = 0f;
        ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, alpha);
        spotLight.intensity = intensity;

        this.gameObject.SetActive(false);
    }

    public override void SetMaterial(Material _mat)
    {
        Material sampleMat = new Material(_mat);
        mat = sampleMat;
        ren.material = mat;
    }
}
