using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Soft.Exceptions
{
    public partial class FrmMessageError : Form
    {
        public FrmMessageError()
        {
            InitializeComponent();
        }

        public void ShowError(String ex, String Details, Bitmap Icon)
        {
            lblMessage.Text = ex;
            txtDetails.Text = Details;
            upbIcon.Image = Icon;
            ShowDialog();
        }

        private void ugbDetails_ExpandedStateChanging(object sender, CancelEventArgs e)
        {
            if (!ugbDetails.Expanded)
            { 
                Height = 250;
            }
            else { Height = 112; }
        }

    }
}
