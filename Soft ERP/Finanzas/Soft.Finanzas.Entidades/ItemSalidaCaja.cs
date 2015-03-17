using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Finanzas.Entidades
{
    public class ItemSalidaCaja : Parent
    {
        public ItemSalidaCaja() { }

        public virtual string Codigo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Observacion{ get; set; }
        public virtual decimal Cantidad { get; set; }
        public virtual decimal Precio { get; set; }
        public virtual bool EsTipoTransporte { get; set; }
        public virtual Departamento DepartamentoOrigen { get; set; }
        public virtual Provincia ProvinciaOrigen { get; set; }
        public virtual Distrito DistritoOrigen { get; set; }
        public virtual Departamento DepartamentoDestino { get; set; }
        public virtual Provincia ProvinciaDestino { get; set; }
        public virtual Distrito DistritoDestino { get; set; }
        public virtual string Direccion { get; set; }
        public virtual string TipoVehiculo { get; set; }
        public virtual string IDOrdenProduccion { get; set; }
        public virtual string NumeracionOrdenProduccion { get; set; }
        public virtual string IDConsolidadoOrdenProduccion { get; set; }
        public virtual string NumeracionConsolidadoOrdenProduccion { get; set; }

        private decimal Total_;
        public virtual decimal Total { 
            get {
                Total_ = Cantidad * Precio;
                return Total_; 
            } 
            set { Total_ = value; } 
        }
    }
}
