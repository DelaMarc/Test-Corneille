using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;
    [SerializeField] State m_currentState;

    public void Init()
    {
        //init singleton
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        //start current state
        m_currentState.EnterState();
    }

    public void SetState(State state)
    {
        if (state != null)
        {
            // leave the current state
            m_currentState.LeaveState();
            Debug.Log($"Leaving State {m_currentState.name}");
            //enter the new state
            m_currentState = state;
            Debug.Log($"Entering State {m_currentState.name}");
            m_currentState.EnterState();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentState != null)
            m_currentState.UpdateState();
    }
}
