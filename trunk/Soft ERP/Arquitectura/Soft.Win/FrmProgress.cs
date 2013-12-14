using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Soft.Win
{
    public partial class FrmProgress : Form
    {
        public FrmProgress()
        {
            InitializeComponent();
        }

        private Int32 m_Lenght = 0; 

        public void Start(Int32 Lenght, String Title) {
            m_Lenght = Lenght;
            Text = Title;
        }

        public void Next() {
            upbProgress.Value += upbProgress.Value + (100 / m_Lenght); 
        }

    }
}
