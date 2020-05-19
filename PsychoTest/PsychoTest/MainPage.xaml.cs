using System;
using System.IO;
using System.Linq;
using Xamarin.Forms;

namespace PsychoTest
{
    public enum TestType
    {
        Color,
        Sound,
        RandomPoint,
        ColorPoint,
        EvenOdd,
        Arrow,
        IndividualMinute
    }
    public partial class MainPage : ContentPage
    {
        public static string[] TestNames =
        {
            Data.ColorName,
            Data.SoundName,
            Data.RandomPointName,
            Data.ColorPointName,
            Data.EvenOddName,
            Data.ArrowName,
            Data.IndividualMinuteName
        };



        public static TestType[] TestTypes =
        {
            TestType.Color,
            TestType.Sound,
            TestType.RandomPoint,
            TestType.ColorPoint,
            TestType.EvenOdd,
            TestType.Arrow,
            TestType.IndividualMinute
        };

        public string test { get; set; }

        public MainPage()
        {
            InitializeComponent();

            var userResult = new UserResult();
            var stackLayout = new StackLayout();
            var buttons = TestTypes.Select(t => new Button 
            { 
                Text = TestNames[(int)t],
                Command = new Command(() => Navigation.PushAsync(new TestPreviewPage(t, userResult)))
            });


            foreach (var button in buttons)
                stackLayout.Children.Add(button);

            stackLayout.Children.Add(
                new Button 
                {
                    Text = "Сохранить результат",
                    Command = new Command(() => Navigation.PushAsync(new SavePage(userResult)))
                });

            stackLayout.Children.Add(
                new Button
                {
                    Text = "Показать результаты",
                    Command = new Command(() => Navigation.PushAsync(new ShowAllResultsPage()))
                });



            Content = new ScrollView
            {
                Content = stackLayout
            };

        }


    }
}