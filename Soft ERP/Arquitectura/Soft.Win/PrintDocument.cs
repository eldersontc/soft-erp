using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Entities;
using Soft.Reporte.Entidades;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using EReporte = Soft.Reporte.Entidades.Reporte;

namespace Soft.Win
{
    public class PrintDocument : ControllerApp 
    {
        public override void Start()
        {
            try
            {
                object documento = (object)base.m_ObjectFlow;
                if (!(documento.GetType().GetProperty("TipoDocumento") == null))
                {
                    TipoDocumento tipoDocumento = (TipoDocumento)documento.GetType().GetProperty("TipoDocumento").GetValue(documento, null);
                    EReporte reporte = tipoDocumento.Reporte;
                    String sql = reporte.SQL;

                    foreach (ParametroReporte parametro in reporte.ParametrosSQL)
                    {
                        sql = sql.Replace(parametro.Nombre, documento.GetType().GetProperty(parametro.Propiedad).GetValue(documento, null).ToString());//.ValueByProperty(parametro.Propiedad).ToString());
                    }

                    ReportDocument reportDocument = new ReportDocument();
                    reportDocument.Load(String.Format("{0}{1}", FrmMain.CarpetaReportes, reporte.Ubicacion));
                    reportDocument.SetDataSource(HelperNHibernate.GetDataSet(sql));

                    foreach (ParametroReporte parametro in reporte.ParametrosCrystal)
                    {
                        reportDocument.SetParameterValue(parametro.Nombre, parametro.Valor);
                    }

                    FrmMain.MostrarReporte(reporte.Nombre, reportDocument);
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
