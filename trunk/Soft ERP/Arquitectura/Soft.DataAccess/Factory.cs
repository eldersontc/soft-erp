using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using System.Reflection;
using System.Xml;
using System.Collections;
using NHibernate;
using System.Data;
using System.Data.SqlClient;
using Soft.Seguridad.Entidades;
using Soft.DataAccess;
using System.Drawing;

namespace Soft.DataAccess
{
    public class Factory : ControllerApp
    {

        public override void  Start()
        {
            try
            {
                this.CreateNewObject();             
                base.m_ResultProcess = EnumResult.SUCESS;
            }catch (Exception){
                base.m_ResultProcess = EnumResult.ERROR;
                throw;
            }
            base.Start();
        }

        public void CreateNewObject() {
            //String[] Parametros = Convert.ToString(this.m_Parameter).Split('|');
            //this.m_ObjectFlow = InstanceObject(Parametros[0], Parametros[1]);
            base.m_ObjectFlow = InstanceObject(base.m_EntidadSF.EnsambladoClase.Ensamblado_, base.m_EntidadSF.NombreClase);
        }

        public static Object InstanceObject(String Assembly, String Class)
        {
            Type InstanceObject;
            Assembly InstanceAssemby;
            InstanceAssemby = System.Reflection.Assembly.Load(Assembly);
            InstanceObject = InstanceAssemby.GetType(String.Format("{0}.{1}", Assembly,Class));
            Object ObjectSF = Activator.CreateInstance(InstanceObject);
            return ObjectSF;
        }

        public static string ToXML(Object Entity)
        {
            String XML = "";
            XML += String.Format("<{0} ", Entity.GetType().Name);

            PropertyInfo[] AllProperties = Entity.GetType().GetProperties();

            List<PropertyInfo> Atributes = new List<PropertyInfo>();
            List<PropertyInfo> References = new List<PropertyInfo>();
            List<PropertyInfo> Collections = new List<PropertyInfo>();

            foreach (PropertyInfo Property in AllProperties)
            {
                Object Value = Property.GetValue(Entity,null);
                if (Value is String || Value is Int32 || Value is Double || Value is Decimal || Value is DateTime || Value is Boolean) {
                    Atributes.Add(Property);
                }
                else if (Value is IList)
                {
                    Collections.Add(Property);
                }
                else if (Value is Parent)
                {
                    References.Add(Property);
                }
            }

            foreach (PropertyInfo Attr in Atributes)
            {
                XML += String.Format("{0}=\"{1}\" ", Attr.Name, Attr.GetValue(Entity, null));
            }

            foreach (PropertyInfo Ref in References)
            {
                Parent Reference = (Parent)Ref.GetValue(Entity, null);
                XML += String.Format("Ref-{0}-{1}=\"{2}\" ", Reference.GetType().Name, Ref.Name, (Reference != null) ? Reference.ID : "0");
            }

            if (Collections.Count == 0) { XML += String.Format("/>"); } else { XML += String.Format(">"); }

            foreach (PropertyInfo List in Collections)
            {
                XML += String.Format("<{0}>", List.Name);
                IList Lst = (IList)List.GetValue(Entity, null);
                foreach (var Item in Lst)
                {
                    XML += ToXML(Item);
                }
                XML += String.Format("</{0}>", List.Name);
            }

            if (Collections.Count > 0) { XML += String.Format("</{0}>", Entity.GetType().Name); }

            return XML;
        }

        public static Object ToObject(String StrXML,String Ensamblado)
        {
            XmlDocument XML = new XmlDocument();
            XML.LoadXml(StrXML);

            Object Objeto = null;
            Type Type = null;
            Assembly Assembly = null;
            Assembly = Assembly.Load(Ensamblado);
            Type = Assembly.GetType(String.Format("{0}.{1}",Ensamblado,XML.DocumentElement.Name));
            Objeto = Activator.CreateInstance(Type);

            foreach (XmlAttribute Attr in XML.DocumentElement.Attributes)
            {
                if (!Attr.Name.Contains("Ref-"))
                {
                    PropertyInfo Prop = Objeto.GetType().GetProperty(Attr.Name);
                    Prop.SetValue(Objeto, GetValue(Prop.PropertyType, Attr.Value), null);
                }
                else {
                    String Name = Attr.Name.Replace("Ref-","");
                    String[] Strings = Name.Split('-');
                    PropertyInfo Prop = Objeto.GetType().GetProperty(Strings[1]);
                    Prop.SetValue(Objeto, HelperNHibernate.GetEntityByID(Strings[0], Attr.Value), null);
                }
            }

            foreach (XmlNode Node in XML.DocumentElement.ChildNodes)
            {
                PropertyInfo Prop = Objeto.GetType().GetProperty(Node.Name);
                IList List = (IList)Prop.GetValue(Objeto, null);
                foreach (XmlNode Item in Node.ChildNodes)
                {
                    Object ItemObj = ToObject(Item.OuterXml,Ensamblado);
                    List.Add(ItemObj);
                }
            }

            return Objeto;
        }

        public static Object GetValue(Type tipo, String Valor)
        {
            switch (tipo.Name)
            {
                case "String": return Convert.ToString(Valor);
                case "Decimal":
                case "Double": return Convert.ToDecimal(Valor);
                case "Int32":
                case "Int64": return Convert.ToInt32(Valor);
                case "Boolean": return Convert.ToBoolean(Valor);
                case "DateTime": return Convert.ToDateTime(Valor);
                default: return null;
            }
        }

    }
}
