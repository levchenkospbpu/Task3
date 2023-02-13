using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectVisibilityView : BaseView
{
    [field: SerializeField] public ScrollRect ObjectsScrollRect;
    [field: SerializeField] public Toggle CheckAllToggle { get; private set; }
    [field: SerializeField] public Button ChangeVisibilityButton { get; private set; }
    [field: SerializeField] public Button TransparencyLevel1Button { get; private set; }
    [field: SerializeField] public Button TransparencyLevel2Button { get; private set; }
    [field: SerializeField] public Button TransparencyLevel3Button { get; private set; }
    [field: SerializeField] public Button TransparencyLevel4Button { get; private set; }
    [field: SerializeField] public Button TransparencyLevel5Button { get; private set; }
    [field: SerializeField] public Button OpenCloseButton { get; private set; }

    public List<Toggle> CheckToggles { get; private set; } = new List<Toggle>();
}
