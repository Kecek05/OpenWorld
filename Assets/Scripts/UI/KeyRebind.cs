

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyRebind : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI[] rebindTexts;
    [SerializeField] private Button[] rebindButtons;
    [SerializeField] private GameObject pressKeyPanel;

    private void Awake()
    {
        rebindButtons[0].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Move_Up); });
        rebindButtons[1].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Move_Down); });
        rebindButtons[2].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Move_Left); });
        rebindButtons[3].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Move_Right); });
        rebindButtons[4].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Jump); });
        rebindButtons[5].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Interact); });
        rebindButtons[6].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Alternate_Interact); });
        rebindButtons[7].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Run); });
        rebindButtons[8].onClick.AddListener(() => { RebindBinding(WitchInputs.Binding.Pause); });
    }
    private void Start()
    {
        HidePressToRebindKey();
        UpdateBindText();
    }



    private void ShowPressToRebindKey()
    {
        pressKeyPanel.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressKeyPanel.SetActive(false);
    }

    private void RebindBinding(WitchInputs.Binding binding)
    {
        ShowPressToRebindKey();
        WitchInputs.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateBindText();
        });
    }

    private void UpdateBindText()
    {
        rebindTexts[0].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Move_Up);
        rebindTexts[1].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Move_Down);
        rebindTexts[2].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Move_Left);
        rebindTexts[3].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Move_Right);
        rebindTexts[4].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Jump);
        rebindTexts[5].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Interact);
        rebindTexts[6].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Alternate_Interact);
        rebindTexts[7].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Run);
        rebindTexts[8].text = WitchInputs.Instance.GetBindingText(WitchInputs.Binding.Pause);
    }
}
