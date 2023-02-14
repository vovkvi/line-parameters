using System;
using System.Linq;
using System.Windows.Forms;

namespace LineParameters
{
    public partial class ATTemplate : UserControl
    {
        public ATTemplate()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Name = "Автотрансформатор или силовой трансформатор";
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

        // Вкладка "Автоторансформатор"
        private void Calculate()
        {
            double Sv = tSv.Text.ToDouble();
            double Ss = tSs.Text.ToDouble();
            double Sn = tSn.Text.ToDouble();
            double Uv = tUv.Text.ToDouble();
            double Us = tUs.Text.ToDouble();
            double Un = tUn.Text.ToDouble();
            double Pkv = tPkv.Text.ToDouble();
            double Pks = tPks.Text.ToDouble();
            double Pkn = tPkn.Text.ToDouble();
            double Ukv = tUkv.Text.ToDouble();
            double Uks = tUks.Text.ToDouble();
            double Ukn = tUkn.Text.ToDouble();
            double Ph = tPh.Text.ToDouble();
            double Ih = tIh.Text.ToDouble();
            // Расчёт
            double _Zob = ((Pkv * Math.Pow(Uv, 2)) / Math.Pow(Sv, 2)) / 1000;
            double _Rv = Math.Round(_Zob / 2, 2);
            double _Rs = Math.Round(_Zob / 2, 2);
            double _Rn = Math.Round(_Rv * 2, 2);
            double _Xvs = (Ukv * Math.Pow(Uv, 2)) / (100 * Sv);
            double _Xvn = (Uks * Math.Pow(Uv, 2)) / (100 * Sv);
            double _Xsn = (Ukn * Math.Pow(Uv, 2)) / (100 * Sv);
            double _Xv = Math.Round(0.5 * (_Xvs + _Xvn - _Xsn), 2);
            double _Xs = Math.Round(0.5 * (_Xvs + _Xsn - _Xvn), 2);
            double _Xn = Math.Round(0.5 * (_Xsn + _Xvn - _Xvs), 2);
            double _Qh = Math.Round((Ih / 100) * Sv, 2);
            double _B = Math.Round((_Qh / Math.Pow(Uv, 2)) * 1000000, 2);
            double _G = Math.Round((Ph / Math.Pow(Uv, 2)) * 1000, 2);
            // Вывод результата
            lbR1.Text = _Rv.ToString();
            lbR2.Text = _Rs.ToString();
            lbR3.Text = _Rn.ToString();
            lbX1.Text = _Xv.ToString();
            lbX2.Text = _Xs.ToString();
            lbX3.Text = _Xn.ToString();
            lbK1.Text = "0";
            lbK2.Text = Math.Round(Us / Uv, 4).ToString();
            lbK3.Text = Math.Round(Un / Uv, 4).ToString();
            lbB1.Text = _B.ToString();
            lbG1.Text = _G.ToString();
            lbB2.Text = "0";
            lbB3.Text = "0";
            lbG2.Text = "0";
            lbG3.Text = "0";
            tRv.Text = "R1 = " + lbR1.Text;
            tRs.Text = "R2 = " + lbR2.Text;
            tRn.Text = "R3 = " + lbR3.Text;
            tXv.Text = "X1 = " + lbX1.Text;
            tXs.Text = "X2 = " + lbX2.Text;
            tXn.Text = "X3 = " + lbX3.Text;
            tG.Text = "G = " + lbG1.Text;
            tB.Text = "B = " + lbB1.Text;
            tKs.Text = "K1 = " + lbK2.Text;
            tKn.Text = "K2 = " + lbK3.Text;
        }
    }
}
