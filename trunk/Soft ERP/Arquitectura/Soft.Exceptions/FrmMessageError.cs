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

        public void ShowError(String Ex, String Details, Bitmap Icon)
        {
            lblMessage.Text = Ex;
            txtDetails.Text = Details;
            upbIcon.Image = Icon;
            ShowDialog();
        }

    }
}
