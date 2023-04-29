using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfMvvmListViewApp.Converter
{
    public class TextBoxNamesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            #region Tuple로 TextBox 컨트롤을 넘기는 방법
            TextBox textBox1 = (TextBox)values[0];
            TextBox textBox2 = (TextBox)values[1];
            return new Tuple<TextBox, TextBox>(textBox1, textBox2);
            #endregion

            #region Tuple 또는 정적배열을 이용해서 TextBox이름을 넘기는 방법
            //return new Tuple<string, string>((string)values[0], (string)values[1]);

            // TextBox 컨트롤 이름을 넘기는 방법
            //string[] textBoxNames = new string[2];
            //if (values.Length == 2 && values[0] is TextBox && values[1] is TextBox)
            //{
            //    textBoxNames[0] = ((TextBox)values[0]).Name;
            //    textBoxNames[1] = ((TextBox)values[1]).Name;
            //}
            //return textBoxNames;
            #endregion

            #region List 동적배열에 TextBox 컨트롤 담아서 넘기는 방법
            //var textBoxes = new List<TextBox>();
            //foreach (var value in values)
            //{
            //    if (value is TextBox textBox)
            //    {
            //        textBoxes.Add(textBox);
            //    }
            //}

            // textBoxes 리스트에 담긴 TextBox 컨트롤들을 사용하여 필요한 작업 수행
            // ToDo..

            //return textBoxes;
            #endregion
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
