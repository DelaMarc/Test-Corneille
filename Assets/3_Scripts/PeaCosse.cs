using TMPro;
using UnityEngine;

public class PeaCosse : MonoBehaviour
{
    private const string SHRINK_FINISHED = "SKRINK_FINISHED";
    [SerializeField] TextMeshProUGUI m_text;
    [SerializeField] GameObject m_whitePea;
    [SerializeField] GameObject m_greenPea;
    string m_content;

    public void SetUp(string a_txt)
    {
        m_content = a_txt;
        m_text.text = "";
        transform.localScale = Vector3.one;
        m_whitePea.SetActive(false);
        m_greenPea.SetActive(true);
    }

    public void CleanUp()
    {
        gameObject.SetActive(false);
    }

    public void Reveal()
    {
        m_whitePea.SetActive(false);
        m_text.text = m_content;
        m_greenPea.SetActive(true);
    }

    public void SetWhite()
    {
        m_whitePea.SetActive(true);
        m_greenPea.SetActive(false);
    }

    void ShrinkFinished()
    {
        GameManager.Instance.FireEvent(SHRINK_FINISHED, new EventParameter() { sender = this });
    }

    public void Shrink(float a_time)
    {
        LeanTween.scaleX(gameObject, 0, a_time).setOnComplete(ShrinkFinished);
    }
}
