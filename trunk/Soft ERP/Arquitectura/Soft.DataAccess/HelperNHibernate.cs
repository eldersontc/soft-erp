using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using NHibernate;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using NHibernate.Criterion;
using Soft.DataAccess;
using System.Data.OleDb;

namespace Soft.DataAccess
{
    public class HelperNHibernate: ControllerApp 
    {

        public static Parent GetEntityByID(String Class,String ID) {
            try
            {
                using (ISession Sesion = m_SessionFactory.OpenSession())
                {
                    Parent Entity = (Parent)Sesion.Get(Class, ID);
                    return Entity;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IList GetEntitiesByField(String Class, String Field, String Value)
        {
            try
            {
                using (ISession Sesion = m_SessionFactory.OpenSession())
                {
                    IList List = (IList)Sesion.CreateCriteria(Class).Add(Restrictions.Eq(Field,Value)).List();
                    return List;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static XmlDocument ExecuteView(String View, String Filter)
        {
            IDbCommand Command;
            XmlDocument XMLResult = new XmlDocument();
            String StrmResult = String.Empty;
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Command = Sesion.Connection.CreateCommand();
                        Trans.Enlist(Command);
                        if (Filter.Length > 0) { Filter = String.Format("WHERE {0}", Filter); }
                        Command.CommandText = String.Format("SELECT * FROM {0} {1} FOR XML AUTO", View, Filter);
                        Command.CommandType = System.Data.CommandType.Text;
                        using (IDataReader StrmReader = Command.ExecuteReader())
                        {
                            while (StrmReader.Read()) { StrmResult += StrmReader[0].ToString(); }
                            if (StrmResult.Length > 0) { XMLResult.LoadXml(String.Format("<ROOT>{0}</ROOT>", StrmResult)); } 
                        }
                        return XMLResult;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public static XmlDocument ExecuteSQL(String Query, String Filter)
        {
            IDbCommand Command;
            XmlDocument XMLResult = new XmlDocument();
            String StrmResult = String.Empty;
            try
            {
                using (ISession Sesion = m_SessionFactory.OpenSession())
                {
                    using (ITransaction Trans = Sesion.BeginTransaction())
                    {
                        Command = Sesion.Connection.CreateCommand();
                        Trans.Enlist(Command);
                        if (Filter.Length > 0) { Filter = String.Format("WHERE {0}", Filter); }
                        Command.CommandText = String.Format("{0} {1} FOR XML AUTO", Query, Filter);
                        Command.CommandType = System.Data.CommandType.Text;
                        using (IDataReader StrmReader = Command.ExecuteReader())
                        {
                            while (StrmReader.Read()) { StrmResult += StrmReader[0].ToString(); }
                            if (StrmResult.Length > 0) { XMLResult.LoadXml(String.Format("<ROOT>{0}</ROOT>", StrmResult)); } 
                        }
                        return XMLResult;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet GetDataSet(String Query)
        {
            DataSet DataSet = new DataSet();
            IDbCommand Command;
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                try
                {
                    Command = Sesion.Connection.CreateCommand();
                    Command.CommandText = Query;
                    Command.CommandType = System.Data.CommandType.Text;
                    IDataAdapter DataAdapter = new SqlDataAdapter((SqlCommand)Command);
                    DataAdapter.Fill(DataSet);
                    return DataSet;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static DataSet GetDataSetOLEDB(String Query)
        {
            DataSet DataSet = new DataSet();
            OleDbConnection Connection = null;
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                try
                {
                    Connection = new OleDbConnection(String.Format("Provider=SQLOLEDB;{0}", Sesion.Connection.ConnectionString));
                    OleDbDataAdapter DataAdapter = new OleDbDataAdapter(Query, Connection);
                    DataAdapter.Fill(DataSet);
                    //DataSet.WriteXmlSchema("G:\\Proyectos SVN\\Soft ERP\\Reportes\\XSD Definitions\\EntradaInventario.xsd");
                    return DataSet;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static String GenerateID()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        public static Parent Copy(Parent Entity)
        {
            Parent Copy = null;
            using (ISession Session = m_SessionFactory.OpenSession())
            {
                EvictProperties(Entity);
                Session.Evict(Entity);
                Entity.ID = HelperNHibernate.GenerateID();
                Copy = (Parent)Session.Merge(Entity);
            }
            return Copy;
        }

        public static void EvictProperties(Parent Entity)
        {
            using (ISession Session = m_SessionFactory.OpenSession())
            {
                Type EntityType = Entity.GetType();
                PropertyInfo[] properties = EntityType.GetProperties();
                foreach (PropertyInfo Property in properties)
                {
                    if (Property.PropertyType.Name.Equals("IList`1"))
                    {
                        IList List = (IList)Property.GetValue(Entity, null);
                        foreach (Parent Item in List)
                        {
                            EvictProperties(Item);
                            Session.Evict(Item);
                            ((Parent)Item).ID = HelperNHibernate.GenerateID();
                        }
                    }
                }
            }
        }

    }
}
