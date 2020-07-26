using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] 
    protected State m_nextState;

    public abstract void EnterState();

    public abstract void LeaveState();

    public abstract void UpdateState();
}
