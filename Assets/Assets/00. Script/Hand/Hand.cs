using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HandType
{
    Left,
    Right,
}

public abstract class Hand : MonoBehaviour
{
    public HandType handType;

    public SpriteRenderer ren;
    public Material mat;
    public Light spotLight;

    protected float alpha = 0f;
    protected float intensity = 0f;

    public float showSpeed = 0.2f;
    public float hideSpeed = 0.2f;

    public abstract void SetMaterial(Material _mat);
    public abstract void OnHand();
    public abstract void ReturnHand();
    public abstract IEnumerator ShowHand();
    public abstract IEnumerator HideHand();
    public abstract void HandDisable();
}
