#if GooglePlayGamesPlatform_yg
using UnityEngine;

namespace YG
{
    /// <summary>
    /// Платформа для Google Play
    /// </summary>
    public partial class PlatformYG2 : IPlatformsYG2
    {
        // инициализация платформы
        public void InitStart()
        {
            Debug.Log("GPP InitStart");
            InitAppodeal();
        }
    }
}

namespace YG.Insides
{
    public partial class ProjectSettings
    {
        public string myOption; // Добавление опции
    }
}

#endif
