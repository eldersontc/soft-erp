using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Entities;
using Soft.Reporte.Entidades;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;

namespace Soft.Win
{
    public class PrintDocument : ControllerApp 
    {
        public override void Start()
        {
            try
            {
                if (base.m_ObjectFlow is DocumentoGenerico)
                {
                    DocumentoGenerico Documento = (DocumentoGenerico)base.m_ObjectFlow;
                    Soft.Reporte.Entidades.Reporte Reporte = Documento.TipoDocumento.Reporte;
                    String SQL = Reporte.SQL;
                    foreach (ParametroReporte Parametro in Reporte.ParametrosSQL)
                    {
                        SQL = SQL.Replace(Parametro.Nombre, Documento.ValueByProperty(Parametro.Propiedad).ToString());
                    }
                    ReportDocument Document = new ReportDocument();
                    Document.Load(String.Format("{0}{1}", FrmMain.CarpetaReportes, Reporte.Ubicacion));
                    Document.SetDataSource(HelperNHibernate.GetDataSet(SQL));
                    foreach (ParametroReporte Parametro in Reporte.ParametrosCrystal)
                    {
                        Document.SetParameterValue(Parametro.Nombre,Parametro.Valor);
                    }
                    FrmMain.MostrarReporte(Reporte.Nombre,Document);
                }
                else
                {
                    throw new Exception("No se ha seleccionado ningún Documento ...");
                }
                base.m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source);
                MessageBox.Show(ex.InnerException.Message);
            }
            base.Start();
        }
    }
}
