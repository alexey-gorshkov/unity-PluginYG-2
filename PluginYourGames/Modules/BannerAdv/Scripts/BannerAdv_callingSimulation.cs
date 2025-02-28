#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

namespace YG.Insides
{
    public partial class AdvCallingSimulation
    {
        /// <summary>
        /// TODO: —имул¤цию позиции пока не настроил
        /// </summary>
        /// <param name="position"></param>
        public static void BannerAdvOpen(BannerAdvPosition position)
        {
            AdvCallingSimulation call = CreateCallSimulation();

            call.StartCoroutine(call.BannerAdvOpen(YG2.infoYG.Simulation.durationAdv));
        }

        public IEnumerator BannerAdvOpen(float duration)
        {
            yield return new WaitForSecondsRealtime(YG2.infoYG.Simulation.loadAdv);
            YGInsides.OpenBannerAdv();

            if (!YG2.infoYG.Simulation.testFailAds)
                DrawTopBanner(new Color(0, 1, 0, 0.5f));
            else
                DrawTopBanner(new Color(1, 0, 0, 0.5f));
        }

        public static void BannerAdvHide()
        {
            var obj = FindFirstObjectByType<AdvCallingSimulation>();
            Destroy(obj);
        }
    }
}
#endif