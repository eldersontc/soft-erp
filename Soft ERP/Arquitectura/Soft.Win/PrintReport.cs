using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Reporte.Entidades;
using CReporte = Soft.Reporte.Entidades.Reporte;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using Soft.DataAccess;

namespace Soft.Win
{
    public class PrintReport : ControllerApp
    {
        public override void Start()
        {
            try
            {
                if (base.m_ObjectFlow is CReporte)
                {
                    CReporte Reporte = (CReporte)base.m_ObjectFlow;
                    String SQL = Reporte.SQL;
                    ReportDocument ReportDocument = new ReportDocument();
                    ReportDocument.Load(String.Format("{0}{1}", FrmMain.CarpetaReportes, Reporte.Ubicacion));
                    foreach (ParametroReporte Parametro in Reporte.ParametrosSQL)
                    {
                        SQL = SQL.Replace(Parametro.Nombre, Parametro.Valor);
                    }

                    if (SQL.Trim().Length > 0) { ReportDocument.SetDataSource(HelperNHibernate.GetDataSet(SQL)); }

                    foreach (ParametroReporte Parametro in Reporte.ParametrosCrystal)
                    {
                        ReportDocument.SetParameterValue(Parametro.Nombre, Parametro.Valor);
                    }
                    FrmMain.MostrarReporte(Reporte.Nombre, ReportDocument);
                }
                else
                {
                    throw new Exception("No se ha seleccionado ningún Reporte ...");
                }
                base.m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source);
                MessageBox.Show(ex.InnerException.Message);
            }
        }
    }
}
