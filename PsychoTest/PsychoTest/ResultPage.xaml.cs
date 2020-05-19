
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
        List<String> itemSource = new List<string>();
        ResultPage()
        {
            InitializeComponent();
            Title = "Результаты";
            //костыл
      

            var listView = new ListView
            {
                ItemsSource = itemSource,
                
  


            };

            
            var relativeLayout = new RelativeLayout();

            relativeLayout.Children.Add(
                listView,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.50)
            );

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

            Content = relativeLayout;
        }
        public ResultPage(List<TimeSpan> results, int countOfMistakes) : this()
        {
            var seconds = results.Select(r => r.TotalSeconds);
            var stringSeconds = string.Join(" ", seconds.Select(n => string.Format("{0:N2} сек.", n)));
            itemSource.Add(string.Format("Результат: {0}", stringSeconds));
            itemSource.Add(string.Format("Среднее: {0:N2} сек.", seconds.Average()));
            itemSource.Add(string.Format("Количество ошибочных нажатий: {0}", countOfMistakes));

        }

        public ResultPage(int countOfCorrect, int countOfMistakes, int countOfArrows, TimeSpan resultTime) : this()
        {
            itemSource.Add(string.Format("Количетсво правильно отмеченных {0} из {1}", countOfCorrect, countOfArrows / 4));
            itemSource.Add(string.Format("Количетсво неправильно отмеченных {0} из {1}", countOfMistakes, countOfArrows * 3 / 4));
            itemSource.Add(string.Format("Время {0:N2} сек.", resultTime.TotalSeconds));
        }

        public ResultPage(TimeSpan resultTime) : this()
        {
            itemSource.Add(string.Format("Индивидуальная минута {0:N2} сек.", resultTime.TotalSeconds));
        }
    }
}