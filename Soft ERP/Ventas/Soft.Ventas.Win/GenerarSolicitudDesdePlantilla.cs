using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Ventas.Entidades;

namespace Soft.Ventas.Win
{
    public class GenerarSolicitudDesdePlantilla : ControllerApp 
    {
        public override void  Start()
        {
            try 
	        {
                Plantilla Plantilla = (Plantilla)base.m_ObjectFlow;
                SolicitudCotizacion Solicitud = new SolicitudCotizacion();
                Solicitud.Cantidad = 1;
                Solicitud.Descripcion = Plantilla.Nombre;
                Solicitud.TipoDocumento = (TipoSolicitudCotizacion)HelperNHibernate.GetEntityByID("TipoSolicitudCotizacion", "B8AA5B25-9180-44A6-B750-F96D1EA17147");
                Solicitud.Observacion = String.Format("Generado desde la Plantilla - {0}", Plantilla.Codigo);
                foreach (ItemPlantilla Item in Plantilla.Items)
                {
                    ItemSolicitudCotizacion ItemSolicitud = new ItemSolicitudCotizacion();
                    ItemSolicitud.Existencia = Item.Existencia;
                    ItemSolicitud.Unidad = Item.Unidad;
                    ItemSolicitud.Cantidad = Item.Cantidad;
                }
                base.m_ObjectFlow = Solicitud;
                base.m_ResultProcess = EnumResult.SUCESS;
	        }
	        catch (Exception)
	        {
                base.m_ResultProcess = EnumResult.ERROR;
	        }
 	        base.Start();
        }
    }
}
