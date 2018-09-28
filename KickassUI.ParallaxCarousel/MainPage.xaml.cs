using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using KickassUI.ParallaxCarousel.ViewModels;
using Xamarin.Forms;

namespace KickassUI.ParallaxCarousel
{
    public partial class MainPage : ContentPage
    {
        private int _currentIndex;
        private List<Color> _backgroundColors = new List<Color>();

        public Wrapper Wrapper { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Wrapper = new Wrapper
            {
                Items = new List<CarouselItem>() 
                {
                    // Just create some dummy data here for now.
                    new CarouselItem{ Position=0, Type="JUICY AND ORANGE", ImageSrc="oranges.png", Name = "ORANGE AWESOMENESS", Price = 120, Title = "ORANGE AWESOMENESS", BackgroundColor= Color.FromHex("#9866d5"), StartColor=Color.FromHex("#f3463f"),  EndColor=Color.FromHex("#fece49")},
                    new CarouselItem{ Position=0, Type="NOT A TYPICAL FRUIT", ImageSrc="tomato.png", Name = "TERRIBLE TOMATO", Price = 129, Title = "TERRIBLE TOMATO", BackgroundColor= Color.FromHex("#fab62a"), StartColor=Color.FromHex("#42a7ff"),  EndColor=Color.FromHex("#fab62a")},
                    new CarouselItem{ Position=0, Type="SWEET AND GREEN", ImageSrc="pear.png", Name = "PEAR PARTY", Price = 140, Title = "PEAR PARTY", BackgroundColor= Color.FromHex("#425cfc"), StartColor=Color.FromHex("#33ccf3"),  EndColor=Color.FromHex("#ccee44")}
                }
            };

            this.BindingContext = Wrapper;

            // Create out a list of background colors based on our items colors so we can do a gradient on scroll.
            for (int i = 0; i < Wrapper.Items.Count; i++)
            {
                var current = Wrapper.Items[i];
                var next = Wrapper.Items.Count > i + 1 ? Wrapper.Items[i + 1] : null;

                if (next != null)
                    _backgroundColors.AddRange(SetGradients(current.BackgroundColor, next.BackgroundColor, 100));
                else
                    _backgroundColors.Add(current.BackgroundColor);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Need to start somewhere...
            page.BackgroundColor = _backgroundColors.First();
        }

        public void Handle_PositionSelected(object sender, PositionSelectedEventArgs e)
        {
            _currentIndex = e.NewValue;
            Wrapper.SlidePosition = 0;
        }

        public void Handle_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            int position = 0;

            if (e.Direction == ScrollDirection.Right)
                position = (int)((_currentIndex * 100) + e.NewValue);
            else if (e.Direction == ScrollDirection.Left)
                position = (int)((_currentIndex * 100) - e.NewValue);

            // Set the background color of our page to the item in the color gradient
            // array, matching the current scrollindex.
            if (position > _backgroundColors.Count - 1)
                page.BackgroundColor = _backgroundColors.Last();
            else if (position < 0)
                page.BackgroundColor = _backgroundColors.First();
            else
                page.BackgroundColor = _backgroundColors[position];

            // Save the current scroll position
            Wrapper.SlidePosition = e.NewValue;

            if (e.Direction == ScrollDirection.Right)
            {
                // When scrolling right, we offset the current item and the next one.
                Wrapper.Items[_currentIndex].Position = -Wrapper.SlidePosition;

                if (_currentIndex < Wrapper.Items.Count - 1)
                {
                    Wrapper.Items[_currentIndex + 1].Position = 100 - Wrapper.SlidePosition;
                }
            }
            else if (e.Direction == ScrollDirection.Left)
            {
                // When scrolling left, we offset the current item and the previous one.
                Wrapper.Items[_currentIndex].Position = Wrapper.SlidePosition;

                if (_currentIndex > 0)
                {
                    Wrapper.Items[_currentIndex - 1].Position = -100 + Wrapper.SlidePosition;
                }
            }
        }

        // Create a list of all the colors in between our start and end color.
        public static IEnumerable<Color> SetGradients(Color start, Color end, int steps)
        {
            var colorList = new List<Color>();

            double aStep = ((end.A * 255) - (start.A * 255)) / steps;
            double rStep = ((end.R * 255) - (start.R * 255)) / steps;
            double gStep = ((end.G * 255) - (start.G * 255)) / steps;
            double bStep = ((end.B * 255) - (start.B * 255)) / steps;

            for (int i = 0; i < 100; i++)
            {
                var a = (start.A * 255) + (int)(aStep * i);
                var r = (start.R * 255) + (int)(rStep * i);
                var g = (start.G * 255) + (int)(gStep * i);
                var b = (start.B * 255) + (int)(bStep * i);

                colorList.Add(Color.FromRgba(r / 255, g / 255, b / 255, a / 255));
            }

            return colorList;
        }
    }
}
