using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Soft.Win
{
    public partial class FrmReportView : Form
    {
        public FrmReportView()
        {
            InitializeComponent();
        }

        public FrmReportView(String NameReport,ReportDocument Report,Form FrmParent) {
            InitializeComponent();
            MdiParent = FrmParent;
            ((FrmMain)FrmParent).DeshabilitarOpciones();
            ViewReport(NameReport,Report);
            Show();
        }

        public void ViewReport(String NameReport,ReportDocument Report)
        {
            Text = String.Format(":: {0} ::",NameReport);
            crvReport.ReportSource = Report;
            crvReport.Zoom(75);
            crvReport.Refresh(); 
        }

    }
}
