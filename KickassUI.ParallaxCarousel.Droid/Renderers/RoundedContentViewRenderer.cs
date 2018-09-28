using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using KickassUI.ParallaxCarousel.Controls;
using KickassUI.ParallaxCarousel.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedContentView), typeof(RoundedContentViewRenderer))]
namespace KickassUI.ParallaxCarousel.Droid.Renderers
{
	public class RoundedContentViewRenderer : VisualElementRenderer<ContentView>
	{
		private float _cornerRadius;
		private bool _hasBackgroundGradient;
		private Xamarin.Forms.Color _startColor;
		private Xamarin.Forms.Color _endColor;

		public RoundedContentViewRenderer(Android.Content.Context context) : base(context) { }

		protected override void OnElementChanged(ElementChangedEventArgs<ContentView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
				return;

			var element = (RoundedContentView)Element;

			//if (element.HasShadow)
			//{
			//	this.Elevation = element.ShadowElevation;
			//	this.TranslationZ = element.ShadowTranslationZ;
			//}

			_startColor = element.GradientStartColor;
			_endColor = element.GradientEndColor;
			_hasBackgroundGradient = element.HasBackgroundGradient;

			_cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, element.CornerRadius, Context.Resources.DisplayMetrics);
			this.OutlineProvider = new RoundedCornerOutlineProvider(_cornerRadius);
			this.ClipToOutline = true;
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var element = (RoundedContentView)Element;

            if (e.PropertyName == RoundedContentView.HasShadowProperty.PropertyName)
			{
				this.Elevation = element.HasShadow ? 40 : 0;
				this.TranslationZ = 0;
			}
		}

        protected override void DispatchDraw(Canvas canvas)
        {
			var bounds = new RectF(0, 0, Width, Height);

			if(_hasBackgroundGradient)
			{
				var fillPaint = new Paint(PaintFlags.AntiAlias);
				var shader = new LinearGradient(0, 0, 0, this.Height, _startColor.ToAndroid(), _endColor.ToAndroid(), Shader.TileMode.Clamp);
				fillPaint.SetShader(shader);
				canvas.DrawRect(bounds, fillPaint);
            }

            base.DispatchDraw(canvas);
        }
	}

	public class RoundedCornerOutlineProvider : ViewOutlineProvider
	{
		float roundCorner;

		public RoundedCornerOutlineProvider(float round)
		{
			roundCorner = round;
		}

		public override void GetOutline(Android.Views.View view, Outline outline)
		{
			outline.SetRoundRect(0,0, view.Width, view.Height, roundCorner);
		}
	}
}
