using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LineParameters
{
    public partial class LineTemplate : UserControl
    {       
        List<vl> Lines = new List<vl>();
        double Rz, Kx1, Kx1z, Kx2, Kx2z, Q35, Q110, Q220, Q330 = 0;

        public LineTemplate()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Name = "Воздушная линия";
            //----------------------------------------------------------
            //  Load line base from XML
            //----------------------------------------------------------
            XDocument xml = XDocument.Load("config.xml");
            Rz = xml.Root.Attribute("Rz").Value.ToDouble();
            Kx1 = xml.Root.Attribute("Kx1").Value.ToDouble();
            Kx1z = xml.Root.Attribute("Kx1z").Value.ToDouble();
            Kx2 = xml.Root.Attribute("Kx2").Value.ToDouble();
            Kx2z = xml.Root.Attribute("Kx2z").Value.ToDouble();
            Q35 = xml.Root.Attribute("Q35").Value.ToDouble();
            Q110 = xml.Root.Attribute("Q110").Value.ToDouble();
            Q220 = xml.Root.Attribute("Q220").Value.ToDouble();
            Q330 = xml.Root.Attribute("Q330").Value.ToDouble();
            foreach (XElement e in xml.Root.Elements()) 
            {
                Lines.Add(new vl()
                {
                    Name = e.Attribute("marka").Value,
                    R = e.Attribute("R").Value.ToDouble(),
                    X35 = e.Attribute("X35").Value.ToDouble(),
                    X110 = e.Attribute("X110").Value.ToDouble(),
                    X220 = e.Attribute("X220").Value.ToDouble(),
                    X330 = e.Attribute("X330").Value.ToDouble(),
                    B110 = e.Attribute("B110").Value.ToDouble(),
                    B220 = e.Attribute("B220").Value.ToDouble(),
                    B330 = e.Attribute("B330").Value.ToDouble()
                });
            }
            //----------------------------------------------------------
            foreach (vl ln in Lines)
                cbMarka.Items.Add(ln.Name);
            cbMarka.SelectedIndex = 0;
            cbType.SelectedIndex = 0;
            cbU.SelectedIndex = 0;
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
            Calculate(tbLine, null);
        }

        // Вкладка "Линия"
        private void Calculate(object sender, EventArgs e)
        {
            try
            {
                vl line = Lines[cbMarka.SelectedIndex];
                double km = tbLine.Text.ToDouble();
                double R = line.R;
                double R0, X, X0, C, B, Q = 0.0;
                switch (cbU.SelectedIndex)
                {
                    case 0: //35
                        X = line.X35;
                        B = line.B35;
                        Q = Q35;
                        break;
                    case 1: //110
                        X = line.X110;
                        B = line.B110;
                        Q = Q110;
                        break;
                    case 2: //220
                        X = line.X220;
                        B = line.B220;
                        Q = Q220;
                        break;
                    case 3: //330
                        X = line.X330;
                        B = line.B330;
                        Q = Q330;
                        break;
                    default:
                        X = B = Q = 0;
                        break;
                }
                if (cbType.SelectedIndex == 0 && chbGZT.Checked == false) // 1Ц
                    X0 = Kx1;
                else if (cbType.SelectedIndex == 1 && chbGZT.Checked == false) // 2Ц
                    X0 = Kx2;
                else if (cbType.SelectedIndex == 0 && chbGZT.Checked == true) // 1Ц + ГЗТ
                    X0 = Kx1z;
                else if (cbType.SelectedIndex == 1 && chbGZT.Checked == true) // 2Ц + ГЗТ
                    X0 = Kx2z;
                else
                    X0 = 0;
                R0 = R + Rz;
                C = Math.Round(B / 314, 4);
                //----------------------------------------------------------
                // Print nominal
                //----------------------------------------------------------
                lblU.Text = cbU.Text;
                lblM.Text = cbMarka.Text;
                lblQg.Text = Q.ToString();
                lblR.Text = R.ToString();
                lblR0.Text = R0.ToString();
                lblX.Text = X.ToString();
                lblX0.Text = X0.ToString();
                lblB.Text = B.ToString();
                lblC.Text = C.ToString();
                //----------------------------------------------------------
                // Print calculate result
                //----------------------------------------------------------
                lbRr.Text = Math.Round((R * km), 2).ToString();
                lblRr0.Text = Math.Round((R0 * km), 2).ToString();
                lbXr.Text = Math.Round((X * km), 2).ToString();
                lbXr0.Text = Math.Round((X * X0) * km, 2).ToString();
                lbBr.Text = Math.Round((B * km), 2).ToString();
                lblCr.Text = Math.Round((C * km), 2).ToString();
                lblQgr.Text = Math.Round((Q * km), 2).ToString();     
                textR.Text = "R = " + lbRr.Text;
                textX.Text = "X = " + lbXr.Text;
                textB.Text = "B = " + lbBr.Text;
            }
            catch { }
        }
    }
}
