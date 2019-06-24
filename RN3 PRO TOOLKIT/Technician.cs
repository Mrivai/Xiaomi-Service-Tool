using MetroFramework.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RN3_PRO_TOOLKIT
{
    public partial class Technician : MetroForm
    {
        public Technician()
        {
            InitializeComponent();
            Height = Screen.PrimaryScreen.Bounds.Height;
            Width = Screen.PrimaryScreen.Bounds.Width;
        }
    }
}
