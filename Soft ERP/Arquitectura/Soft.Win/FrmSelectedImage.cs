using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Infragistics.Win.UltraWinListView;

namespace Soft.Win
{
    public partial class FrmSelectedImage : FrmParent
    {
        public FrmSelectedImage()
        {
            InitializeComponent();
        }

        public void MostrarImagenes() {
            foreach (String Key in ilMain.Images.Keys)
            {
                UltraListViewItem Item = ulvImagenes.Items.Add(Key,Key);
                Item.Appearance.Image = base.ilMain.Images[Key];
            }
        }

        public String GetSelectedImage() {
            MostrarImagenes();
            ShowDialog();
            return ulvImagenes.ActiveItem.Key;
        }

        public override void Cancelar()
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
        }

        private void ubSeleccionar_Click(object sender, EventArgs e)
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
        }

        private void ulvImagenes_DoubleClick(object sender, EventArgs e)
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
        }

        private void ulvImagenes_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    base.m_ResultProcess = EnumResult.SUCESS;
                    Close();
                    break;
                case Keys.Escape:
                    base.m_ResultProcess = EnumResult.SUCESS;
                    Close();
                    break;
            }
        }
    }
}
