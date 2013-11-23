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

	        }
	        catch (Exception)
	        {
		        throw;
	        }
 	        base.Start();
        }
    }
}
