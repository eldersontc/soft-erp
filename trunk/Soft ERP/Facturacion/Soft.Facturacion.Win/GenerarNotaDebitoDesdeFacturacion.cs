using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Exceptions;
using Soft.Facturacion.Entidades;
using Soft.Entities;

namespace Soft.Facturacion.Win
{
    public class GenerarNotaDebitoDesdeFacturacion : ControllerApp
    {
        public override void Start()
        {
            try
            {
                Soft.Facturacion.Entidades.Facturacion Facturacion = (Soft.Facturacion.Entidades.Facturacion)m_ObjectFlow;
                NotaDebito NotaDebito = new NotaDebito();
                NotaDebito.Cliente = Facturacion.Cliente;
                NotaDebito.Responsable = Facturacion.Responsable;
                NotaDebito.Moneda = Facturacion.Moneda;
                NotaDebito.NroFactura = Facturacion.Numeracion;
                NotaDebito.IDFactura = Facturacion.ID;
                m_ObjectFlow = NotaDebito;
                m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByField("EntidadSF", "NombreClase", "NotaDebito");
                m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                m_ResultProcess = EnumResult.ERROR;
                SoftException.Control(ex);
            }
            base.Start();
        }
    }
}
