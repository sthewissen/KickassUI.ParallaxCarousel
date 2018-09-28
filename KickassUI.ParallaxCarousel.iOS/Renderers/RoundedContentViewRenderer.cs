using System;
using System.ComponentModel;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using KickassUI.ParallaxCarousel.Controls;
using KickassUI.ParallaxCarousel.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(RoundedContentView), typeof(RoundedContentViewRenderer))]
namespace KickassUI.ParallaxCarousel.iOS.Renderers
{
	public class RoundedContentViewRenderer : VisualElementRenderer<ContentView>
	{
        private RoundedContentView _element;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                return;

            _element = (RoundedContentView)e.NewElement;

            SetupCorners();
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            float cornerRadius = _element.CornerRadius;

            if (cornerRadius == -1f)
                cornerRadius = 5f; // default corner radius

            if (_element != null && _element.HasBackgroundGradient) // perform initial setup
            {
                var gradientLayer = new CAGradientLayer
                {
                    CornerRadius = cornerRadius,
                    Frame = NativeView.Bounds,
                    Colors = new CGColor[] { _element.GradientStartColor.ToCGColor(), _element.GradientEndColor.ToCGColor() }
                };

                NativeView.Layer.InsertSublayer(gradientLayer, 0);
            }
        }

        void SetupCorners()
		{
			float cornerRadius = ((RoundedContentView)Element).CornerRadius;

			if (cornerRadius == -1f)
				cornerRadius = 5f; // default corner radius

			Layer.CornerRadius = cornerRadius;

			if (((RoundedContentView)Element).HasShadow)
			{
				Layer.ShadowRadius = 10;
				Layer.ShadowColor = UIColor.Black.CGColor;
				Layer.ShadowOpacity = 0.4f;
				Layer.ShadowOffset = new SizeF();

				Layer.RasterizationScale = UIScreen.MainScreen.Scale;
				Layer.ShouldRasterize = true;
			}
		}

		private RoundedContentView FormsControl
		{
			get { return Element as RoundedContentView; }
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == RoundedContentView.CornerRadiusProperty.PropertyName)
				this.Layer.CornerRadius = (float)FormsControl.CornerRadius;

			if (e.PropertyName == RoundedContentView.HasShadowProperty.PropertyName)
			{
				if (FormsControl.HasShadow)
				{
					Layer.ShadowRadius = 10;
					Layer.ShadowColor = UIColor.Black.CGColor;
					Layer.ShadowOpacity = 0.4f;
					Layer.ShadowOffset = new SizeF();

					Layer.RasterizationScale = UIScreen.MainScreen.Scale;
					Layer.ShouldRasterize = true;
				}
				else
				{
					this.Layer.ShadowRadius = 0;
					Layer.ShadowOpacity = 0;
				}
			}
		}
	}
}
