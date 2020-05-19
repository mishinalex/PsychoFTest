using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PsychoTest
{
    static class Data
    {
        public const string ColorInfo =
            "Окно через произвольные промежутки времени будет загораться красным цветом. Как только оно загорится, нажмите на экран.";

        public const string RandomPointInfo =
            "Через произвольные промежутки времени в произвольном месте будет загораться квадрат. Как только он загорится, нажимаете на экран.";

        public const string SoundInfo =
            "Через произвольные промежутки времени будет включаться звуковой сигнал. Как только он включается, нажимаете на на экран.";

        public const string ColorPointInfo =
            "Через произвольные промежутки времени в произвольном месте будет загораться квадрат. Как только он загорится, нажимаете на экран.";

        public const string EvenOddInfo =
            "На экране будет появляться число. В ответ на четные числа нажимайте клавишу 2, на нечетные - клавишу 1";

        public const string ArrowInfo =
            "На экране будет таблица из символов \"←\", \"→\", \"↑\", \"↓\". Вы должны как можно быстрее сивол \"{0}\"";

        public const string IndividualMinuteInfo =
            "Внимательно всмотритесь в движение секундной стрелки, а затем без отсчета определите индивидуальную минуту.";

        public const string ColorName = "Реакция на цвет";

        public const string SoundName = "Реакция на звук";

        public const string RandomPointName = "Реакция на цвет в области экрана";

        public const string ColorPointName = "Распознавание цвета";

        public const string EvenOddName = "Распознавание четных чисел";

        public const string ArrowName = "Сосредоточенность внимания";

        public const string IndividualMinuteName = "Индивидуальная минута";

        public static string ResultPath =>  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "results2.csv");

        public static string ExportPath => Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).AbsolutePath, "results2.xls");

    }
}
