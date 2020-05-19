using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Media;

using SkiaSharp;
using SkiaSharp.Views;
//using SkiaSharp.Views.Android;
using SkiaSharp.Views.Forms;

namespace PsychoTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test1Page : ContentPage
    {

        MediaPlayer player;

        SKCanvas canvas;
        SKImageInfo info;


        async Task<DateTime> startEvent(Action action, int delay)
        {
            await Task.Delay(delay);
            action();
            return DateTime.Now;
        }

        void onCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            info = args.Info;
            var surface = args.Surface;
            canvas = surface.Canvas;
            canvas.Clear();
        }


        public Test1Page(TestType testType, UserResult userResult)
        {
            InitializeComponent();
            var button = new Button();
            var random = new Random();
            const int countOfTests = 5;
            var results = new List<TimeSpan>();
            var countOfMistakes = 0;

            Action startEvent;
            Action cancelEvent;

            var layout = new RelativeLayout();

            layout.Children.Add(
                button, 
                Constraint.Constant(0), 
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Width), 
                Constraint.RelativeToParent((parent) => parent.Height)
                );

            if (testType == TestType.Color)
            {
                startEvent = () => button.BackgroundColor = Color.Red;
                cancelEvent = () => button.BackgroundColor = Color.White;
            }
            else if (testType == TestType.Sound)
            {
                player = new MediaPlayer();
                player.Reset();
                
                var assetFileDescriptor = Android.App.Application.Context.Assets.OpenFd("sound.mp3");
                player.SetDataSource(assetFileDescriptor);
                player.Looping = true;
                player.Prepare();
                
                startEvent = () => player.Start();
                cancelEvent = () => player.Pause();
            }
            else
            {
                var redBox = new BoxView
                {
                    BackgroundColor = Color.Red
                };

                startEvent = () => layout.Children.Add(
                    redBox,
                    Constraint.RelativeToParent((parent) => random.Next(30, (int)parent.Width - 30)), 
                    Constraint.RelativeToParent((parent) => random.Next(30, (int)parent.Height - 30)),
                    Constraint.Constant(30), 
                    Constraint.Constant(30)
                    );

                cancelEvent = () => layout.Children.Remove(redBox);
            }

            var task = this.startEvent(startEvent, random.Next(2000, 5000));

            button.Clicked += (sender, args) =>
            {
                if (task.IsCompleted)
                {
                    cancelEvent();
                    results.Add(DateTime.Now - task.Result);
                    if (results.Count == countOfTests)
                    {
                        player?.Stop();
                        player?.Release();
                        Navigation.PushAsync(new ResultPage(results, countOfMistakes, userResult, testType));
                    }
                    else
                        task = this.startEvent(startEvent, random.Next(2000, 5000));
                }
                else
                    countOfMistakes++;


            };
            Content = layout;
        }

        protected override bool OnBackButtonPressed()
        {
            player?.Stop();
            player?.Release();
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            player?.Stop();
            player?.Release();
            base.OnDisappearing();
        }
    }
}