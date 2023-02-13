using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectVisibilityPresenter : BasePresenter<ObjectVisibilityView, ObjectVisibilityModel>
{
    public Action OnOpenCloseButton;
    public Action<int> OnTransparencyLevelButton;
    public Action<bool> OnChangeVisibilityButton;
    public Action<bool, GameObject> OnCheckValueChanged;
    public Action<bool> OnCheckAllValueChanged;

    protected override GameObject Prefab { get; }
    protected override Transform Parent { get; }

    private GameObject _objectGridElementPrefab;
    private bool _visibilityButtonIsOn = true;
    private bool _isActive = true;

    private List<RawImage> _checkedRawImages;

    public ObjectVisibilityPresenter(UICanvasData uiCanvasData, UIProviderConfig uiProviderConfig) : base(uiCanvasData)
    {
        Prefab = uiProviderConfig.ObjectVisibilityPanel;
        Parent = uiCanvasData.Screens;
        _objectGridElementPrefab = uiProviderConfig.ObjectGridElement;
        _checkedRawImages = new List<RawImage>();
    }

    protected override void OnEnable()
    {
        InitializeScrollView();

        View.CheckAllToggle.onValueChanged.AddListener((bool isOn) => OnCheckAllValueChanged?.Invoke(isOn));
        View.OpenCloseButton.onClick.AddListener(() => OnOpenCloseButton?.Invoke());
        View.ChangeVisibilityButton.onClick.AddListener(() => OnChangeVisibilityButton?.Invoke(_visibilityButtonIsOn));
        View.TransparencyLevel1Button.onClick.AddListener(() => OnTransparencyLevelButton?.Invoke(1));
        View.TransparencyLevel2Button.onClick.AddListener(() => OnTransparencyLevelButton?.Invoke(2));
        View.TransparencyLevel3Button.onClick.AddListener(() => OnTransparencyLevelButton?.Invoke(3));
        View.TransparencyLevel4Button.onClick.AddListener(() => OnTransparencyLevelButton?.Invoke(4));
        View.TransparencyLevel5Button.onClick.AddListener(() => OnTransparencyLevelButton?.Invoke(5));

        OnCheckValueChanged += (bool isOn, GameObject obj) =>
        {
            Model.ChangeCheckedObjects(new DataProvider(isOn, obj));
        };

        OnCheckAllValueChanged += (bool isOn) =>
        {
            ChangeCheckAllToggleState(isOn);
            UpdateAllToggles(isOn);
        };

        OnOpenCloseButton += () =>
        {
            ChangePanelState();
        };

        OnChangeVisibilityButton += (bool isOn) =>
        {
            ChangeVisibilityButtonState();
            Model.ChangeVisibility(new DataProvider(_visibilityButtonIsOn));
        };

        OnTransparencyLevelButton += (int transparencyLevel) =>
        {
            Model.ChangeTranparency(new DataProvider(transparencyLevel));
        };
    }

    protected override void OnDisable()
    {
        OnOpenCloseButton = null;
        OnCheckValueChanged = null;
        OnCheckAllValueChanged = null;
    }

    private void ChangePanelState()
    {
        if (_isActive)
        {
            View.gameObject.GetComponent<Animation>().Play("ObjectVisibilityPanelClose");
            _isActive = false;
        }
        else
        {
            View.gameObject.GetComponent<Animation>().Play("ObjectVisibilityPanelOpen");
            _isActive = true;
        }
    }

    private void ChangeVisibilityButtonState()
    {
        if (_visibilityButtonIsOn)
        {
            View.ChangeVisibilityButton.image.color = new Color(0.5f, 0.5f, 0.5f);
            _visibilityButtonIsOn = false;
            foreach (RawImage image in _checkedRawImages)
            {
                image.color = new Color(0.7176471f, 0.7176471f, 0.7176471f);
            }
        }
        else
        {
            View.ChangeVisibilityButton.image.color = new Color(0.3294118f, 0.2980392f, 0.6509804f);
            _visibilityButtonIsOn = true;
            foreach (RawImage image in _checkedRawImages)
            {
                image.color = new Color(0.3294118f, 0.2980392f, 0.6509804f);
            }
        }
    }

    private void ChangeCheckAllToggleState(bool isOn)
    {
        if (isOn)
        {
            View.CheckAllToggle.image.fillCenter = true;
            View.CheckAllToggle.image.color = new Color(0.3294118f, 0.2980392f, 0.6509804f);
        }
        else
        {
            View.CheckAllToggle.image.fillCenter = false;
            View.CheckAllToggle.image.color = new Color(0.7176471f, 0.7176471f, 0.7176471f);
        }
    }

    private void ChangeGridElementState(bool isOn, Toggle toggle, Outline outline, RawImage rawImage)
    {
        if (isOn)
        {
            outline.effectColor = new Color(0.9622642f, 0.6507185f, 0.1951762f);
            toggle.image.fillCenter = true;
            toggle.image.color = new Color(0.3294118f, 0.2980392f, 0.6509804f);
            _checkedRawImages.Add(rawImage);
        }
        else
        {
            outline.effectColor = new Color(0.7176471f, 0.7176471f, 0.7176471f);
            toggle.image.fillCenter = false;
            toggle.image.color = new Color(0.7176471f, 0.7176471f, 0.7176471f);
            _checkedRawImages.Remove(rawImage);
        }
    }

    private void InitializeScrollView()
    {
        foreach (VisibilityControllable vc in GameObject.FindObjectsOfType<VisibilityControllable>())
        {
            GameObject objectGridElement = GameObject.Instantiate(_objectGridElementPrefab, View.ObjectsScrollRect.content);
            objectGridElement.GetComponentInChildren<TextMeshProUGUI>().text = vc.name;
            Toggle toggle = objectGridElement.gameObject.GetComponentInChildren<Toggle>();
            RawImage rawImage = objectGridElement.gameObject.GetComponentInChildren<RawImage>();
            Outline outline = objectGridElement.gameObject.GetComponent<Outline>();
            View.CheckToggles.Add(toggle);
            toggle.onValueChanged.AddListener((bool isOn) => OnCheckValueChanged?.Invoke(isOn, vc.gameObject));
            toggle.onValueChanged.AddListener((bool isOn) => ChangeGridElementState(isOn, toggle, outline, rawImage));
        }
    }

    private void UpdateAllToggles(bool isOn)
    {
        foreach (Toggle toggle in View.CheckToggles)
        {
            toggle.isOn = isOn;
        }
    }
}
