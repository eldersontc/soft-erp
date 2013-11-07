using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;

namespace Soft.Controls
{
    public partial class SoftGrid : UserControl
    {
        public SoftGrid()
        {
            InitializeComponent();
        }

        public UltraGrid UltraGrid { get { return UltraGridx; } set { UltraGridx = value; } }

    }
}
