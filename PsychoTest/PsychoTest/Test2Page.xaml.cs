using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsychoTest
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test2Page : ContentPage
    {
        const int countOfTests = 5;
        int countOfMistakes = 0;
        List<TimeSpan> results = new List<TimeSpan>();
        DateTime lastDate;

        DateTime startEvent(Action action)
        {
            action();
            return DateTime.Now;
        }

        private void onButtonCommand(int buttonIndex, int currentState, Action action)
        {
            if (currentState != buttonIndex)
                countOfMistakes++;
            results.Add(DateTime.Now - lastDate);
            if (results.Count == countOfTests)
                Navigation.PushAsync(new ResultPage(results, countOfMistakes));
            else
                lastDate = startEvent(action);
        }


        async void delayAction(Action action)
        {
            await Task.Delay(500);
            action();
        }


        public Test2Page(TestType testType)
        {
            InitializeComponent();

            var random = new Random();
            var currentState = 0;
            var layout = new RelativeLayout();

            var parentLayout = new RelativeLayout();

            Action testEvent;

            if (testType == TestType.ColorPointTest)
            {
                var points = new[]
                {
                    new BoxView
                    {
                        BackgroundColor = Color.Red
                    },
                    new BoxView
                    {
                      BackgroundColor = Color.Blue
                    }
                };

                testEvent = () =>
                {
                    currentState = random.Next(0, 2);
                    layout.Children.Clear();
                    layout.Children.Add(
                        points[currentState],
                        Constraint.RelativeToParent((parent) => random.Next(0, (int)parent.Width)),
                        Constraint.RelativeToParent((parent) => random.Next(0, (int)parent.Height)),
                        Constraint.Constant(30),
                        Constraint.Constant(30)
                    );
                };


            }
            else
            {
                var label = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    FontSize = 75.0,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                layout.Children.Add(
                    label,
                    Constraint.Constant(0),
                    Constraint.Constant(0),
                    Constraint.RelativeToParent((parent) => parent.Width),
                    Constraint.RelativeToParent((parent) => parent.Height)
                );

                testEvent = () =>
                {
                    var number = random.Next(0, 100);
                    label.Text = number.ToString();
                    currentState = (number + 1) % 2;
                };
            }



            var button1 = new Button
            {
                Text = "1",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var button2 = new Button
            {
                Text = "2",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var buttonStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                        {
                            button1,
                            button2
                        }
            };

            parentLayout.Children.Add(
                layout,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.75)
            );


            parentLayout.Children.Add(
                buttonStack,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.75),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25)
            );

            Content = parentLayout;


            delayAction(() =>
                {
                    lastDate = startEvent(testEvent);
                    button1.Clicked += (k, args) => onButtonCommand(0, currentState, testEvent);
                    button2.Clicked += (k, args) => onButtonCommand(1, currentState, testEvent);
                }
            );



        }
    }
}