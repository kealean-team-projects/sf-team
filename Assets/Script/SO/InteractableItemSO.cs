using UnityEngine;

[CreateAssetMenu(fileName = "InteractableItemSO", menuName = "Interact/InteractableItemSO")]
public class InteractableItemSO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set;}
}
