using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class Presupuesto : Documento  
    {
        public Presupuesto() { FechaCreacion = DateTime.Now; }
        public virtual SocioNegocio Cliente { get; set; }

        public virtual ItemPresupuesto AddItem(Cotizacion Cotizacion)
        {
            ItemPresupuesto Item = new ItemPresupuesto();
            Item.IDCotizacion = Cotizacion.ID;
            Items.Add(Item);
            return Item;
        }

        public virtual String ObtenerFiltroCotizaciones() {
            String Filtro = "";
            foreach (ItemPresupuesto Item in Items)
            {
                if (Filtro.Length > 0) { Filtro += ","; }
                Filtro += String.Format("'{0}'", Item.IDCotizacion);
            }
            return Filtro;
        }

        public virtual void GenerarNumCp()
        {
            String Result = "";
            if (NewInstance)
            {
                Result = TipoDocumento.GenerarNumerodeDocumento();
                Numeracion = Result;
            }
            if (!Result.Equals(""))
            {
                String SQL = "UPDATE TipoPresupuesto SET NumeracionActual = " + (TipoDocumento.NumeracionActual + 1) + " WHERE ID ='" + TipoDocumento.ID + "'";
                HelperNHibernate.GetDataSet(SQL);
            }
        }

    }
}
