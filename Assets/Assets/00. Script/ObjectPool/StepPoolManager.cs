using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepPoolManager : MonoBehaviour
{
    #region Instance

    private static StepPoolManager instance;

    public static StepPoolManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new StepPoolManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    [Header("Steps")]
    [SerializeField]
    private GameObject leftStep;
    [SerializeField]
    private GameObject rightStep;

    [Header("Player")]
    [SerializeField]
    private Transform playerTr;
    private Queue<Step> stepQueue = new Queue<Step>();
    private int stepIndex = 0;

    [Header("Enemy")]
    [SerializeField]
    private List<DummyEnemy> enemies = new List<DummyEnemy>();
    [SerializeField]
    private Transform enemyTr;

    private Queue<LeftStep> enemyLeftStepQueue = new Queue<LeftStep>();
    private Queue<RightStep> enemRightStepQueue = new Queue<RightStep>();

    private void Awake()
    {
        if(Instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        Initialize(10);
        EnemyInitialize(10 * enemies.Count);
        playerTr = FindFirstObjectByType<PlayerController>().GetComponent<Transform>();
    }

    #region Player

    private void Initialize(int initCount)
    {
        for(int i = 0; i < initCount; i++) 
            stepQueue.Enqueue(CreateNewObject());
    }

    private Step CreateNewObject()
    {
        Step step = null;

        if(stepIndex == 0)
        {
            step = Instantiate(leftStep).GetComponent<Step>();
            stepIndex = -1;
        }
        else
        {
            step = Instantiate(rightStep).GetComponent<Step>();
            stepIndex = 0;
        }

        step.stepType = StepType.Player;
        step.SetMaterial(step.ren.material);
        step.StepDisable();
        step.transform.SetParent(this.transform);

        return step;
    }

    public Step GetStep()
    {
        Vector3 pos = playerTr.position;
        Quaternion rot = playerTr.rotation;

        if (stepQueue.Count > 0)
        {
            var step = stepQueue.Dequeue();
            step.transform.SetParent(null);
            step.transform.position = new Vector3(pos.x, 0, pos.z);
            step.transform.rotation = rot;

            step.gameObject.SetActive(true);

            step.OnStep();

            return step;
        }
        else
        {
            var newStep = CreateNewObject();

            newStep.gameObject.SetActive(true);
            newStep.transform.SetParent(null);
            newStep.transform.position = new Vector3(pos.x, 0, pos.z);
            newStep.transform.rotation = rot;

            newStep.OnStep();

            return newStep;
        }
    }

    public void ReturnStep(Step step)
    {
        step.transform.SetParent(this.transform);
        stepQueue.Enqueue(step);
    }

    #endregion

    #region Enemy

    private void EnemyInitialize(int initCount)
    {
        for(int i = 0; i < initCount / 2; i++)
            enemyLeftStepQueue.Enqueue(CreateEnemyLeftStep());

        for (int i = 0; i < initCount / 2; i++)
            enemRightStepQueue.Enqueue(CreateEnemyRightStep());
    }

    private LeftStep CreateEnemyLeftStep()
    {
        LeftStep step = null;

        step = Instantiate(leftStep).GetComponent<LeftStep>();

        step.stepType = StepType.Enemy;
        step.SetMaterial(step.ren.material);
        step.StepDisable();
        step.transform.SetParent(this.enemyTr);

        return step;
    }

    private RightStep CreateEnemyRightStep()
    {
        RightStep step = null;

        step = Instantiate(rightStep).GetComponent<RightStep>();

        step.stepType = StepType.Enemy;
        step.SetMaterial(step.ren.material);
        step.StepDisable();
        step.transform.SetParent(this.enemyTr);

        return step;
    }

    public LeftStep GetEnemyLeftStep(Transform tr)
    {
        Vector3 pos = tr.position;
        Quaternion rot = tr.rotation;

        LeftStep step = null;

        if (enemyLeftStepQueue.Count > 0)
        {
            LeftStep leftStep = enemyLeftStepQueue.Dequeue();
            leftStep.transform.SetParent(null);
            leftStep.transform.position = new Vector3(pos.x, 0, pos.z);
            leftStep.transform.rotation = rot;

            leftStep.gameObject.SetActive(true);
            leftStep.OnStep();

            step = leftStep;
        }
        else
        {
            var newStep = CreateEnemyLeftStep();

            newStep.gameObject.SetActive(true);
            newStep.transform.SetParent(null);
            newStep.transform.position = new Vector3(pos.x, 0, pos.z);
            newStep.transform.rotation = rot;

            newStep.OnStep();

            step = newStep;
        }

        return step;
    }

    public RightStep GetEnemyRightStep(Transform tr)
    {
        Vector3 pos = tr.position;
        Quaternion rot = tr.rotation;

        RightStep step = null;

        if (enemRightStepQueue.Count > 0)
        {
            RightStep leftStep = enemRightStepQueue.Dequeue();
            leftStep.transform.SetParent(null);
            leftStep.transform.position = new Vector3(pos.x, 0, pos.z);
            leftStep.transform.rotation = rot;

            leftStep.gameObject.SetActive(true);
            leftStep.OnStep();

            step = leftStep;
        }
        else
        {
            var newStep = CreateEnemyRightStep();

            newStep.gameObject.SetActive(true);
            newStep.transform.SetParent(null);
            newStep.transform.position = new Vector3(pos.x, 0, pos.z);
            newStep.transform.rotation = rot;

            newStep.OnStep();

            step = newStep;
        }

        return step;
    }

    public void ReturnEnemyLeftStep(LeftStep step)
    {
        step.transform.SetParent(this.enemyTr);
        enemyLeftStepQueue.Enqueue(step);
    }

    public void ReturnEnemyRightStep(RightStep step)
    {
        step.transform.SetParent(this.enemyTr);
        enemRightStepQueue.Enqueue(step);
    }

    #endregion
}
