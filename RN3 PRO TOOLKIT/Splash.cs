using MetroFramework.Forms;
using System;

namespace RN3_PRO_TOOLKIT
{
    public partial class Splash : MetroForm
    {
        static int count = 0;
        public Splash()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            count += 1;
            progres.Value = count;
            if (progres.Value == 1000)
            {
                timer1.Enabled = false;
            }
        }

        private void Splash_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Close();
        }
    }
}
