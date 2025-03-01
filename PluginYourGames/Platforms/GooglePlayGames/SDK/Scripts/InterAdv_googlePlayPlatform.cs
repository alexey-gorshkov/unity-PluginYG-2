#if GooglePlayGamesPlatform_yg && InterstitialAdv_yg
using UnityEngine;
using YG.Insides;

namespace YG
{
    /// <summary>
    /// Межстраничная реклама - Google play
    /// </summary>
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void InterstitialAdvShow()
        {
            Debug.Log("GPP_InterstitialAdvShow");
            AppodealModule.ShowInterstitial(DEFAULT_PLACEMENT);
        }

        public void FirstInterAdvShow()
        {
            OptionalPlatform.FirstInterAdvShow_RealizationSkip();
        }

        public void OtherInterAdvShow() { }
    }
}
#endif