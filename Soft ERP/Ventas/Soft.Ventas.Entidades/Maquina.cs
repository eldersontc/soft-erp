using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class Maquina : Parent {
    
        public Maquina() { }
        public virtual String Codigo { get; set; }
        public virtual TipoMaquina TipoMaquina { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual SocioNegocio Proveedor { get; set; }
        public virtual Int32 CantidadCuerpos { get; set; }
        public virtual Decimal MaximoGramaje { get; set; }
        public virtual Decimal MinimoGramaje { get; set; }
        public virtual Int32 PliegoAnchoMimino { get; set; }
        public virtual Int32 PliegoAnchoMaximo { get; set; }
        public virtual Int32 PliegoAltoMinimo { get; set; }
        public virtual Int32 PliegoAltoMaximo { get; set; }
        public virtual Int32 MargenPinza { get; set; }
        public virtual Int32 MargenSalida { get; set; }
        public virtual Int32 MargenEscuadra { get; set; }
        public virtual Int32 MargenContraEscuadra { get; set; }
        public virtual Int32 MargenCalle { get; set; }
        public virtual Int32 ResolucionMinimo { get; set; }
        public virtual Int32 ResolucionMaximo { get; set; }
        public virtual String Descripcion { get; set; }
    }
}
