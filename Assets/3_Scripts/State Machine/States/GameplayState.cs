using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : State
{
    private const string PEA_CLICKED_EVENT = "PEA_CLICKED";
    [SerializeField] Cosse m_cosse;
    int m_letterIndex;
    #region Lifecycle
    public override void EnterState()
    {
        var gm = GameManager.Instance;

        m_cosse.SetUp();
        m_letterIndex = 0;
        gm.RegisterEvent(PEA_CLICKED_EVENT, OnPeaClicked);
    }

    public override void LeaveState()
    {
        var gm = GameManager.Instance;
        gm.UnregisterEvent(PEA_CLICKED_EVENT, OnPeaClicked);
    }

    public override void UpdateState()
    {
    }
    #endregion

    private void OnPeaClicked(EventParameter a_arg)
    {
        Pea pea = a_arg.sender as Pea;
        var gm = GameManager.Instance;

        if (pea != null && pea.Grapheme ==  gm.Data.Graphemes[m_letterIndex])
        {
            //update the cosse
            m_cosse.AddPea(m_letterIndex);
            ++m_letterIndex;
            if (m_letterIndex >= gm.Data.Graphemes.Length)
            {
                //game won, move to next state
                StateMachine.Instance.SetState(m_nextState);
            }
        }
    }
}
