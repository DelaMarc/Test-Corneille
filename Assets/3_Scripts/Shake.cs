using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private const string SHAKE_FINISHED = "SHAKE_FINISHED";
    [SerializeField] RectTransform m_rectTransform;
    Vector3 m_scale;

    public void DoShake()
    {
        float offset = Random.insideUnitCircle.magnitude * 0.01f;
        LeanTween.moveX(gameObject, offset, 0.5f).setEaseInBounce().setLoopCount(3).setOnComplete(DoScale);
    }

    void DoScale()
    {
        m_scale = transform.localScale;
        m_rectTransform.anchoredPosition = Vector2.left;
        LeanTween.scale(gameObject, transform.localScale * 1.5f, 1f).setEaseInBounce().setLoopCount(3).setOnComplete(RePosition);
    }

    void RePosition()
    {
        transform.localScale = m_scale;
        m_rectTransform.anchoredPosition = Vector2.left;
        GameManager.Instance.FireEvent(SHAKE_FINISHED, new EventParameter());
    }
}
