#if GooglePlayGamesPlatform_yg && InterstitialAdv_yg
using UnityEngine;
using YG.Insides;

namespace YG
{
    /// <summary>
    /// Баннерная реклама - Google play
    /// </summary>
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void BannerAdvShow(BannerAdvPosition position)
        {
            Debug.Log("GPP_BannerAdvShow");
            switch (position)
            {
                case BannerAdvPosition.TOP:
                    AppodealModule.ShowBannerTop(DEFAULT_PLACEMENT);
                    break;
                case BannerAdvPosition.BOTTOM:
                    AppodealModule.ShowBannerBottom(DEFAULT_PLACEMENT);
                    break;
                case BannerAdvPosition.RIGHT:
                    AppodealModule.ShowBannerRight(DEFAULT_PLACEMENT);
                    break;
                case BannerAdvPosition.LEFT:
                    AppodealModule.ShowBannerLeft(DEFAULT_PLACEMENT);
                    break;
            }
        }

        public void BannerAdvHide()
        {
            Debug.Log("GPP_BannerAdvHide");
            AppodealModule.HideBanner();

            // кидаем сами событие т.к. нет колбека
            YGInsides.BannerAdvHide();
        }
    }
}
#endif