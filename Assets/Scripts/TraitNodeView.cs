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
    private TraitTreeBoard board;

    public string Id => data.id;
    public RectTransform RectTransform => transform as RectTransform;

    public void Init(TraitNodeData nodeData, TraitTreeRuntime runtime, TraitTreeBoard board)
    {
        data = nodeData;
        treeRuntime = runtime;
        this.board = board;

        iconImage.sprite = data.icon;
        costText.text = data.cost.ToString();

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        Refresh();
    }

    private void OnClick()
    {
        bool success = treeRuntime.TryPurchase(data.id);

        if (success)
        {
            board.RefreshAllNodes();
        }
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