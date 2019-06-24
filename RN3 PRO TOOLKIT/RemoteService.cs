using MetroFramework.Forms;
using System;

namespace RN3_PRO_TOOLKIT
{
    public partial class RemoteService : MetroForm
    {
        public RemoteService()
        {
            InitializeComponent();
        }

        private void PelitabangsaBrowser_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {
            var res = PelitabangsaBrowser.DocumentText;
            // if user hasbeen login
            // extract user phone number and user id as remote service username(phonenumber) and password(user id).
            if (res.Contains("logedin"))
            {
                //exract phone number and user id
            }
        }
    }
}
