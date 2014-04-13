using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    public class DocumentoGenerico : Parent
    {
        public DocumentoGenerico() { }

        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual String Numeracion { get; set; }
        public virtual String Observacion { get; set; }
        public virtual DateTime FechaCreacion { get; set; }

        public virtual void GenerarNumeracion()
        {
            if (NewInstance)
            {
                Numeracion = TipoDocumento.GenerarNumerodeDocumento();
            }
        }
    }
}
