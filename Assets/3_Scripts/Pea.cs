using PathCreation;
using TMPro;
using UnityEngine;

public class Pea : MonoBehaviour
{
    private const string PEA_CLICKED = "PEA_CLICKED";
    private const string PEA_PLACED = "PEA_PLACED";
    [SerializeField] TextMeshPro m_text;
    [SerializeField] float m_speed = 2f;
    AudioClip m_phoneme;
    string m_grapheme;
    bool m_ready = false;
    //pea movement
    PathCreator m_path;
    float m_distanceTravelled = 0;
    Vector3 m_prevPos;
    bool m_moving;

    #region Getters
    internal string Grapheme => m_grapheme;
    #endregion

    public void SetUp(string a_txt, AudioClip a_phoneme, PathCreator a_path)
    {
        m_text.text = "";
        m_grapheme = a_txt;
        m_phoneme = a_phoneme;
        m_path = a_path;
        m_distanceTravelled = 0;
        m_prevPos = transform.position;
        m_moving = true;
        m_ready = false;
    }

    public void CleanUp()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (m_ready == true)
        {
            var gm = GameManager.Instance;
            gm.FireEvent(PEA_CLICKED, new EventParameter() { sender = this});
            //play sound
            gm.PlaySound(m_phoneme);
        }
    }

    public void SetReady(bool a_value)
    {
        m_ready = a_value;
    }

    public void DisplayLetter()
    {
        var gm = GameManager.Instance;

        m_text.text = m_grapheme;
        gm.FireEvent(PEA_PLACED, new EventParameter() { sender = this });
    }

    void Update()
    {
        if (m_moving)
        {
            m_prevPos = transform.position;
            m_distanceTravelled += m_speed * Time.deltaTime;
            transform.position = m_path.path.GetPointAtDistance(m_distanceTravelled, EndOfPathInstruction.Stop);
            if (transform.position == m_prevPos)
            {
                m_moving = false;
                DisplayLetter();
            }
        }
    }

}
