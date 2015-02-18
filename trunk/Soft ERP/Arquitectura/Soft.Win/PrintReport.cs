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
using NHibernate;
using NHibernate.Criterion;

namespace Soft.Win
{
    public class PrintReport : ControllerApp
    {
        public override void Start()
        {
            try
            {
                if (string.IsNullOrEmpty(base.m_Parameter))
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
                }
                else 
                {
                    CReporte reporte = ObtenerReporte();
                    String sql = reporte.SQL;

                    foreach (ParametroReporte parametro in reporte.ParametrosSQL)
                    {
                        sql = sql.Replace(parametro.Nombre, base.m_ObjectFlow.GetType().GetProperty(parametro.Propiedad).GetValue(base.m_ObjectFlow, null).ToString());//.ValueByProperty(parametro.Propiedad).ToString());
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
                base.m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source);
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        public CReporte ObtenerReporte() 
        {
            CReporte reporte = null;
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                ICriteria criteria = Sesion.CreateCriteria<CReporte>();
                criteria.Add(Restrictions.Eq("Codigo", base.m_Parameter));
                IList<CReporte> lista = criteria.List<CReporte>();
                if (lista.Count > 0)
                {
                    reporte = lista.First();
                }
                else
                {
                    throw new Exception("No se ha encontrado ningún reporte con código: " + base.m_Parameter);
                }
            }
            return reporte;
        }
    }
}
