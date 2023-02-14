using System;
using System.Linq;
using System.Windows.Forms;

namespace LineParameters
{
    public partial class RaschTemplate : UserControl
    {
        public RaschTemplate()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Name = "Трансформатор с ращепленной обмоткой низкого напряжения";
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

        // Вкладка "Трансформатор с расчепленной НН"
        private void Calculate()
        {
            double Sv = t3Sv.Text.ToDouble();
            double Sn1 = t3Sn1.Text.ToDouble();
            double Sn2 = t3Sn2.Text.ToDouble();
            double Uv = t3Uv.Text.ToDouble();
            double Un1 = t3Un1.Text.ToDouble();
            double Un2 = t3Un2.Text.ToDouble();
            double Pk = t3Pk.Text.ToDouble();
            double Uk = t3Uk.Text.ToDouble();
            double Ph = t3Ph.Text.ToDouble();
            double Ih = t3Ih.Text.ToDouble();
            // Расчёт
            double _Rob = ((Pk * Math.Pow(Uv, 2)) / Math.Pow(Sv, 2)) / 1000;
            double _Rv = Math.Round(_Rob / 2, 2);
            double _Rn1 = Math.Round(_Rv * 2, 2);
            double _Rn2 = Math.Round(_Rv * 2, 2);
            double _Xob = (Uk * Math.Pow(Uv, 2)) / (100 * Sv);
            double _Xv = Math.Round(_Xob * 0.125, 2);
            double _Xn1 = Math.Round(_Xob * 1.75, 2);
            double _Xn2 = Math.Round(_Xob * 1.75, 2);
            double _dQ = Math.Round((Ih / 100) * Sv, 2);
            double _B = Math.Round((_dQ / Math.Pow(Uv, 2)) * 1000000, 2);
            double _G = Math.Round((Ph / Math.Pow(Uv, 2)) * 1000, 2);
            // Вывод результата
            lb3R1.Text = _Rv.ToString();
            lb3R2.Text = _Rn1.ToString();
            lb3R3.Text = _Rn2.ToString();
            lb3X1.Text = _Xv.ToString();
            lb3X2.Text = _Xn1.ToString();
            lb3X3.Text = _Xn2.ToString();
            lb3B1.Text = _B.ToString();
            lb3G1.Text = _G.ToString();
            lb3K1.Text = "0";
            lb3K2.Text = Math.Round(Un1 / Uv, 4).ToString();
            lb3K3.Text = Math.Round(Un2 / Uv, 4).ToString();
            lb3B2.Text = "0";
            lb3B3.Text = "0";
            lb3G2.Text = "0";
            lb3G3.Text = "0";
            t3Rv.Text = "R1= " + lb3R1.Text;
            t3Rn1.Text = "R2= " + lb3R2.Text;
            t3Rn2.Text = "R3= " + lb3R3.Text;
            t3Xv.Text = "X1= " + lb3X1.Text;
            t3Xn1.Text = "X2= " + lb3X2.Text;
            t3Xn2.Text = "X3= " + lb3X3.Text;
            t3G.Text = "G= " + lb3G1.Text;
            t3B.Text = "B= " + lb3B1.Text;
            t3K1.Text = "K1= " + lb3K2.Text;
            t3K2.Text = "K2= " + lb3K3.Text;
        }
    }
}
