﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Ventas.Entidades;
using Soft.Exceptions;
using Soft.Entities;
using Soft.DataAccess;
using NHibernate;
using System.Data.SqlClient;

namespace Soft.Ventas.Win
{
    public partial class FrmAsistenteSolicitudCotizacion : FrmParent
    {
        public FrmAsistenteSolicitudCotizacion()
        {
            InitializeComponent();
            Iniciar();
        }

        private InfoAsistenSolicitudCotizacion InfoAsistente = null;

        public void Iniciar() {
            InfoAsistente = new InfoAsistenSolicitudCotizacion();
            udtFechaCreacion.Value = InfoAsistente.FechaCreacion;
            ObtenerCodigoGrupo(InfoAsistente);
        }

        public void ObtenerCodigoGrupo(InfoAsistenSolicitudCotizacion Info) {
            try
            {
                using (ISession Sesion = m_SessionFactory.OpenSession())
                {
                    using (ITransaction Trans = Sesion.BeginTransaction())
                    {
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.CommandText = "pSF_Obtener_Grupo_SolicitudCotizacion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.Add("@CodigoGrupo",SqlDbType.Int).Direction = ParameterDirection.Output;
                        SqlCmd.ExecuteNonQuery();
                        Trans.Commit();
                        Info.CodigoGrupo = Convert.ToInt32(SqlCmd.Parameters["@CodigoGrupo"].Value);
                        txtCodigoGrupo.Value = Info.CodigoGrupo;
                    }
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubGenerarSolicitudCotizacion_Click(object sender, EventArgs e)
        {
            try
            {
                GenerarSolicitudDesdePlantilla Generacion = new GenerarSolicitudDesdePlantilla();
                Generacion.m_ObjectFlow = HelperNHibernate.GetEntityByID("Plantilla", InfoAsistente.Plantilla.ID);
                Generacion.Start();
                SolicitudCotizacion Solicitud = (SolicitudCotizacion)Generacion.m_ObjectFlow;
                Solicitud.Cliente = InfoAsistente.Cliente;
                Solicitud.TipoDocumento = (TipoDocumento)HelperNHibernate.GetEntityByID("TipoSolicitudCotizacion", InfoAsistente.TipoDocumento.ID);
                Solicitud.Moneda = InfoAsistente.Moneda;
                Solicitud.FechaCreacion = InfoAsistente.FechaCreacion;
                Solicitud.CodigoGrupo = InfoAsistente.CodigoGrupo;
                Solicitud.GenerarNumCp();
                IniciarSolicitud(Solicitud);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        public virtual void IniciarSolicitud(SolicitudCotizacion Solicitud) {
            FrmSolicitudCotizacion FrmSolicitud = new FrmSolicitudCotizacion();
            FrmSolicitud.m_ObjectFlow = Solicitud;
            FrmSolicitud.m_Modal = true;
            FrmSolicitud.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", "1DEDB5BA-376B-41CE-9923-29B6CF61B9E6");
            FrmSolicitud.Start();
            if(FrmSolicitud.m_ResultProcess == EnumResult.SUCESS ){
                CreateEntity Crear = new CreateEntity();
                Crear.m_ObjectFlow = FrmSolicitud.m_ObjectFlow;
                Crear.Start();
                FrmMain.RefreshView();
            }
        }

        public override void Cerrar(FormClosingEventArgs e) {
            return;
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
                TipoSolicitudCotizacion TipoDocumento = (TipoSolicitudCotizacion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoSolicitudCotizacion), "Tipo Solicitud de Cotización");
                if (TipoDocumento != null) {
                    InfoAsistente.TipoDocumento = TipoDocumento;
                    ssTipoDocumento.Text = InfoAsistente.TipoDocumento.Nombre;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                InfoAsistente.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarCliente = new FrmSelectedEntity();
                SocioNegocio Cliente = (SocioNegocio)FrmSeleccionarCliente.GetSelectedEntity(typeof(SocioNegocio), "Cliente");
                if (Cliente != null)
                {
                    InfoAsistente.Cliente = Cliente;
                    ssCliente.Text = InfoAsistente.Cliente.Nombre;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarMoneda = new FrmSelectedEntity();
                Moneda Moneda = (Moneda)FrmSeleccionarMoneda.GetSelectedEntity(typeof(Moneda), "Moneda");
                if (Moneda != null)
                {
                    InfoAsistente.Moneda = Moneda;
                    ssMoneda.Text = InfoAsistente.Moneda.Simbolo;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssPlantilla_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarPlantilla = new FrmSelectedEntity();
                Plantilla Plantilla = (Plantilla)FrmSeleccionarPlantilla.GetSelectedEntity(typeof(Plantilla), "Plantilla");
                if (Plantilla != null)
                {
                    InfoAsistente.Plantilla = Plantilla;
                    ssPlantilla.Text = InfoAsistente.Plantilla.Nombre;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

    }
}
