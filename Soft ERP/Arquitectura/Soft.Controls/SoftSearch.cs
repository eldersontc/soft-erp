using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Soft.Controls
{
    public partial class SoftSearch : UserControl
    {

        public event EventHandler Search;

        public SoftSearch()
        {
            InitializeComponent();
        }

        public override String Text { get { return txtSearch.Text; } set { txtSearch.Text = value; } }

        private void txtSearch_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            switch (e.Button.Key) { 
                case "Search":
                    Search(sender, e);
                    break;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter ){
                Search(sender, e);
            }
        }
    }
}
