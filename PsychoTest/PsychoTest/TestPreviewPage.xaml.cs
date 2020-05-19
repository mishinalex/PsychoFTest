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
    public partial class TestPreviewPage : ContentPage
    {

        public static string[] TestInfos =
        {
            Data.ColorInfo,
            Data.SoundInfo,
            Data.RandomPointInfo,
            Data.ColorPointInfo,
            Data.EvenOddInfo,
            Data.ArrowInfo,
            Data.IndividualMinuteInfo
        };

        public TestPreviewPage(TestType testType, UserResult userResult)
        {

            InitializeComponent();
            var parentLayout = new RelativeLayout();



            var label = new Label
            {
                Text = TestInfos[(int)testType],
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label))
            };

            var button = new Button
            {
                Text = "ОК",
                Command = new Command(() => 
                {
                    if (testType == TestType.Color
                        || testType == TestType.Sound
                        || testType == TestType.RandomPoint)
                        Navigation.PushAsync(new Test1Page(testType, userResult));
                    else if (testType == TestType.EvenOdd
                        || testType == TestType.ColorPoint)
                        Navigation.PushAsync(new Test2Page(testType, userResult));
                    else if (testType == TestType.Arrow)
                        Navigation.PushAsync(new ArrowPage(ArrowPage.Arrow.Up, userResult, testType));
                    else if (testType == TestType.IndividualMinute)
                        Navigation.PushAsync(new IndividualMinuteClockPage(userResult, testType));
                })
            };

            parentLayout.Children.Add(
                label,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.75)
            );


            parentLayout.Children.Add(
                button,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.75),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25)
            );

            Content = parentLayout;




        }
    }
}