using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePreview : MonoBehaviour
{
    Cursor _cursor;
    TextMeshProUGUI[] _statsText;
    [SerializeField] GameObject _content;

    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<Cursor>();
        _statsText = GetComponentsInChildren<TextMeshProUGUI>(true);
    }

    private void setPreviewText(BuildingTypeSO type)
    {
        _content.SetActive(true);
        _statsText[0].text = "Damage: " + type.damage.ToString() + " -> " + type.upgrade.damage.ToString();
        _statsText[1].text = "Range: " + type.range.ToString() + " -> " + type.upgrade.range.ToString();
        _statsText[2].text = "Attack interval: " + type.attackRate.ToString() + " -> " + type.upgrade.attackRate.ToString();
        _statsText[3].text = "Cost: " + type.upgrade.cost.ToString();

    }

    private void disablePreview()
    {
        _content.SetActive(false);
    }
    private void OnEnable()
    {
        _cursor.onMouseoverUpgrade += setPreviewText;
        _cursor.onMouseoverNoUpGrade += disablePreview;
    }
    private void OnDisable()
    {
        _cursor.onMouseoverUpgrade += setPreviewText;
        _cursor.onMouseoverNoUpGrade -= disablePreview;
    }
}
