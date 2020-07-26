using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosse : MonoBehaviour
{
    private const string SHRINK_FINISHED = "SKRINK_FINISHED";
    private const string COSSE_REPOSITIONED = "COSSE_REPOSITIONED";
    [SerializeField] List<PeaCosse> m_middle;
    [SerializeField] RectTransform m_start;
    [SerializeField] RectTransform m_end;
    [SerializeField] PeaCosse m_middlePeaPrefab;
    [SerializeField] float m_shrinkAnimTime = 1f;
    [SerializeField] Shake m_shake;
    [SerializeField] RectTransform m_rectTransform;
    int m_waitShrinkFinished;

#region Getters
    internal RectTransform RectTransform => m_rectTransform;
#endregion

    public void SetUp()
    {
        float offset = m_start.localScale.x + m_end.localScale.x;
        var gm = GameManager.Instance;
        PeaCosse sprite;
        
        if (m_middle.Count == 0)
        {
            m_middle = new List<PeaCosse>();
        }
        m_rectTransform.anchoredPosition = Vector2.zero;
        //add the necessary placeholders
        while (m_middle.Count < gm.Data.Graphemes.Length)
        {
            sprite = Instantiate(m_middlePeaPrefab, transform);
            m_middle.Add(sprite);
        }
        //set peas active
        for (int i = 0; i < gm.Data.Graphemes.Length; ++i)
        {
            m_middle[i].gameObject.SetActive(true);
            m_middle[i].SetUp(gm.Data.Graphemes[i]);
            offset += m_middle[i].transform.localScale.x;
        }
        //Debug.LogError("all peas active");
        m_start.transform.SetAsFirstSibling();
        m_end.transform.SetAsLastSibling();
        m_rectTransform.position = (offset * 0.5f) * Vector3.left;
        m_middle[0].SetWhite();
    }

    public void AddPea(int a_index)
    {
        m_middle[a_index].Reveal();
        if (a_index + 1 < m_middle.Count)
        {
            m_middle[a_index + 1].SetWhite();
        }
    }

    void OnPeaShrinked(EventParameter a_arg)
    {
        --m_waitShrinkFinished;
        PeaCosse pea = a_arg.sender as PeaCosse;
        pea.CleanUp();
        if (m_waitShrinkFinished <= 0)
        {
            //m_rectTransform.anchoredPosition = Vector2.left;
            var gm = GameManager.Instance;
            gm.FireEvent(COSSE_REPOSITIONED, new EventParameter());
            gm.UnregisterEvent(SHRINK_FINISHED, OnPeaShrinked);
        }
    }
    public void ShrinkPeas()
    {
        var gm = GameManager.Instance;

        //register events
        gm.RegisterEvent(SHRINK_FINISHED, OnPeaShrinked);
        m_waitShrinkFinished = gm.Data.Graphemes.Length;
        for (int i = 0; i < gm.Data.Graphemes.Length; ++i)
        {
            m_middle[i].Shrink(m_shrinkAnimTime);
        }
    }

    public void Shake()
    {
        m_shake.DoShake();
    }
}
