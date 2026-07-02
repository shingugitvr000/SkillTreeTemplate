using UnityEngine;

public class TraitLineView : MonoBehaviour
{
    [SerializeField] private RectTransform lineRect;

    public void Connect(RectTransform from, RectTransform to)
    {
        Vector2 start = from.anchoredPosition;
        Vector2 end = to.anchoredPosition;

        Vector2 direction = end - start;
        float distance = direction.magnitude;

        lineRect.anchoredPosition = start + direction * 0.5f;
        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.localRotation = Quaternion.Euler(0, 0, angle);
    }
}