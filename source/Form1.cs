using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LineParameters
{
    public partial class Form1 : Form
    {
        List<Control> Templates = new List<Control>() 
        { 
            new LineTemplate(),
            new ATTemplate(),
            new DualTemplate(),
            new RaschTemplate()
        };
    	
        public Form1()
        {
            InitializeComponent();
            Text += " v." + Application.ProductVersion;
            DoubleBuffered = true;
            foreach (Control c in Templates)
                cbMode.Items.Add(c.Name);
            cbMode.SelectedIndex = 0;
        }

        void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            Control ctrl = Templates[((ComboBox)sender).SelectedIndex];
            int i = 0;
            if (pnlContent.Controls.Contains(ctrl))
            {
                i = pnlContent.Controls.IndexOf(ctrl);  
            }
            else
            {
                pnlContent.Controls.Add(ctrl);
                i = pnlContent.Controls.Count - 1;
            }
            pnlContent.Controls[i].BringToFront();
            pnlContent.Focus();
        }
    }
}
