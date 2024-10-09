using UnityEngine;

namespace WEngine
{
    public static class SettingsUtil
    {
        private static WEngineSetting _setting;
        public static WEngineSetting WEngineSetting {

            get
            {
                if (_setting == null)
                {
                    _setting = Resources.Load<WEngineSetting>("Setting/WEngineSetting");
                }
                return _setting;
            }
        }
    }
}