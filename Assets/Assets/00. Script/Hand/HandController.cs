using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
}
public class HandController : MonoBehaviour
{
    [SerializeField]
    private float distance = 0.6f;

    [HideInInspector]
    public bool isLeft = false;
    [HideInInspector]
    public bool isRight = false;

    private void Update()
    {
        // 왼쪽 충돌 감지
        Raycast(Direction.Left);
        // 오른쪽 충돌 감지
        Raycast(Direction.Right);
    }

    // 방향 충돌 감지
    private void Raycast(Direction dir)
    {
        int layerMask = 1 << LayerMask.NameToLayer("Map");

        Vector3 origin = transform.position;
        Vector3 direction = Vector3.zero;

        if (dir == Direction.Left)
            direction = -this.transform.right;
        else
            direction = this.transform.right;

        Ray ray = new Ray(origin, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            // 왼쪽일 경우
            if (dir == Direction.Left)
                isLeft = true;
            // 오른쪽일 경우
            else
                isRight = true;
        }
        else if(!Physics.Raycast(ray, out hit, distance, layerMask))
        {
            if (dir == Direction.Left)
                isLeft = false;
            // 오른쪽일 경우
            else
                isRight = false;
        }
    }


    public void OnHand(Transform tr)
    {
        if (isLeft)
            HandPoolManager.Instance.GetLeftHand(tr);
        else if (isRight)
            HandPoolManager.Instance.GetRightHand(tr);
    }
}
