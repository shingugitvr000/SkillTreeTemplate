using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TraitNodeView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Button button;
    [SerializeField] private GameObject lockedOverlay;
    [SerializeField] private GameObject purchasedOverlay;

    private TraitNodeData data;
    private TraitTreeRuntime treeRuntime;

    public string Id => data.id;
    public RectTransform RectTransform => transform as RectTransform;

    public void Init(TraitNodeData nodeData, TraitTreeRuntime runtime)
    {
        data = nodeData;
        treeRuntime = runtime;

        iconImage.sprite = data.icon;
        costText.text = data.cost.ToString();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        Refresh();
    }

    private void OnClick()
    {
        treeRuntime.TryPurchase(data.id);
        Refresh();
    }

    public void Refresh()
    {
        bool purchased = treeRuntime.IsPurchased(data.id);
        bool canPurchase = treeRuntime.CanPurchase(data.id);

        lockedOverlay.SetActive(!purchased && !canPurchase);
        purchasedOverlay.SetActive(purchased);
        button.interactable = !purchased && canPurchase;
    }
}