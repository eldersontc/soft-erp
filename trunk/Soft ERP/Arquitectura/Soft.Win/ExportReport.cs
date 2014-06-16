using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Entities;
using Soft.Reporte.Entidades;
using CReporte = Soft.Reporte.Entidades.Reporte;
using CrystalDecisions.CrystalReports.Engine;
using Soft.Exceptions;
using CrystalDecisions.Shared;
using System.Diagnostics;

namespace Soft.Win
{
    public class ExportReport : ControllerApp
    {
        public override void Start()
        {
            FrmProgress frmProgreso = new FrmProgress();
            frmProgreso.Start(2, "Exportando ...");
            frmProgreso.Next();
            try
            {
                CReporte reporte = null;
                ExportFormatOptions opciones = null;
                ExportFormatType formato;
                string nombreArchivo = string.Empty;
                string extension = string.Empty;
                string sql = string.Empty;
                if (base.m_ObjectFlow is DocumentoGenerico)
                {
                    DocumentoGenerico documento = (DocumentoGenerico)base.m_ObjectFlow;
                    reporte = documento.TipoDocumento.Reporte;
                    nombreArchivo = string.Format("{0} - Nº {1} {2}", reporte.Nombre, documento.Numeracion, DateTime.Now.ToString("yyyy-MM-dd"));
                    sql = reporte.SQL;
                    foreach (ParametroReporte Parametro in reporte.ParametrosSQL)
                        sql = sql.Replace(Parametro.Nombre, documento.ValueByProperty(Parametro.Propiedad).ToString());
                }
                else if (base.m_ObjectFlow is CReporte)
                {
                    reporte = (CReporte)base.m_ObjectFlow;
                    nombreArchivo = string.Format("{0} {1}", reporte.Nombre, DateTime.Now.ToString("yyyy-MM-dd"));
                    sql = reporte.SQL;
                    foreach (ParametroReporte Parametro in reporte.ParametrosSQL)
                        sql = sql.Replace(Parametro.Nombre, Parametro.Valor);
                }
                switch (base.m_Parameter)
                {
                    case TypeEnum.CEnumExportFormat.PDF:
                        formato = ExportFormatType.PortableDocFormat;
                        opciones = new PdfRtfWordFormatOptions();
                        extension = ".pdf";
                        break;
                    case TypeEnum.CEnumExportFormat.WORD:
                        formato = ExportFormatType.WordForWindows;
                        opciones = new PdfRtfWordFormatOptions();
                        extension = ".doc";
                        break;
                    case TypeEnum.CEnumExportFormat.EXCEL:
                        formato = ExportFormatType.Excel;
                        opciones = new ExcelFormatOptions();
                        extension = ".xls";
                        break;
                    default:
                        throw new Exception("El formato no es válido.");
                }
                if (reporte != null)
                {
                    
                    ReportDocument CryRpt = new ReportDocument();
                    CryRpt.Load(String.Format("{0}{1}", FrmMain.CarpetaReportes, reporte.Ubicacion));
                    frmProgreso.Next();
                    // Si existe una consulta SQL se ejecuta.
                    if (sql.Trim().Length > 0) { CryRpt.SetDataSource(HelperNHibernate.GetDataSet(sql)); }
                    // Se reemplazan los parámetros Crystal.
                    foreach (ParametroReporte Parametro in reporte.ParametrosCrystal)
                        CryRpt.SetParameterValue(Parametro.Nombre, Parametro.Valor);
                    // Se exporta el reporte.
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    CrDiskFileDestinationOptions.DiskFileName = string.Format("{0}{1}{2}", FrmMain.CarpetaExportacion, nombreArchivo, extension);
                    CrExportOptions = CryRpt.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = formato;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = opciones;
                    }
                    // Se exporta el archivo.
                    CryRpt.Export();
                    // Se inicia un proceso para abrir el archivo.
                    Process.Start(CrDiskFileDestinationOptions.DiskFileName);
                }
                else
                    throw new Exception("Entidad no válida.");
                base.m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
            finally
            {
                frmProgreso.Close();
                base.Start();
            }
        }
    }
}
