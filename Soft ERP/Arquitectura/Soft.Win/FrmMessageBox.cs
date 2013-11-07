using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.DataAccess;
using Infragistics.Win.Misc;

namespace Soft.Win
{
    public partial class FrmMessageBox : ControllerApp 
    {
        public FrmMessageBox()
        {
            InitializeComponent();
        }

        public override void Start()
        {
            Show(MessageBoxIcon.Question);
            ShowDialog();
        }

        public void Show(MessageBoxIcon Icon) {
            switch (Icon)
            {
                case MessageBoxIcon.Error:
                    MakeStyleError();
                    break;
                case MessageBoxIcon.Question:
                    MakeStyleConfirmation();
                    break;
                default:
                    break;
            }
        }

        public void MakeStyleError() {
            
        }

        public void MakeStyleConfirmation() {
            base.Text = "Confirmación";
            base.Size = new System.Drawing.Size(225, 150);
            UltraLabel LabelMsg = new UltraLabel();
            LabelMsg.Text = "¿ Seguro de Continuar ?";
            LabelMsg.Location = new System.Drawing.Point(35, 10);
            LabelMsg.Size = new System.Drawing.Size(160, 25);
            LabelMsg.Appearance.BackColor = System.Drawing.Color.Transparent;
            UltraButton ButtonAcept = new UltraButton();
            ButtonAcept.Text = "Si";
            ButtonAcept.Location = new System.Drawing.Point(20, 40);
            ButtonAcept.Click += ButtonAcept_Click;
            UltraButton ButtonCancel = new UltraButton();
            ButtonCancel.Text = "No";
            ButtonCancel.Location = new System.Drawing.Point(100, 40);
            ButtonCancel.Click += ButtonCancel_Click;
            ugbMessageBox.Controls.Add(LabelMsg);
            ugbMessageBox.Controls.Add(ButtonAcept);
            ugbMessageBox.Controls.Add(ButtonCancel);
        }

        private void ButtonAcept_Click(object sender, EventArgs e)
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
            base.Start();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            base.m_ResultProcess = EnumResult.CANCEL;
            Close();
            base.Start();
        }

    }
}
