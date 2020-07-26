using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementState : State
{
    private const string PEA_PLACED = "PEA_PLACED";
    [SerializeField] LetterData[] m_letters;
    [SerializeField] AudioClip m_plecementSound;
    [SerializeField] PathCreator[] m_dummies;
    [SerializeField] Pea m_peaPrefab;
    [SerializeField] Image m_wordImage;
    [SerializeField] State m_transitionState;
    List<Pea> m_peas;
    int m_peasToPlace;
    bool m_wordPronounced;

    #region Lifecycle Methods
    public override void EnterState()
    {
        var gm = GameManager.Instance;
        int i;

        m_wordPronounced = false;
        if (m_peas == null)
        {
            m_peas = new List<Pea>();
        }
        //SetUp the peas
        for (i = 0; i < m_peas.Count && i < gm.Data.Graphemes.Length; ++i)
        {
            m_peas[i].gameObject.SetActive(true);
            m_peas[i].SetUp(gm.Data.Graphemes[i], gm.Data.Phonemes[i], m_dummies[i]);
        }
        //add extra peas if needed
        while (m_peas.Count < gm.Data.Graphemes.Length)
        {
            m_peas.Add(SpawnPea());
            m_peas[i].SetUp( gm.Data.Graphemes[i], gm.Data.Phonemes[i], m_dummies[i]);
            ++i;
        }
        //add the 2 extra letters
        AddExtraLetter(i);
        AddExtraLetter(i + 1);
        m_peasToPlace = i + 1;
        for (i = i + 2; i < m_peas.Count; ++i)
        {
            m_peas[i].gameObject.SetActive(false);
        }
        m_wordImage.sprite = gm.Data.Image;
        //play the word sound
        (m_transitionState as CosseTransitionState).SetPeas(m_peas);
        gm.RegisterEvent(PEA_PLACED, PeaPlaced);
    }

    public override void LeaveState()
    {
        var gm = GameManager.Instance;

        gm.UnregisterEvent(PEA_PLACED, PeaPlaced);
    }

    public override void UpdateState()
    {
        var gm = GameManager.Instance;

        if (gm.IsPlayingSound == false && m_wordPronounced == false)
        {
            m_wordPronounced = true;
            gm.PlaySound(gm.Data.Word);
        }
        //move to next state if all peas are placed and the word is pronounced
        if (m_wordPronounced && m_peasToPlace <= 0)
        {
            //make all peas clickable
            foreach (Pea p in m_peas)
            {
                p.SetReady(true);
            }
            StateMachine.Instance.SetState(m_nextState);
        }
    }
#endregion
    Pea SpawnPea()
    {
        Pea pea = Instantiate(m_peaPrefab);
        return pea;
    }

    void PeaPlaced(EventParameter a_arg)
    {
        --m_peasToPlace;
    }

    void AddExtraLetter(int a_dummyIndex)
    {
        int index = Random.Range(0, m_letters.Length - 1);
        LetterData letter = m_letters[index];

        if (m_peas.Count > a_dummyIndex)
        {
            m_peas[a_dummyIndex].gameObject.SetActive(true);
        }
        else
        {
            m_peas.Add(SpawnPea());
        }
        m_peas[a_dummyIndex].SetUp(letter.letter, letter.sound, m_dummies[a_dummyIndex]);
    }
}
