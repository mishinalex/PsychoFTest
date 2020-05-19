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
    public partial class ArrowPage : ContentPage
    {
        public enum Arrow
        {
            Up,
            Down,
            Left,
            Right,
            None
        }
        public static readonly string[] Arrows = { "↑", "↓", "←", "→", "✕" };


        public ArrowPage(Arrow correctArrow, UserResult userResult, TestType testType)
        {
            InitializeComponent();
            var countOfCorrect = 0;
            var countOfMistakes = 0;
            var layout = new RelativeLayout();
            var grid = new Grid();

            var gridWidth = 8;
            var gridHeight = 8;

            var countOfArrows = gridWidth * gridHeight;




            for (int i = 0; i < gridWidth; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star)}); 
            }

            for (int i = 0; i < gridHeight; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            var random = new Random();

            var views = Enumerable.Range(0, 4)
                .SelectMany(a => Enumerable.Range(0, countOfArrows / 4).Select(r => a))
                .OrderBy(a => random.Next())
                .Select(a =>
                new Button
                {
                    Text = Arrows[a],
                    FontSize = 30.0
                }
                )
                .Select((b, ind) => (view: b, left: ind % gridWidth, top: ind / gridHeight));

            foreach (var view in views)
            {
                view.view.Clicked += (a, b) =>
                {
                    if (view.view.Text == Arrows[(int)correctArrow])
                        countOfCorrect++;
                    else
                    {
                        if (view.view.Text != Arrows[(int)Arrow.None])
                            countOfMistakes++;
                        //else
                            
                    }
                    view.view.Text = Arrows[(int)Arrow.None];
                };
                grid.Children.Add(view.view, view.left, view.top);
            }

            layout.Children.Add(
                grid, 
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.75)
                );

            var startTime = DateTime.Now;

            layout.Children.Add(
                new Button
                {
                    Text = "Закончить",
                    Command = new Command(() => Navigation.PushAsync(
                        new ResultPage(countOfCorrect, countOfMistakes, countOfArrows, DateTime.Now - startTime, userResult, testType)
                        )
                    )
                },
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Height * 0.75),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.25)
                );
            Content = layout;
            
        }


    }
}
