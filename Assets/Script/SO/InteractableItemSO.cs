using Script.Interectable_Object;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableItemSO", menuName = "Interact/InteractableItemSO")]
public class InteractableItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set;}
    
    [field:SerializeField] public IInteractable Item { get; private set; }

    public void SetItem(IInteractable data)
    {
        Item = data;
    }
}
