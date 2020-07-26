using UnityEngine;

[CreateAssetMenu(fileName = "Letter data", menuName = "Corneille/Letter")]
public class LetterData : ScriptableObject
{
    public string letter;
    public AudioClip sound;
}
