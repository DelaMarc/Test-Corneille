using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CosseTransitionState : State
{
    private const string COSSE_REPOSITIONED = "COSSE_REPOSITIONED";
    private const string SHAKE_FINISHED = "SHAKE_FINISHED";
    List<Pea> m_peas;
    [SerializeField] Image m_wordPicture;
    [SerializeField] Cosse m_cosse;
    [SerializeField] AudioClip m_shakeSound;

#region Lifecycle Methods
    public override void EnterState()
    {
        GameManager.Instance.RegisterEvent(COSSE_REPOSITIONED, OnShrinkFinished);
        GameManager.Instance.RegisterEvent(SHAKE_FINISHED, OnShakeFinished);
        m_cosse.ShrinkPeas();
        //hide all the peas
        foreach (var pea in m_peas)
        {
            pea.CleanUp();
        }
        m_wordPicture.sprite = null;
    }

    public override void LeaveState()
    {
        var gm = GameManager.Instance;

        gm.UnregisterEvent(COSSE_REPOSITIONED, OnShrinkFinished);
        gm.NextLevel();
    }

    public override void UpdateState()
    {
    }
#endregion

    private void OnShrinkFinished(EventParameter a_args)
    {
        //make the cosse shake
        GameManager.Instance.PlaySound(m_shakeSound);
        m_cosse.Shake();
    }

    private void OnShakeFinished(EventParameter a_args)
    {
        StateMachine.Instance.SetState(m_nextState);
    }

    public void SetPeas(List<Pea> a_peas)
    {
        m_peas = a_peas;
    }
}
