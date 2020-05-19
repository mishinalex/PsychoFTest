
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsychoTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        Label label = new Label
        {
            FontSize = 30.0
        };

        RelativeLayout relativeLayout = new RelativeLayout();
        ResultPage(TestType testType)
        {
            InitializeComponent();
            Title = "Результаты";
            //костыл
            var relativeLayout = new RelativeLayout();




            relativeLayout.Children.Add(
                new Button
                {
                    Text = "На главную",
                    Command = new Command(() => Navigation.PopToRootAsync())
                },
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.50),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25)
            );

           
        }

        void AddView(View view)
        {
            relativeLayout.Children.Add(
                new ScrollView
                {
                    Content = view
                },
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.50)
            );
        }
        public ResultPage(List<TimeSpan> results, int countOfMistakes, UserResult userResult, TestType testType) : this(testType)
        {
            var average = results.Select(r => r.TotalSeconds).Average();
            var newResult = new UserResult();
            if (testType == TestType.Color)
            {
                newResult.CountOfMistakesColorResult = countOfMistakes;
                newResult.TimeColorResult = average;
            }
            else if (testType == TestType.Sound)
            {
                newResult.CountOfMistakesSoundResult = countOfMistakes;
                newResult.TimeSoundResult = average;
            }
            else if (testType == TestType.RandomPoint)
            {
                newResult.CountOfMistakesRandomPointResult = countOfMistakes;
                newResult.TimeRandomPointResult = average;
            }
            else if (testType == TestType.ColorPoint)
            {
                newResult.CountOfMistakesColorPointResult = countOfMistakes;
                newResult.TimeColorPointResult = average;
            }
            else if (testType == TestType.EvenOdd)
            {
                newResult.CountOfMistakesEvenOddResult = countOfMistakes;
                newResult.TimeEvenOddResult = average;
            }

            AddView(newResult.GetView());
            userResult.Add(newResult);
            Content = relativeLayout;

        }

        public ResultPage(int countOfCorrect, int countOfMistakes, int countOfArrows, TimeSpan resultTime, 
            UserResult userResult, TestType testType) : this(testType)
        {
            var time = resultTime.TotalSeconds;
            var newResult = new UserResult();
            if (testType == TestType.Arrow)
            {

                newResult.TimeArrowResult = time;
                newResult.CountOfCorrectArrowResult = countOfCorrect;
                newResult.CountOfMistakesArrowResult = countOfMistakes;
            }

            AddView(newResult.GetView());
            userResult.Add(newResult);
            Content = relativeLayout;
        }

        public ResultPage(TimeSpan resultTime, UserResult userResult, TestType testType) : this(testType)
        {
            var newResult = new UserResult();
            newResult.TimeIndividualMinuteResult = resultTime.TotalSeconds;
            AddView(newResult.GetView());
            userResult.Add(newResult);
            Content = relativeLayout;
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }

        protected override void OnDisappearing()
        {
            Navigation.PopToRootAsync();
            base.OnDisappearing();
        }
    }
}