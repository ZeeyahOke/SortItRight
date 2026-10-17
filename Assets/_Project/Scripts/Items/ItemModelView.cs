using UnityEngine;

public class ItemModelView : MonoBehaviour
{
    [SerializeField] private Transform modelRoot;
    [SerializeField] private GameObject standIn;

    private GameObject currentModel;

    public void Show(GameObject modelPrefab)
    {
        Clear();

        if (modelPrefab != null)
        {
            standIn.SetActive(false);
            currentModel = Instantiate(modelPrefab, modelRoot, false);
        }
    }

    public void Clear()
    {
        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

        standIn.SetActive(true);
    }
}
