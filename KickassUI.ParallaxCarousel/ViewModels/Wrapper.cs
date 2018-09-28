using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KickassUI.ParallaxCarousel.ViewModels
{
    public class Wrapper : INotifyPropertyChanged
    {
        public List<CarouselItem> Items { get; set; } = new List<CarouselItem>();

        private double _slidePosition;

        public event PropertyChangedEventHandler PropertyChanged;

        public double SlidePosition
        {
            get => _slidePosition; set
            {
                if (_slidePosition != value)
                {
                    _slidePosition = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
