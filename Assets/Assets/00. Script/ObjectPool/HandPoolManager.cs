using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPoolManager : MonoBehaviour
{
    #region Instance

    private static HandPoolManager instance;

    public static HandPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new HandPoolManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    [Header("Steps")]
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;

    [Header("Player")]
    [SerializeField]
    private Transform playerTr;
    private Queue<LeftHand> leftHandQueue = new Queue<LeftHand>();
    private Queue<RightHand> rightHandQueue = new Queue<RightHand>();

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        Initialize(5);
        playerTr = FindFirstObjectByType<PlayerController>().GetComponent<Transform>();
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            leftHandQueue.Enqueue(CreateLeftHand());
            rightHandQueue.Enqueue(CreateRightHand());
        }
    }

    private LeftHand CreateLeftHand()
    {
        LeftHand hand = null;

        hand = Instantiate(leftHand).GetComponent<LeftHand>();

        hand.handType = HandType.Left;
        hand.SetMaterial(hand.ren.material);
        hand.HandDisable();
        hand.transform.SetParent(this.transform);

        return hand;
    }

    private RightHand CreateRightHand()
    {
        RightHand hand = null;

        hand = Instantiate(rightHand).GetComponent<RightHand>();

        hand.handType = HandType.Right;
        hand.SetMaterial(hand.ren.material);
        hand.HandDisable();
        hand.transform.SetParent(this.transform);

        return hand;
    }

    public LeftHand GetLeftHand()
    {
        Vector3 pos = playerTr.position;
        Quaternion rot = playerTr.rotation;

        if(leftHandQueue.Count > 0)
        {
            LeftHand hand = leftHandQueue.Dequeue();

            hand.transform.SetParent(null);
            hand.transform.position = new Vector3(pos.x, 0, pos.z);
            hand.transform.rotation = Quaternion.Euler(0, rot.y - 90, 0);

            hand.gameObject.SetActive(true);

            hand.OnHand();

            return hand;
        }
        else
        {
            LeftHand newHand = CreateLeftHand();

            newHand.gameObject.SetActive(true);
            newHand.transform.SetParent(null);
            newHand.transform.position = new Vector3(pos.x, 0, pos.z);
            newHand.transform.rotation = Quaternion.Euler(0, rot.y - 90, 0);

            newHand.OnHand();

            return newHand;
        }
    }

    public RightHand GetRightHand()
    {
        Vector3 pos = playerTr.position;
        Quaternion rot = playerTr.rotation;

        if (leftHandQueue.Count > 0)
        {
            RightHand hand = rightHandQueue.Dequeue();

            hand.transform.SetParent(null);
            hand.transform.position = new Vector3(pos.x, 0, pos.z);
            hand.transform.rotation = Quaternion.Euler(0, rot.y - 270, 0);

            hand.gameObject.SetActive(true);

            hand.OnHand();

            return hand;
        }
        else
        {
            RightHand newHand = CreateRightHand();

            newHand.gameObject.SetActive(true);
            newHand.transform.SetParent(null);
            newHand.transform.position = new Vector3(pos.x, 0, pos.z);
            newHand.transform.rotation = Quaternion.Euler(0, rot.y - 270, 0);

            newHand.OnHand();

            return newHand;
        }
    }

    public void ReturnLeftHand(LeftHand hand)
    {
        hand.transform.SetParent(this.transform);
        leftHandQueue.Enqueue(hand);
    }

    public void ReturnRightHand(RightHand hand)
    {
        hand.transform.SetParent(this.transform);
        rightHandQueue.Enqueue(hand);
    }
}
