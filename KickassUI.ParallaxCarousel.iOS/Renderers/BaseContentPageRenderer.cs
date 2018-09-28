using System;
using CoreAnimation;
using CoreGraphics;
using KickassUI.ParallaxCarousel.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(BaseContentPageRenderer))]
namespace KickassUI.ParallaxCarousel.iOS.Renderers
{
    public class BaseContentPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ModalPresentationCapturesStatusBarAppearance = true;

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
        }

        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
    }
}
