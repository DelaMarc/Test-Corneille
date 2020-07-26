using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "Corneille/Level")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    AudioClip m_word;
    [SerializeField]
    Sprite m_image;
    [SerializeField]
    string[] m_graphemes;
    [SerializeField]
    AudioClip[] m_phonemes;

    #region Getters
    internal AudioClip Word => m_word;
    internal Sprite Image => m_image;
    internal string[] Graphemes => m_graphemes;
    internal AudioClip[] Phonemes => m_phonemes;
    #endregion
}
