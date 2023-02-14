using System;
using System.Linq;
using System.Windows.Forms;

namespace LineParameters
{
    public partial class DualTemplate : UserControl
    {
        public DualTemplate()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Name = "Двухобмоточный трансформатор";
        }

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && !(e.KeyChar == ',') && !(e.KeyChar == '.'))
                if (e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
        }

        private void tb_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            tb.Text = tb.Text.Replace('.', ',');
            if (tb.Text.Count<char>(x => x == ',') > 1)
                tb.Text = tb.Text.TrimEnd(',');
            tb.SelectionStart = tb.Text.Length;
            Calculate();
        }

        // Вкладка "Двухобмоточный трансформатор"
        private void Calculate()
        {
            double Sv = t2Sv.Text.ToDouble();
            double Sn = t2Sn.Text.ToDouble();
            double Uv = t2Uv.Text.ToDouble();
            double Un = t2Un.Text.ToDouble();
            double Pk = t2Pk.Text.ToDouble();
            double Uk = t2Uk.Text.ToDouble();
            double Ph = t2Ph.Text.ToDouble();
            double Ih = t2Ih.Text.ToDouble();
            // Расчёт
            double _R = Math.Round(((Pk * Math.Pow(Uv, 2)) / Math.Pow(Sv, 2)) / 1000, 2);
            double _X = Math.Round((Uk * Math.Pow(Uv, 2)) / (100 * Sv), 2);
            double _dQ = (Ih * Sv) / 100;
            double _B = Math.Round((_dQ / Math.Pow(Uv, 2)) * 1000000, 2);
            double _G = Math.Round((Ph / Math.Pow(Uv, 2)) * 1000, 2);
            double _K = Math.Round(Un / Uv, 4);
            // Вывод результата
            lb2R.Text = _R.ToString();
            lb2X.Text = _X.ToString();
            lb2B.Text = _B.ToString();
            lb2K.Text = _K.ToString();
            lb2G.Text = _G.ToString();
            t2R.Text = "R = " + lb2R.Text;
            t2X.Text = "X = " + lb2X.Text;
            t2B.Text = "B = " + lb2B.Text;
            t2K.Text = "Kт = " + lb2K.Text;
            t2G.Text = "G = " + lb2G.Text;
        }
    }
}
