#if GooglePlayGamesPlatform_yg && RewardedAdv_yg
using UnityEngine;

namespace YG
{
    /// <summary>
    /// реклама за вознаграждение - Google Play Platform
    /// </summary>
    public partial class PlatformYG2 : IPlatformsYG2
    {
        public void RewardedAdvShow(string id)
        {
            Debug.Log("GPP_RewardedAdvShow");
            AppodealModule.ShowRewarded(DEFAULT_PLACEMENT);
        }

        public void LoadRewardedAdv() {
            Debug.Log("GPP_LoadRewardedAdv");
            AppodealModule.CacheRewarded();
        }
    }
}
#endif