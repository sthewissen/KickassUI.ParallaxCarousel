using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace KickassUI.ParallaxCarousel.Controls
{
	public class RoundedContentView : ContentView
	{
		public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(RoundedContentView), default(bool));
		public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(RoundedContentView), default(float));

		public static readonly BindableProperty ShadowElevationProperty = BindableProperty.Create(nameof(ShadowElevation), typeof(float), typeof(RoundedContentView), defaultValue: 40f);
		public static readonly BindableProperty ShadowTranslationZProperty = BindableProperty.Create(nameof(ShadowTranslationZ), typeof(float), typeof(RoundedContentView), defaultValue: 0f);

	    public static readonly BindableProperty HasBackgroundGradientProperty = BindableProperty.Create(nameof(HasBackgroundGradient), typeof(bool), typeof(RoundedContentView), defaultValue: default(bool));
		public static readonly BindableProperty GradientStartColorProperty = BindableProperty.Create(nameof(GradientStartColor), typeof(Color), typeof(RoundedContentView), defaultValue: default(Color));
		public static readonly BindableProperty GradientEndColorProperty = BindableProperty.Create(nameof(GradientEndColor), typeof(Color), typeof(RoundedContentView), defaultValue: default(Color));

		public bool HasBackgroundGradient
		{
			get { return (bool)GetValue(HasBackgroundGradientProperty); }
			set { SetValue(HasBackgroundGradientProperty, value); }
		}

		public Color GradientStartColor
		{
			get { return (Color)GetValue(GradientStartColorProperty); }
			set { SetValue(GradientStartColorProperty, value); }
		}

		public Color GradientEndColor
		{
			get { return (Color)GetValue(GradientEndColorProperty); }
			set { SetValue(GradientEndColorProperty, value); }
		}

		public bool HasShadow
		{
			get { return (bool)GetValue(HasShadowProperty); }
			set { SetValue(HasShadowProperty, value); }
		}

		public float ShadowElevation
		{
			get { return (float)GetValue(ShadowElevationProperty); }
			set { SetValue(ShadowElevationProperty, value); }
		}

		public float ShadowTranslationZ
		{
			get { return (float)GetValue(ShadowTranslationZProperty); }
			set { SetValue(ShadowTranslationZProperty, value); }
		}

		public float CornerRadius
		{
			get { return (float)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}
	}
}
