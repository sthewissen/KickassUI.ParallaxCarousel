using System;
using Xamarin.Forms;

namespace KickassUI.ParallaxCarousel.Controls
{ 
    public class RoundedButton : Button
    {
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(RoundedButton), new Thickness(0, 0, 0, 0));

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        private bool _measured = false;
        private bool _self = false;

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (!_self) _measured = true;
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (_measured)
            {
                _measured = false;
                _self = true;

                WidthRequest = width + Padding.Left + Padding.Right;
                HeightRequest = height + Padding.Top + Padding.Bottom;
            }
            else
            {
                _self = false;
            }
        }
    }
}
