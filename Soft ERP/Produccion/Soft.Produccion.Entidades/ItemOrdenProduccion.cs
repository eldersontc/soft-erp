using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;
using Soft.Ventas.Entidades;

namespace Soft.Produccion.Entidades
{
    [Serializable]
    public class ItemOrdenProduccion : ItemDocumento
    {
        public ItemOrdenProduccion()
        {
            Servicios = new List<ItemOrdenProduccionServicio>();
        }

        public virtual IList<ItemOrdenProduccionServicio> Servicios { get; set; }
        public virtual Int32 ImpresoTiraColor { get; set; }
        public virtual Int32 ImpresoRetiraColor { get; set; }
        public virtual String Observacion { get; set; }
        public virtual Existencia Operacion { get; set; }


        private Maquina mMaquina;
        public virtual Maquina Maquina
        {
            get
            {
                return mMaquina;
            }
            set
            {
                mMaquina = value;
                if (NewInstance) { return; }

                if (mMaquina == null)
                {
                    this.FormatoImpresionAlto = 0;
                    this.FormatoImpresionLargo = 0;
                }
                else
                {
                    this.FormatoImpresionAlto = this.mMaquina.PliegoAltoMaximo;
                    this.FormatoImpresionLargo = this.mMaquina.PliegoAnchoMaximo;
                }
            }
        }

        public virtual Existencia Material { get; set; }

        public virtual String TipoUnidad { get; set; }

        private Decimal mMedidaAbiertaLargo;
        public virtual Decimal MedidaAbiertaLargo
        {
            get
            {
                return mMedidaAbiertaLargo;
            }
            set
            {
                mMedidaAbiertaLargo = value;
            }
        }

        private Decimal mMedidaAbiertaAlto;
        public virtual Decimal MedidaAbiertaAlto
        {
            get
            {
                return mMedidaAbiertaAlto;
            }
            set
            {
                mMedidaAbiertaAlto = value;
            }
        }

        public virtual Decimal MedidaCerradaLargo { get; set; }
        public virtual Decimal MedidaCerradaAlto { get; set; }
        public virtual Decimal Costo { get; set; }
        public virtual Decimal CostoMaquina { get; set; }
        public virtual Decimal CostoMaterial { get; set; }
        public virtual Decimal CostoTransporte { get; set; }
        public virtual Boolean TieneMedidaAbierta { get; set; }
        public virtual Boolean TieneMedidaCerrada { get; set; }
        public virtual Boolean TieneTiraRetira { get; set; }
        public virtual Int32 SeparacionX { get; set; }
        public virtual Int32 SeparacionY { get; set; }

        //Nuevos
        public virtual Decimal FormatoImpresionLargo { get; set; }
        public virtual Decimal FormatoImpresionAlto { get; set; }
        public virtual Boolean GraficoPrecorteGirado { get; set; }
        public virtual Boolean GraficoImpresionGirado { get; set; }
        public virtual Int32 NroPiezasPrecorte { get; set; }
        public virtual Int32 NroPiezasImpresion { get; set; }

        public virtual String MetodoImpresion { get; set; }




        public virtual Boolean TieneGraficos { get; set; }
        public virtual Boolean TieneMaterial { get; set; }
        public virtual Boolean TieneMaquina { get; set; }

        public virtual Int32 NumerodePases { get; set; }
        public virtual Decimal CantidadMaterial { get; set; }
        public virtual Decimal CantidadProduccion { get; set; }
        public virtual Decimal CantidadDemasia { get; set; }

        public virtual Decimal CantidadElemento { get; set; }

        public virtual Decimal CostoServicio { get; set; }

        public virtual Decimal CantidadUnidad { get; set; }

        public virtual Decimal CantidadDemasiaMaterial { get; set; }

        public virtual Int32 NumeroPliegos { get; set; }

        public virtual Boolean GraficoImpresionManual { get; set; }

        public virtual Boolean TieneTipoUnidad { get; set; }
        public virtual String UnidadMedidaAbierta { get; set; }

        public virtual String LabelMaterial { get; set; }
        public virtual String LabelMaterialAlmancen { get; set; }
        public virtual String LabelProduccion { get; set; }


        public virtual ItemOrdenProduccionServicio AddServicio()
        {
            ItemOrdenProduccionServicio Item = new ItemOrdenProduccionServicio();
            Servicios.Add(Item);
            return Item;
        }






        public virtual void CalcularProduccionItem()
        {
            try
            {


                if (this.Operacion.Codigo.Equals("IMPRVINIL") || this.Operacion.Nombre.Equals("IMPRESION BANNER"))
                {
                    Decimal largo = this.MedidaAbiertaLargo;
                    Decimal alto = this.MedidaAbiertaAlto;

                    if (this.UnidadMedidaAbierta.Equals("CM."))
                    {
                        largo = largo / 100;
                        alto = alto / 100;
                    }
                    this.CantidadMaterial = Math.Round((this.CantidadElemento * (largo * alto)), 0);
                    this.CantidadDemasiaMaterial = this.CantidadDemasia;
                    this.CantidadProduccion = this.CantidadMaterial + this.CantidadDemasiaMaterial;
                    //itemcosteado.CantidadMaterial += itemcosteado.CantidadDemasia;
                }
                else
                {

                    Decimal mat = (this.CantidadElemento / (this.NroPiezasPrecorte * this.NroPiezasImpresion));
                    Int32 mate = Convert.ToInt32(mat);

                    if ((mat - mate) > 0)
                    {
                        this.CantidadMaterial = mate + 1;
                    }
                    else
                    {
                        this.CantidadMaterial = mate;
                    }

                    //itemcosteado.CantidadMaterial += itemcosteado.CantidadDemasia;
                    try
                    {
                        this.CantidadDemasiaMaterial = this.CantidadDemasia / this.NroPiezasPrecorte;
                    }
                    catch (Exception)
                    {
                    }


                    Int32 pases = 1;



                    if (this.MetodoImpresion.Equals("TIRA Y RETIRA"))
                    {
                        pases = 2;
                    }
                    else if (this.MetodoImpresion.Equals("CONTRAPINZA"))
                    {
                        pases = 2;
                    }




                    if (this.CantidadUnidad == 0)
                    {
                        this.NumeroPliegos = 1;
                    }
                    else
                    {
                        Decimal pliegos = this.CantidadUnidad / (this.NroPiezasImpresion * 2);
                        Decimal entero = Math.Truncate(pliegos);
                        Decimal paginasresiduo = entero - pliegos;

                        this.NumeroPliegos = Convert.ToInt32(entero);

                        if (this.NumeroPliegos == 0)
                        {
                            this.NumeroPliegos = 1;
                        }
                    }

                    this.CantidadProduccion = (this.CantidadMaterial + this.CantidadDemasiaMaterial) * this.NumerodePases * this.NroPiezasPrecorte * pases;


                }
            }
            catch (Exception)
            {
                //SoftException.ShowException(ex);
            }

        }







    }
}
