#if GooglePlayGamesPlatform_yg && Appodeal_yg
using AppodealStack.Monetization.Common;
using System;
using UnityEngine;
using YG.Insides;

namespace YG
{
    /// <summary>
    /// Модуль апподила для Google Play platform
    /// </summary>
    public partial class PlatformYG2 : IPlatformsYG2
    {
        const string DEFAULT_PLACEMENT = "default";

        private AppodealAdNetwork _appodealAdNetwork;

        private AppodealAdNetwork AppodealModule => _appodealAdNetwork;

        public void InitAppodeal()
        {
#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IOS
            var appKey = "";
#elif UNITY_ANDROID
            var appKey = YG2.infoYG.Appodeal.AndroidAppKey;
#elif UNITY_IOS
            var appKey = YG2.infoYG.Appodeal.IOSAppKey;
#else
	        var appKey = "";
#endif

            Debug.Log("GPP Start InitAppodeal");
            _appodealAdNetwork = new AppodealAdNetwork(appKey, YG2.infoYG.Appodeal.IsTesting, YG2.infoYG.Appodeal.IsLogging);

            AppodealCallbacks.Interstitial.OnShown += OpenInterAdv;
            AppodealCallbacks.Interstitial.OnClosed += CloseInterAdv;
            AppodealCallbacks.Interstitial.OnShowFailed += ErrorInterAdv;

            AppodealCallbacks.RewardedVideo.OnShown += OpenRewardedAdv;
            AppodealCallbacks.RewardedVideo.OnShowFailed += ErrorRewardedAdv;
            AppodealCallbacks.RewardedVideo.OnClosed += CloseRewardedAdv;
            AppodealCallbacks.RewardedVideo.OnFinished += RewardAdv;

            AppodealCallbacks.Banner.OnShown += OpenBannerAdv;
            AppodealCallbacks.Banner.OnShowFailed += ErrorBannerAdv;
        }

        #region Interstitial CallBacks
        private void OpenInterAdv(object sender, EventArgs e) => YGInsides.OpenInterAdv();
        private void CloseInterAdv(object sender, EventArgs e) => YGInsides.CloseInterAdv("true");
        private void ErrorInterAdv(object sender, EventArgs e) => YGInsides.ErrorInterAdv();
        #endregion

        #region Rewarded CallBacks
        private void OpenRewardedAdv(object sender, EventArgs e) => YGInsides.OpenRewardedAdv();
        private void CloseRewardedAdv(object sender, RewardedVideoClosedEventArgs e) => YGInsides.CloseRewardedAdv();
        private void RewardAdv(object sender, RewardedVideoFinishedEventArgs rewarded) => YGInsides.RewardAdv(string.Empty);
        private void ErrorRewardedAdv(object sender, EventArgs e) => YGInsides.ErrorRewardedAdv();
        #endregion

        #region Banner CallBacks
        private void OpenBannerAdv(object sender, EventArgs e) => YGInsides.OpenBannerAdv();
        private void ErrorBannerAdv(object sender, EventArgs e) => YGInsides.ErrorBannerAdv();
        #endregion
    }
}
#endif