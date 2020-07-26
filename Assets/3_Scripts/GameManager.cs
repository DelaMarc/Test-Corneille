using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] LevelData[] m_levelData;
    [SerializeField] StateMachine m_stateMachine;
    [SerializeField] AudioSource m_audioSource;
    Dictionary<string, UnityEvent<EventParameter>> m_events;
    int m_currentData = 0;

    #region Getters
    public LevelData Data { get { return m_levelData[m_currentData]; } }
    public bool IsPlayingSound { get { return m_audioSource.isPlaying; } }
    #endregion

    void Start()
    {
        //setup Singleton
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        m_events = new Dictionary<string, UnityEvent<EventParameter>>();
        m_stateMachine.Init();
    }

    public void PlaySound(AudioClip a_clip)
    {
        m_audioSource.clip = a_clip;
        m_audioSource.Play();
    }

    public void NextLevel()
    {
        m_currentData = (m_currentData + 1) % m_levelData.Length;
    }

    #region Event Management
    public void RegisterEvent(string a_key, UnityAction<EventParameter> a_action)
    {
        if (m_events.ContainsKey(a_key) == false)
        {
            m_events.Add(a_key, new EventAction());
        }
        m_events[a_key].AddListener(a_action);
    }

    public void UnregisterEvent(string a_key, UnityAction<EventParameter> a_action)
    {
        if (m_events.ContainsKey(a_key))
        {
            m_events[a_key].RemoveListener(a_action);
        }
    }

    public void FireEvent(string a_key, EventParameter a_arg)
    {
        if (m_events.ContainsKey(a_key))
        {
            m_events[a_key].Invoke(a_arg);
        }
    }
    #endregion
}
