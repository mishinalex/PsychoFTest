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
    public partial class IndividualMinuteTestPage : ContentPage
    {
        public IndividualMinuteTestPage()
        {
            InitializeComponent();
            var relativeLayout = new RelativeLayout();

            var timerIsRunning = false;

            var startDate = new DateTime();

            var button = new Button
            {
                Text = "Начать тест",
                TextColor = Color.White,
                BackgroundColor = Color.Red
            };

            button.Clicked += (a, b) =>
            {
                if (!timerIsRunning)
                {
                    button.Text = "Стоп";
                    button.BackgroundColor = Color.Blue;
                    startDate = DateTime.Now;
                    timerIsRunning = true;
                }
                else
                {
                    var resultTime = DateTime.Now - startDate;
                    Navigation.PushAsync(new ResultPage(resultTime));
                }
            };
                
            relativeLayout.Children.Add(
                button,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.75),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25)
            );

            Content = relativeLayout;
        }
    }
}