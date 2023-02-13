using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public UICanvasData UICanvasData;
    [field: SerializeField] public UIProviderConfig UIProviderConfig;

    private ObjectVisibilityPresenter _objectVisibilityPresenter;

    private void Awake()
    {
        _objectVisibilityPresenter = new ObjectVisibilityPresenter(UICanvasData, UIProviderConfig);
        _objectVisibilityPresenter.Enable(new ObjectVisibilityModel());
    }
}