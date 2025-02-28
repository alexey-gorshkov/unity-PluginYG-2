using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System;
using UnityEngine;

namespace YG
{
    /// <summary>
    /// Код для работы с SDK Appodeal
    /// https://github.com/appodeal/appodeal-unity-demo/blob/master/Assets/Samples/Appodeal/3.4.2/Usage%20Sample/AppodealDemo.cs
    /// </summary>
    public sealed class AppodealAdNetwork
    {
        public AppodealAdNetwork(string appKey, bool isTesting, bool isLogging)
        {
            InitWithConsent(appKey, isTesting, isLogging);
        }

        private void InitWithConsent(string appKey, bool isTesting, bool isLogging)
        {
            Appodeal.SetTesting(isTesting);

            if (isTesting && isLogging)
            {
                Appodeal.SetLogLevel(AppodealLogLevel.Debug);
            }
            else
            {
                Appodeal.SetLogLevel(isLogging ? AppodealLogLevel.Verbose : AppodealLogLevel.None);
            }

            Appodeal.SetUseSafeArea(true);

            Appodeal.SetUserId("1");
            Appodeal.SetCustomFilter(PredefinedKeys.UserAge, 1);
            Appodeal.SetCustomFilter(PredefinedKeys.UserGender, (int)AppodealUserGender.Female);
            Appodeal.ResetCustomFilter(PredefinedKeys.UserGender);

            //Appodeal.SetBannerRotation(-110, 90);
            Appodeal.SetSmartBanners(false);
            Appodeal.SetTabletBanners(false);
            Appodeal.SetBannerAnimation(false);
            //Appodeal.setBannerBackground(false);

            Appodeal.SetLocationTracking(false);
            Appodeal.MuteVideosIfCallsMuted(true);
            Appodeal.SetChildDirectedTreatment(false);

            Appodeal.SetTriggerOnLoadedOnPrecache(AppodealAdType.Interstitial, true);

            Appodeal.SetAutoCache(AppodealAdType.Interstitial, true);
            Appodeal.SetAutoCache(AppodealAdType.RewardedVideo, true);

            if (isLogging)
            {
                SetAppodealCallbacks();
            }

            int adTypes = AppodealAdType.Banner | AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;

            Appodeal.Initialize(appKey, adTypes);
        }

        public void ShowInterstitial(string adPlacement)
        {
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial) && Appodeal.CanShow(AppodealAdType.Interstitial, adPlacement) && !Appodeal.IsPrecache(AppodealAdType.Interstitial))
            {
                Appodeal.Show(AppodealShowStyle.Interstitial, adPlacement);
            }
            else if (!Appodeal.IsAutoCacheEnabled(AppodealAdType.Interstitial))
            {
                Appodeal.Cache(AppodealAdType.Interstitial);
            }
        }

        public void ShowRewarded(string adPlacement)
        {
            if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo) && Appodeal.CanShow(AppodealAdType.RewardedVideo, adPlacement))
            {
                Appodeal.Show(AppodealShowStyle.RewardedVideo, adPlacement);
            }
            else if (!Appodeal.IsAutoCacheEnabled(AppodealAdType.RewardedVideo))
            {
                Appodeal.Cache(AppodealAdType.RewardedVideo);
            }
        }

        /// <summary>
        /// Кешируем новую рекламу в случае с ручным кешированием
        /// </summary>
        /// <param name="adPlacement"></param>
        public void CacheRewarded()
        {
            if (!Appodeal.IsLoaded(AppodealAdType.RewardedVideo) && !Appodeal.IsAutoCacheEnabled(AppodealAdType.RewardedVideo))
            {
                Appodeal.Cache(AppodealAdType.RewardedVideo);
            }
        }

        public void ShowBannerBottom(string adPlacement)
        {
            Appodeal.Show(AppodealShowStyle.BannerBottom, adPlacement);
        }

        public void ShowBannerTop(string adPlacement)
        {
            Appodeal.Show(AppodealShowStyle.BannerTop, adPlacement);
        }

        public void ShowBannerRight(string adPlacement)
        {
            Appodeal.Show(AppodealShowStyle.BannerRight, adPlacement);
        }

        public void ShowBannerLeft(string adPlacement)
        {
            Appodeal.Show(AppodealShowStyle.BannerLeft, adPlacement);
        }

        public void HideBanner()
        {
            Appodeal.Hide(AppodealAdType.Banner);
        }

        public void ShowBannerView()
        {
            Appodeal.ShowBannerView(AppodealViewPosition.VerticalBottom, AppodealViewPosition.HorizontalCenter, "default");
        }

        public void HideBannerView()
        {
            Appodeal.HideBannerView();
        }

        public bool IsReadyInterstitial()
        {
            return Appodeal.IsLoaded(AppodealAdType.Interstitial);
        }

        public bool IsReadyRewardedVideo()
        {
            return Appodeal.IsLoaded(AppodealAdType.RewardedVideo);
        }

        private void SetAppodealCallbacks()
        {
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;

            AppodealCallbacks.AdRevenue.OnReceived += OnAdRevenueReceived;

            AppodealCallbacks.InAppPurchase.OnValidationSucceeded += OnInAppPurchaseValidationSucceeded;
            AppodealCallbacks.InAppPurchase.OnValidationFailed += OnInAppPurchaseValidationFailed;

            AppodealCallbacks.Banner.OnLoaded += OnBannerLoaded;
            AppodealCallbacks.Banner.OnFailedToLoad += OnBannerFailedToLoad;
            AppodealCallbacks.Banner.OnShown += OnBannerShown;
            AppodealCallbacks.Banner.OnShowFailed += OnBannerShowFailed;
            AppodealCallbacks.Banner.OnClicked += OnBannerClicked;
            AppodealCallbacks.Banner.OnExpired += OnBannerExpired;

            AppodealCallbacks.Interstitial.OnLoaded += OnInterstitialLoaded;
            AppodealCallbacks.Interstitial.OnFailedToLoad += OnInterstitialFailedToLoad;
            AppodealCallbacks.Interstitial.OnShown += OnInterstitialShown;
            AppodealCallbacks.Interstitial.OnShowFailed += OnInterstitialShowFailed;
            AppodealCallbacks.Interstitial.OnClosed += OnInterstitialClosed;
            AppodealCallbacks.Interstitial.OnClicked += OnInterstitialClicked;
            AppodealCallbacks.Interstitial.OnExpired += OnInterstitialExpired;

            AppodealCallbacks.RewardedVideo.OnLoaded += OnRewardedVideoLoaded;
            AppodealCallbacks.RewardedVideo.OnFailedToLoad += OnRewardedVideoFailedToLoad;
            AppodealCallbacks.RewardedVideo.OnShown += OnRewardedVideoShown;
            AppodealCallbacks.RewardedVideo.OnShowFailed += OnRewardedVideoShowFailed;
            AppodealCallbacks.RewardedVideo.OnClosed += OnRewardedVideoClosed;
            AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;
            AppodealCallbacks.RewardedVideo.OnClicked += OnRewardedVideoClicked;
            AppodealCallbacks.RewardedVideo.OnExpired += OnRewardedVideoExpired;
        }

        #region IAppodealInitializeListener implementation

        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e)
        {
            string output = e.Errors == null ? String.Empty : String.Join(", ", e.Errors);
            Debug.Log($"[APDUnity] [Callback] OnInitializationFinished(errors:[{output}])");

            Debug.Log($"[APDUnity] [Appodeal] IsAutoCacheEnabled() for banner: {Appodeal.IsAutoCacheEnabled(AppodealAdType.Banner)}");
            Debug.Log($"[APDUnity] [Appodeal] IsInitialized() for banner: {Appodeal.IsInitialized(AppodealAdType.Banner)}");
            Debug.Log($"[APDUnity] [Appodeal] IsSmartBannersEnabled(): {Appodeal.IsSmartBannersEnabled()}");
            Debug.Log($"[APDUnity] [Appodeal] GetUserId(): {Appodeal.GetUserId()}");
            Debug.Log($"[APDUnity] [Appodeal] GetSegmentId(): {Appodeal.GetSegmentId()}");
            Debug.Log($"[APDUnity] [Appodeal] GetReward(): {Appodeal.GetReward().ToJsonString()}");
            Debug.Log($"[APDUnity] [Appodeal] GetNativeSDKVersion(): {Appodeal.GetNativeSDKVersion()}");

            var networksList = Appodeal.GetNetworks(AppodealAdType.RewardedVideo);
            output = networksList == null ? String.Empty : String.Join(", ", (networksList.ToArray()));
            Debug.Log($"[APDUnity] [Appodeal] GetNetworks() for RV: {output}");

            networksList = Appodeal.GetNetworks(AppodealAdType.Interstitial);
            output = networksList == null ? String.Empty : String.Join(", ", (networksList.ToArray()));
            Debug.Log($"[APDUnity] [Appodeal] GetNetworks() for Interstitial: {output}");

            networksList = Appodeal.GetNetworks(AppodealAdType.Banner);
            output = networksList == null ? String.Empty : String.Join(", ", (networksList.ToArray()));
            Debug.Log($"[APDUnity] [Appodeal] GetNetworks() for Banner: {output}");

            networksList = Appodeal.GetNetworks(AppodealAdType.Mrec);
            output = networksList == null ? String.Empty : String.Join(", ", (networksList.ToArray()));
            Debug.Log($"[APDUnity] [Appodeal] GetNetworks() for Mrec: {output}");
        }

        #endregion

        #region IInAppPurchaseValidationListener implementation

        private void OnInAppPurchaseValidationSucceeded(object sender, InAppPurchaseEventArgs e)
        {
            Debug.Log($"[APDUnity] [Callback] OnInAppPurchaseValidationSucceeded(string json:\n{e.Json})");
        }

        private void OnInAppPurchaseValidationFailed(object sender, InAppPurchaseEventArgs e)
        {
            Debug.Log($"[APDUnity] [Callback] OnInAppPurchaseValidationFailed(string json:\n{e.Json})");
        }

        #endregion

        #region InterstitialAd Callbacks

        private void OnInterstitialLoaded(object sender, AdLoadedEventArgs e)
        {
            Debug.Log($"[APDUnity] [Callback] OnInterstitialLoaded(bool isPrecache:{e.IsPrecache})");
            Debug.Log($"[APDUnity] GetPredictedEcpm(): {Appodeal.GetPredictedEcpm(AppodealAdType.Interstitial)}");
        }

        private void OnInterstitialFailedToLoad(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnInterstitialFailedToLoad()");
        }

        // Called when interstitial is shown
        private void OnInterstitialShown(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnInterstitialShown()");
        }

        private void OnInterstitialShowFailed(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnInterstitialShowFailed()");
        }

        // Called when interstitial is closed
        private void OnInterstitialClosed(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnInterstitialClosed()");
        }

        private void OnInterstitialClicked(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnInterstitialClicked()");
        }

        // Called when interstitial is expired and can not be shown
        private void OnInterstitialExpired(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnInterstitialExpired()");
        }
        #endregion

        #region RewardedVideoAd Callbacks

        private void OnRewardedVideoLoaded(object sender, AdLoadedEventArgs e)
        {
            Debug.Log($"[APDUnity] [Callback] OnRewardedVideoLoaded(bool isPrecache:{e.IsPrecache})");
            Debug.Log($"[APDUnity] GetPredictedEcpm(): {Appodeal.GetPredictedEcpm(AppodealAdType.RewardedVideo)}");
        }

        private void OnRewardedVideoFailedToLoad(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnRewardedVideoFailedToLoad()");
        }

        // Called when rewarded video was loaded, but cannot be shown (internal network errors, placement settings, or incorrect creative)
        private void OnRewardedVideoShowFailed(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnRewardedVideoShowFailed()");
        }

        private void OnRewardedVideoShown(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnRewardedVideoShown()");
        }

        private void OnRewardedVideoClosed(object sender, RewardedVideoClosedEventArgs e)
        {
            Debug.Log($"[APDUnity] [Callback] OnRewardedVideoClosed(bool finished:{e.Finished})");
        }

        // Called when rewarded video is viewed until the end
        private void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs e)
        {
            Debug.Log($"[APDUnity] [Callback] OnRewardedVideoFinished(double amount:{e.Amount}, string name:{e.Currency})");
        }

        private void OnRewardedVideoExpired(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnRewardedVideoExpired()");
        }

        private void OnRewardedVideoClicked(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnRewardedVideoClicked()");
        }
        
        #endregion

        #region BannerAd Callbacks

        private void OnBannerLoaded(object sender, BannerLoadedEventArgs e)
        {
            Debug.Log($"[APDUnity] [Callback] OnBannerLoaded(int height:{e.Height}, bool precache:{e.IsPrecache})");
            Debug.Log($"[APDUnity] GetPredictedEcpm(): {Appodeal.GetPredictedEcpm(AppodealAdType.Banner)}");
        }

        private void OnBannerFailedToLoad(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnBannerFailedToLoad()");
        }

        private void OnBannerShown(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnBannerShown()");
        }

        private void OnBannerShowFailed(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnBannerShowFailed()");
        }

        private void OnBannerClicked(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnBannerClicked()");
        }

        private void OnBannerExpired(object sender, EventArgs e)
        {
            Debug.Log("[APDUnity] [Callback] OnBannerExpired()");
        }
        #endregion

        // доход от рекламы
        private void OnAdRevenueReceived(object sender, AdRevenueEventArgs args)
        {
            Debug.Log($"[APDUnity] [Callback] OnAdRevenueReceived({args.Ad.ToJsonString(true)})");
            // Creating the instance of the YandexAppMetricaRevenue class.
            //YandexAppMetricaAdRevenue adRevenue = new YandexAppMetricaAdRevenue(ad.Revenue, ad.Currency);

            ////var adType = Enum.Parse<AppodealAdType>(ad.AdType);

            //switch (ad.AdType)
            //{
            //    case nameof(AppodealAdType.RewardedVideo):
            //        adRevenue.AdType = YandexAppMetricaAdRevenue.AdTypeEnum.Rewarded;
            //        break;
            //    case nameof(AppodealAdType.Interstitial):
            //        adRevenue.AdType = YandexAppMetricaAdRevenue.AdTypeEnum.Interstitial;
            //        break;
            //    case nameof(AppodealAdType.Banner):
            //        adRevenue.AdType = YandexAppMetricaAdRevenue.AdTypeEnum.Banner;
            //        break;
            //}

            //adRevenue.AdNetwork = ad.NetworkName;
            //adRevenue.AdPlacementName = ad.Placement;
            //adRevenue.AdUnitName = ad.AdUnitName;
            //adRevenue.Precision = ad.RevenuePrecision;

            // Sending data to the AppMetrica server.
            //AppMetrica.Instance.ReportAdRevenue(adRevenue);
        }
    }
}
