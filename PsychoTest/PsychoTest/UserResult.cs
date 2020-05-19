using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace PsychoTest
{
    public class UserResult
    {


        public static List<UserResult> Load()
        {
            var path = Data.ResultPath;
            var result = new List<UserResult>();
            if (File.Exists(path))
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentUICulture))
                {
                    result =  csv.GetRecords<UserResult>().ToList();
                }
            return result;
        }

        public static async void Upload(IEnumerable<UserResult> records, string path)
        {
            //var path = Data.ResultPath;
            if (!File.Exists(path))
                File.Create(path);

            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentUICulture))
            {
                await csv.WriteRecordsAsync(records);
            }
        }

        public static void Upload(IEnumerable<UserResult> records)
        {
            Upload(records, Data.ResultPath);
        }

        public void Save()
        {
            var records = Load();
            records.Add(this);
            Upload(records);
        }

        public View GetView(bool write = false)
        {

            var thisType = this.GetType();



            var labelsEntries = thisType.GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .Where(p => write || p.GetValue(this) != null)
                .Select(p => (
                    label: new Label(),
                    entry: new Entry(),
                    property: p
                ));

            var parent = new StackLayout();
            parent.HorizontalOptions = LayoutOptions.FillAndExpand;
            
 
            foreach (var (label, entry, property) in labelsEntries)
            {
                label.Text = ((NameAttribute)Attribute.GetCustomAttribute(property, typeof(NameAttribute))).Names.First();
                
                entry.SetBinding(Entry.TextProperty, new Binding { Source = this, Path = property.Name, Mode = BindingMode.TwoWay });
                entry.IsEnabled = write;

                parent.Children.Add(label);
                parent.Children.Add(entry);
                

            }

            return new Frame
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Content = parent
            };
        }

        public void Add(UserResult userResult)
        {
            var type = typeof(UserResult);
            var propertiesValues = type.GetProperties()
                .Select(p => (property: p, value: p.GetValue(userResult)))
                .Where(pv => pv.value != null);
            foreach (var (property, value) in propertiesValues)
            {
                property.SetValue(this, value);
            }


        }

        [Name("ФИО")]
        public string Name { get; set; }


        [Name("Возраст")]
        public int? Age { get; set; }

        [Ignore]
        [Name("Возраст")]
        public string AgeString
        {
            get => Age?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    Age = newValue;
            }
        }

        [Name("Стаж в профессии")]
        public int? ExpInPrfofession { get; set; } 

        [Ignore]
        [Name("Стаж в профессии")]
        public string ExpInPrfofessionString
        {
            get => ExpInPrfofession?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    ExpInPrfofession = newValue;
            }
        }

        [Name("Стаж в профессии")]
        public int? ExpInBad { get; set; }

        [Ignore]
        [Name("Стаж во вредных условия")]
        public string ExpInBadString
        {
            get => ExpInPrfofession?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    ExpInPrfofession = newValue;
            }
        }

        [Name(Data.ColorName + " время")]
        public double? TimeColorResult { get; set; }

        [Ignore]
        [Name(Data.ColorName + " время")]
        public string TimeColorResultString
        {
            get => TimeColorResult?.ToString("0.00 сек");
            set
            {
                if (double.TryParse(value, out var newValue))
                    TimeColorResult = newValue;
            }
        }

        [Name(Data.ColorName + " количество ошибок")]
        public int? CountOfMistakesColorResult { get; set; }

        [Ignore]
        [Name(Data.ColorName + " количество ошибок")]
        public string CountOfMistakesColorResultString
        {
            get => CountOfMistakesColorResult?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    CountOfMistakesColorResult = newValue;
            }
        }

        [Name(Data.SoundName + " время")]
        public double? TimeSoundResult { get; set; }

        [Ignore]
        [Name(Data.SoundName + " время")]
        public string TimeSoundResultString
        {
            get => TimeSoundResult?.ToString("0.00 сек");
            set
            {
                if (double.TryParse(value, out var newValue))
                    TimeSoundResult = newValue;
            }
        }

        [Name(Data.SoundName + " количество ошибок")]
        public int? CountOfMistakesSoundResult { get; set; }

        [Ignore]
        [Name(Data.SoundName + " количество ошибок")]
        public string CountOfMistakesSoundResultString
        {
            get => CountOfMistakesSoundResult?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    CountOfMistakesSoundResult = newValue;
            }
        }

        [Name(Data.RandomPointName + " время")]
        public double? TimeRandomPointResult { get; set; }

        [Ignore]
        [Name(Data.RandomPointName + " время")]
        public string TimeRandomPointResultString
        {
            get => TimeRandomPointResult?.ToString("0.00 сек");
            set
            {
                if (double.TryParse(value, out var newValue))
                    TimeRandomPointResult = newValue;
            }
        }

        [Name(Data.RandomPointName + " количество ошибок")]
        public int? CountOfMistakesRandomPointResult { get; set; }

        [Ignore]
        [Name(Data.RandomPointName + " количество ошибок")]
        public string CountOfMistakesRandomPointResultString
        {
            get => CountOfMistakesRandomPointResult?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    CountOfMistakesRandomPointResult = newValue;
            }
        }

        [Name(Data.ColorPointName + " время")]
        public double? TimeColorPointResult { get; set; }

        [Ignore]
        [Name(Data.ColorPointName + " время")]
        public string TimeColorPointResultString
        {
            get => TimeColorPointResult?.ToString("0.00 сек");
            set
            {
                if (double.TryParse(value, out var newValue))
                    TimeColorPointResult = newValue;
            }
        }

        [Name(Data.ColorPointName + " количество ошибок")]
        public int? CountOfMistakesColorPointResult;

        [Ignore]
        [Name(Data.ColorPointName + " количество ошибок")]
        public string CountOfMistakesColorPointResultString
        {
            get => CountOfMistakesColorPointResult?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    CountOfMistakesColorPointResult = newValue;
            }
        }

        [Name(Data.EvenOddName + " время")]
        public double? TimeEvenOddResult { get; set; }

        [Ignore]
        [Name(Data.EvenOddName + " время")]
        public string TimeEvenOddResultString
        {
            get => TimeEvenOddResult?.ToString("0.00 сек");
            set
            {
                if (double.TryParse(value, out var newValue))
                    TimeEvenOddResult = newValue;
            }
        }

        [Name(Data.EvenOddName + " количество ошибок")]
        public int? CountOfMistakesEvenOddResult { get; set; }

        [Ignore]
        [Name(Data.EvenOddName + " количество ошибок")]
        public string CountOfMistakesEvenOddResultString
        {
            get => CountOfMistakesEvenOddResult?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    CountOfMistakesEvenOddResult = newValue;
            }
        }

        [Name(Data.ArrowName + " время")]
        public double? TimeArrowResult { get; set; }

        [Ignore]
        [Name(Data.ArrowName + " время")]
        public string TimeArrowResultString
        {
            get => TimeArrowResult?.ToString("0.00 сек");
            set
            {
                if (double.TryParse(value, out var newValue))
                    TimeArrowResult = newValue;
            }
        }

        [Name(Data.ArrowName + " количество правильных")]
        public int? CountOfCorrectArrowResult { get; set; }

        [Ignore]
        [Name(Data.ArrowName + " количество правильных")]
        public string CountOfCorrectArrowResultString
        {
            get => CountOfCorrectArrowResult?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    CountOfCorrectArrowResult = newValue;
            }
        }

        [Name(Data.ArrowName + " количество ошибок")]
        public int? CountOfMistakesArrowResult { get; set; }

        [Ignore]
        [Name(Data.ArrowName + " количество ошибок")]
        public string CountOfMistakesArrowResultString
        {
            get => CountOfMistakesArrowResult?.ToString();
            set
            {
                if (int.TryParse(value, out var newValue))
                    CountOfMistakesArrowResult = newValue;
            }
        }

        [Name(Data.IndividualMinuteName)]
        public double? TimeIndividualMinuteResult { get; set; }

        [Ignore]
        [Name(Data.IndividualMinuteName)]
        public string TimeIndividualMinuteResultString
        {
            get => TimeIndividualMinuteResult?.ToString("0.00 сек");
            set
            {
                if (double.TryParse(value, out var newValue))
                    TimeIndividualMinuteResult = newValue;
            }
        }

    }
}
