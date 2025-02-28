namespace YG
{
    public enum BannerAdvPosition { TOP, BOTTOM, RIGHT, LEFT }

    public partial interface IPlatformsYG2
    {
        void BannerAdvShow(BannerAdvPosition position) { }
        void BannerAdvHide() { }
    }
}