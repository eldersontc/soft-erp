using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class Panel: Parent
    {
        #region "Cosntructores"

        public Panel() { Columnas = new List<ColumnaPanel>();}

        #endregion

        #region "Propiedades"

        public virtual String NombreVista { get; set; }
        public virtual EntidadSF EntidadSF { get; set; }
        public virtual IList<ColumnaPanel> Columnas { get; set; }

        #endregion

    }
}
