using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen_Text_Manager : MonoBehaviour
{
    [SerializeField] private string _levelNumber;
    [SerializeField] private string _levelName;
    [SerializeField] private TMP_Text _levelNameTextField;
    [SerializeField] private bool _showProductionHousePanel;
    [SerializeField] private GameObject _productionHousePanel;

    [SerializeField] private GameObject _splashBG;
    [SerializeField] private Sprite _splashImage;


    void Start()
    {
        _splashBG.GetComponent<Image>().sprite = _splashImage;

        _levelNameTextField.text = "";
        _levelNameTextField.text = _levelNumber + "\n\n" + _levelName;

        if (_showProductionHousePanel)
            _productionHousePanel.SetActive(true);
        else
            _productionHousePanel.SetActive(false);
    }
    
}
