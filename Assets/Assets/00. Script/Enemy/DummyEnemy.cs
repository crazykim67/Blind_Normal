using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyEnemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField]
    private StepController stepController;

    [SerializeField]
    private List<Transform> destinations = new List<Transform>();
    private int destIndex = 0;
    private int stepIndex = 0;

    private float timer = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(destinations[0].position);
    } 

    private void Update()
    {
        Patroling();

        if(agent.velocity.magnitude > 0)
        {
            timer += Time.deltaTime;

            if(timer >= 1.5f)
            {
                timer = 0;
                if(stepIndex == 0)
                {
                    stepController.OnEnemyStep(this.transform, stepIndex);
                    stepIndex = -1;
                }
                else
                {
                    stepController.OnEnemyStep(this.transform, stepIndex);
                    stepIndex = 0;
                }
            }
        }
        else if(agent.velocity.magnitude <= 0 && agent.pathPending)
        {
            timer = 0;
        }
    }

    private void Patroling()
    {
        if(agent.remainingDistance <= 0 && agent.velocity.magnitude <= 0.2f)
        {
            if (destIndex >= destinations.Count - 1)
                destIndex = 0;
            else
                destIndex += 1;

            agent.SetDestination(destinations[destIndex].position);
        }
    }

}
