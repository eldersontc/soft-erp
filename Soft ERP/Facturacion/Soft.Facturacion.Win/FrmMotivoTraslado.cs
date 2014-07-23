using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Facturacion.Entidades;

namespace Soft.Facturacion.Win
{
    public partial class FrmMotivoTraslado : FrmParent
    {
        public FrmMotivoTraslado()
        {
            InitializeComponent();
        }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }
        public MotivoTraslado MotivoTraslado { get { return (MotivoTraslado)base.m_ObjectFlow; } }


        private void Mostrar() { 
            
        
        }





    }
}
