using UnityEngine;

[CreateAssetMenu(fileName = "NewUIProviderConfig", menuName = "Data/UIProviderConfig")]
public class UIProviderConfig : ScriptableObject
{
    [field: SerializeField] public GameObject ObjectVisibilityPanel;
    [field: SerializeField] public GameObject ObjectGridElement;
}
