using System;
using UnityEngine.UI;
using UnityEngine;
using YG.Insides;
using UnityEngine.UIElements;

namespace YG
{
    /// <summary>
    /// Баннерная реклама (общая)
    /// </summary>
    public partial class YG2
    {
        public static Action onOpenBannerAdv;
        public static Action onHideBannerAdv;
        public static Action onErrorBannerAdv;

        public static void BannerAdvShow(BannerAdvPosition position)
        {
#if UNITY_EDITOR
            Message("Banner Adv");
            if (infoYG.Simulation.testFailAds)
            {
                Message("Error Banner Adv simulation");
                YGInsides.ErrorInterAdv();
            }
            // временно для проверки
#if GooglePlayGamesPlatform_yg && Appodeal_yg
            iPlatform.BannerAdvShow(position);
#else
            AdvCallingSimulation.BannerAdvOpen(position);
#endif

#else
            iPlatform.BannerAdvShow(position);
#endif
        }

        public static void BannerAdvHide()
        {
#if UNITY_EDITOR
#if GooglePlayGamesPlatform_yg && Appodeal_yg
            iPlatform.BannerAdvHide();
#else
            AdvCallingSimulation.BannerAdvHide();
#endif
#else
            iPlatform.BannerAdvHide();
#endif
        }
    }
}

namespace YG.Insides
{
    public static partial class YGInsides
    {
        public static void OpenBannerAdv()
        {
            YG2.onOpenBannerAdv?.Invoke();
        }

        public static void BannerAdvHide()
        {
            YG2.onHideBannerAdv?.Invoke();
        }

        public static void ErrorBannerAdv()
        {
            YG2.onErrorBannerAdv?.Invoke();
        }
    }

    public partial class AdvCallingSimulation
    {
#if UNITY_EDITOR
        private void DrawTopBanner(Color color)
        {
            GameObject obj = gameObject;
            Canvas canvas = obj.AddComponent<Canvas>();
            canvas.sortingOrder = 32767;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            obj.AddComponent<GraphicRaycaster>();

            var imgObject = new GameObject("test banner");
            imgObject.transform.SetParent(obj.transform);
            imgObject.AddComponent<RawImage>().color = color;

            var rt = imgObject.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(300, 100);
            rt.anchoredPosition = new Vector2(0, -50);
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
        }
#endif
    }
}