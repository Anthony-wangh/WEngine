using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace WEngine.Test
{
    public class TestLocalization : MonoBehaviour
    {
        AsyncOperationHandle m_InitializeOperation;
        private Locale _chineseLocale;
        private Locale _englishLocale;

        void Start()
        {
            // SelectedLocaleAsync will ensure that the locales have been initialized and a locale has been selected.
            m_InitializeOperation = LocalizationSettings.SelectedLocaleAsync;
            if (m_InitializeOperation.IsDone)
            {
                InitializeCompleted(m_InitializeOperation);
            }
            else
            {
                m_InitializeOperation.Completed += InitializeCompleted;
            }
        }

        void InitializeCompleted(AsyncOperationHandle obj)
        {
            var locales = LocalizationSettings.AvailableLocales.Locales;
            for (int i = 0; i < locales.Count; ++i)
            {
                var locale = locales[i];
                if (locale.LocaleName == "Chinese (Simplified) (zh-Hans)")
                {
                    _chineseLocale = locale;
                }
                else if (locale.LocaleName == "English (en)")
                {
                    _englishLocale = locale;
                }
            }
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "中文"))
            {
                LocalizationSettings.Instance.SetSelectedLocale(_chineseLocale);
            }

            if (GUI.Button(new Rect(0, 60, 100, 50), "英文"))
            {
                LocalizationSettings.Instance.SetSelectedLocale(_englishLocale);
            }

            if (GUI.Button(new Rect(0, 110, 100, 50), "GetString"))
            {
                Log.Debug(CoreEngine.Localization.GetString("你好"));
            }
        }
    }
}