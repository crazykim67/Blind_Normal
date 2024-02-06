using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RightHand : Hand
{
    public override void OnHand()
    {
        StartCoroutine(ShowHand());
    }

    public override IEnumerator ShowHand()
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

        ReturnHand();
    }

    public override void ReturnHand()
    {
        StartCoroutine(HideHand());
    }

    public override IEnumerator HideHand()
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

        HandDisable();
        
        HandPoolManager.Instance.ReturnRightHand(this);
    }

    public override void HandDisable()
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
