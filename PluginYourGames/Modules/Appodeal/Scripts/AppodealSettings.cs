using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class AppodealSettings
    {
        /// <summary>
        /// Ключ игры для андроид
        /// </summary>
        [Tooltip("Ключ игры для андроид")]
        public string AndroidAppKey;

        /// <summary>
        /// Ключ игры для iOS
        /// </summary>
        [Tooltip("Ключ игры для iOS")]
        public string IOSAppKey;

        /// <summary>
        /// Тестовый режим (без показа реальной рекламы)
        /// </summary>
        [Tooltip("Тестовый режим (без показа реальной рекламы)")]
        public bool IsTesting;

        /// <summary>
        /// Включить логирование СДК
        /// </summary>
        [Tooltip("Включить логирование СДК")]
        public bool IsLogging;
    }

    public partial class InfoYG
    {
        public AppodealSettings Appodeal = new AppodealSettings();
    }
}
