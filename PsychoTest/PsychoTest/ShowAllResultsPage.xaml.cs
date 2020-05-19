using CsvHelper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PsychoTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowAllResultsPage : ContentPage
    {

        IList<UserResult> loadedResults;


        private static void ConvertCsvToExcel(string excelFileName, string worksheetName, string csvFileName)
        {

            bool firstRowIsHeader = true;

            var format = new ExcelTextFormat();
            format.Delimiter = ';';
            // format.EOL = "\r";           


            using (ExcelPackage package = new ExcelPackage(new FileInfo(excelFileName)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetName);
                worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFileName), format, OfficeOpenXml.Table.TableStyles.Medium27, firstRowIsHeader);
                package.Save();
            }

        }

        public ShowAllResultsPage()
        {
            InitializeComponent();

            var stackLayout = new StackLayout
            {
                Spacing = 10
            };

            loadedResults = UserResult.Load();

            loadedResults.Reverse();



            var views = loadedResults
                .Select(r => (result: r, view: new StackLayout()))
                .Select((rv) =>
                {
                    rv.view.Orientation = StackOrientation.Horizontal;
                    rv.view.Children.Add(rv.result.GetView());
                    rv.view.Children.Add(new Button
                    {
                        Text = "✕",
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        Command = new Command(() =>
                        {
                            stackLayout.Children.Remove(rv.view);
                            loadedResults.Remove(rv.result);
                            UserResult.Upload(loadedResults);
                        })
                    });
                    return rv.view;
                });

            stackLayout = new StackLayout();
            
            stackLayout.Spacing = 6;

            foreach (var view in views)
                stackLayout.Children.Add(view);

            var parentStackLayout = new StackLayout
            {
                Children =
                {
                    new ScrollView
                    {
                        Content = stackLayout
                    },
                    new Button
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        Text = "Экспортировать в Excel",
                        Command = new Command(() =>
                        {
                            ConvertCsvToExcel(Data.ExportPath, "1", Data.ResultPath);
                            //UserResult.Upload(loadedResults, Data.ExportPath);
                        })
                    },
                    new Button
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        Text = "Очистить все",
                        Command = new Command(() =>
                        {
                            stackLayout.Children.Clear();
                            UserResult.Upload(new List<UserResult>());

                        })
                    }
                }

            };


            Content = parentStackLayout;
        }

    }
}