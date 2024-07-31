using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;


public class LocaleSelector : MonoBehaviour
{
    private bool active = false; 
    public void ChangeLocale(int localeId ) {
        if(active == true)
            return;
            StartCoroutine(SetLocale(localeId));
    }



    IEnumerator SetLocale(int _localeId) {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeId];
        active = false; 
    }
 
}
