using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace PsychoTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavePage : ContentPage
    {

        public SavePage(UserResult userResult)
        {
            InitializeComponent();

 


            var sumbit = new Button
            {
                Text = "Сохранить",
                Command = new Command(() =>
                {
                    userResult.Save();
                    userResult = new UserResult();
                    Navigation.PopToRootAsync();
                })
            };

            var scrollView = new ScrollView
            {
                Content = userResult.GetView(true)
            };

            
            Content = new StackLayout
            {
                Children =
                {
                    scrollView,
                    sumbit
                }
            };
        }
    }
}