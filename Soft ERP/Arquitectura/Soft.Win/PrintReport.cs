﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Reporte.Entidades;
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
                if (base.m_ObjectFlow is Soft.Reporte.Entidades.Reporte)
                {
                    Soft.Reporte.Entidades.Reporte Reporte = (Soft.Reporte.Entidades.Reporte)base.m_ObjectFlow;
                    ReportDocument ReportDocument = new ReportDocument();
                    ReportDocument.Load(Reporte.Ubicacion);
                    foreach (ParametroReporte Parametro in Reporte.ParametrosCrystal)
                    {
                        ReportDocument.SetParameterValue(Parametro.Nombre, Parametro.Valor);
                    }
                    FrmMain.MostrarReporte(Reporte.Nombre, ReportDocument);
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
            //base.Start();
        }
    }
}
