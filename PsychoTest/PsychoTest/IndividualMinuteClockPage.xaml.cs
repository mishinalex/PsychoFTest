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
    public partial class IndividualMinuteClockPage : ContentPage
    {
        struct HandParams
        {
            public HandParams(double width, double height, double offset) : this()
            {
                Width = width;
                Height = height;
                Offset = offset;
            }

            public double Width { private set; get; }   // fraction of radius  
            public double Height { private set; get; }  // ditto  
            public double Offset { private set; get; }  // relative to center pivot  
        }

        static readonly HandParams secondParams = new HandParams(0.02, 1.1, 0.85);

        BoxView[] tickMarks = new BoxView[60];

        UserResult userResult;
        TestType testType;

        public IndividualMinuteClockPage(UserResult userResult, TestType testType)
        {
            this.userResult = userResult;
            this.testType = testType;
            InitializeComponent();
            // Create the tick marks (to be sized and positioned later).  
            for (int i = 0; i < tickMarks.Length; i++)
            {
                tickMarks[i] = new BoxView { Color = Color.Black };
                absoluteLayout.Children.Add(tickMarks[i]);
            }

            Device.StartTimer(TimeSpan.FromSeconds(1), OnTimerTick);
            
        }

        void OnAbsoluteLayoutSizeChanged(object sender, EventArgs args)
        {
            // Get the center and radius of the AbsoluteLayout.  
            Point center = new Point(absoluteLayout.Width / 2, absoluteLayout.Height / 2);
            double radius = 0.45 * Math.Min(absoluteLayout.Width, absoluteLayout.Height);

            // Position, size, and rotate the 60 tick marks.  
            for (int index = 0; index < tickMarks.Length; index++)
            {
                double size = radius / (index % 5 == 0 ? 15 : 30);
                double radians = index * 2 * Math.PI / tickMarks.Length;
                double x = center.X + radius * Math.Sin(radians) - size / 2;
                double y = center.Y - radius * Math.Cos(radians) - size / 2;
                AbsoluteLayout.SetLayoutBounds(tickMarks[index], new Rectangle(x, y, size, size));
                tickMarks[index].Rotation = 180 * radians / Math.PI;
            }

            // Position and size the three hands.  
            LayoutHand(secondHand, secondParams, center, radius);
        }

        void LayoutHand(BoxView boxView, HandParams handParams, Point center, double radius)
        {
            double width = handParams.Width * radius;
            double height = handParams.Height * radius;
            double offset = handParams.Offset;

            AbsoluteLayout.SetLayoutBounds(boxView,
                new Rectangle(center.X - 0.5 * width,
                              center.Y - offset * height,
                              width, height));

            // Set the AnchorY property for rotations.  
            boxView.AnchorY = handParams.Offset;
        }

        int currentSeccond = 0;
        int maxSeconds = 60;

        bool OnTimerTick()
        {
            secondHand.Rotation = 6 * currentSeccond;
            if (currentSeccond == maxSeconds)
            {
                Navigation.PushAsync(new IndividualMinuteTestPage(userResult, testType));
                return false;
            }
            currentSeccond++;
            return true;
        }

    }

}
