using TMPro;
using UnityEngine;

public class InspectPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text materialText;
    [SerializeField] private TMP_Text hintText;

    private void OnEnable()
    {
        panel.SetActive(false);
        EventBus.Subscribe<ItemInspectedEvent>(OnItemInspected);
        EventBus.Subscribe<InspectEndedEvent>(OnInspectEnded);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<ItemInspectedEvent>(OnItemInspected);
        EventBus.Unsubscribe<InspectEndedEvent>(OnInspectEnded);
    }

    private void OnItemInspected(ItemInspectedEvent e)
    {
        nameText.text = e.item.DisplayName;
        materialText.text = "Material: " + e.item.MaterialName;
        hintText.text = e.item.Hint;
        panel.SetActive(true);
    }

    private void OnInspectEnded(InspectEndedEvent e)
    {
        panel.SetActive(false);
    }
}
