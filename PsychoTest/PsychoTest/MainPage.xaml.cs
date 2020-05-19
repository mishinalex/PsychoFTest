using System;
using System.IO;
using Xamarin.Forms;

namespace PsychoTest
{
    public enum TestType
    {
        ColorTest,
        SoundTest,
        RandomPointTest,
        ColorPointTest,
        EvenOdd
    }
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            Content = new StackLayout
            {
                Children =
                {
                    new Button
                    {
                        Text = "Тест на цвет",
                        Command = new Command(() => Navigation.PushAsync(new Test1Page(TestType.ColorTest)))
                    },
                    new Button
                    {
                        Text = "Тест на звук",
                        Command = new Command(() => Navigation.PushAsync(new Test1Page(TestType.SoundTest)))
                    },
                    new Button
                    {
                        Text = "Тест на квадрат",
                        Command = new Command(() => Navigation.PushAsync(new Test1Page(TestType.RandomPointTest)))
                    },
                    new Button
                    {
                        Text = "Тест на цветной квадрат",
                        Command = new Command(() => Navigation.PushAsync(new Test2Page(TestType.ColorPointTest)))
                    },
                    new Button
                    {
                        Text = "Тест на четное нечетное",
                        Command = new Command(() => Navigation.PushAsync(new Test2Page(TestType.EvenOdd)))
                    },
                    new Button
                    {
                        Text = "Тест на стрелки",
                        Command = new Command(() => Navigation.PushAsync(new ArrowPage(ArrowPage.Arrow.Up)))
                    },
                    new Button
                    {
                        Text = "Индивидуальная минута",
                        Command = new Command(() => Navigation.PushAsync(new IndividualMinuteClockPage()))
                    }



                }
            };

        }


    }
}