using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Soft.Entities;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Soft.Configuracion.Entidades;
using Soft.Exceptions;
using System.Drawing;
using NHibernate.Criterion;
using System.Collections;

namespace Soft.DataAccess
{
    public class ControllerApp : Form
    {
        public Object m_ObjectFlow;
        public EnumResult m_ResultProcess;
        public String m_Parameter;
        public List<String> m_ItemsSelecteds;
        public Accion m_AccionActual;
        public ItemAccion m_ItemAccionAcual;
        public EntidadSF m_EntidadSF;
        public ItemContenedor m_ItemContenedor;
        public Boolean m_Modal;
        public ISession m_Sesion;
        public ITransaction m_Transaccion;

        protected static ISessionFactory m_SessionFactory;

        public ControllerApp() { }

        public ControllerApp(ISession sesion) 
        {
            this.m_Sesion = sesion;
        }

        public static void LoadConfiguration()
        {
            try
            {
                m_SessionFactory = new Configuration().Configure("Soft.Win.exe.config").BuildSessionFactory();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, SystemIcons.Warning.ToBitmap());
                Application.Exit();
            }
        }

        public void AbrirSesion() 
        {
            if (m_Sesion == null)
                m_Sesion = m_SessionFactory.OpenSession();
        }

        public void IniciarTransaccion()
        {
            if (m_Sesion == null)
                m_Sesion = m_SessionFactory.OpenSession();
            m_Transaccion = m_Sesion.BeginTransaction();
        }

        public void FinalizarTransaccion(bool cancelarTransaccion = false)
        {
            if (m_Transaccion != null)
                if (!cancelarTransaccion)
                    m_Transaccion.Commit();
                else
                    m_Transaccion.Rollback();
        }

        public void CerrarSesion()
        {
            if (m_Sesion != null && m_Sesion.IsOpen) m_Sesion.Close();
        }

        public void Agregar(object objeto)
        {
            m_Sesion.Save(objeto);
        }

        public void Modificar(object objeto)
        {
            m_Sesion.Update(objeto);
        }

        public void Eliminar(object objeto)
        {
            m_Sesion.Delete(objeto);
        }

        /// <summary>
        /// Obtiene una lista de objetos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static IList<T> GetObjects<T>(IList<object[]> filters)
        {
            IList<T> list = new List<T>();
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                ICriteria criteria = Sesion.CreateCriteria(typeof(T));
                foreach (var filter in filters)
                {
                    switch (filter[0].ToString())
                    {
                        case TypeEnum.CEnumCondition.LIKE:
                            criteria.Add(Restrictions.Eq(filter[1].ToString(), filter[2].ToString()));
                            break;
                        case TypeEnum.CEnumCondition.IN:
                            criteria.Add(Restrictions.In(filter[1].ToString(), (ICollection)filter[2]));
                            break;
                        case TypeEnum.CEnumCondition.NOT_IN:
                            criteria.Add(Restrictions.Not(Restrictions.In(filter[1].ToString(), (ICollection)filter[2])));
                            break;
                        default:
                            break;
                    }
                }
                list = criteria.List<T>();
            }
            return list;
        }

        public static List<object> GetNObjects(string sql)
        { 
            List<object> list = new List<object>();
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                IDbCommand dbCommand = Sesion.Connection.CreateCommand();
                Sesion.Transaction.Enlist(dbCommand);
                dbCommand.CommandText = sql;
                IDataReader lector = dbCommand.ExecuteReader();
                CrearListaObjeto(lector, ref list);
                lector.Close();
            }
            return list;
        }

        private static void CrearListaObjeto(IDataReader lector, ref List<object> lista)
        {
            object[] propiedades = new object[lector.FieldCount];

            for (int i = 0; i < lector.FieldCount; i++)
                propiedades[i] = new { nombrePropiedad = lector.GetName(i), tipoPropiedad = lector.GetFieldType(i) };

            //object crearObjeto = new object();

            while (lector.Read())
            {
                //object objeto = crearObjeto.CrearNuevoObjeto(propiedades);
                object[] objeto = new object[lector.FieldCount];
                for (int i = 0; i < lector.FieldCount; i++)
                    objeto[i] = lector[i] is DBNull ? null : lector[i];
                    //objeto.GetType().GetProperty(propiedades[i].nombrePropiedad).SetValue(objeto, lector[i] is DBNull ? null : lector[i]);
                lista.Add(objeto);
            }
        }

        public virtual void Start()
        {
            // All code here.
            if (m_ItemAccionAcual == null) { return; }
            if (m_ResultProcess == EnumResult.SUCESS)
            {
                this.ContinuarFlujo(m_ItemAccionAcual.Exito);
            }
            else if (m_ResultProcess == EnumResult.ERROR)
            {
                this.ContinuarFlujo(m_ItemAccionAcual.Error);
            }
            else
            {
                return;
            }
        }

        public void ContinuarFlujo(String Siguiente)
        {
            if (Siguiente.Equals("Salir")) { return; }
            ItemAccion m_Item = m_AccionActual.ItemByName(Siguiente.Trim());
            ControllerApp Controlador = (ControllerApp)Factory.InstanceObject(m_Item.Ensamblado.Ensamblado_, m_Item.Clase);
            Controlador.m_Parameter = m_Item.Parametro;
            Controlador.m_AccionActual = m_AccionActual;
            Controlador.m_ItemAccionAcual = m_Item;
            Controlador.m_ObjectFlow = m_ObjectFlow;
            Controlador.m_EntidadSF = m_EntidadSF;
            Controlador.m_ItemsSelecteds = m_ItemsSelecteds;
            Controlador.m_ItemContenedor = m_ItemContenedor;
            Controlador.Start();
        }

        public enum EnumResult
        {
            DEFAULT,
            SUCESS,
            ERROR,
            CANCEL
        }
    }
}
