using Dapper;
using DynamicObj.Share;
using DynamicObj.Share.Module;
using LightInject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DynamicObj.DAL
{
    public interface IBaseObjRepository
    {
        Obj InsertBaseObj(Obj baseObj);
        Obj GetObj(int id);

        Obj GetObjTT(int id);

    }

    public class BaseObjRepository
    {
        private ConnectionFactory _connectionFactory;
        public BaseObjRepository() { }

        public BaseObjRepository([Inject]ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Obj InsertBaseObj(Obj baseObj)
        {
            string commandOfInsertObj = "insert into Obj(Name) values(@Name);" +
                                "SELECT CAST(SCOPE_IDENTITY() as int)";//得到刚插入的自增Id
            string commandOfInsertObjProperty = "insert into ObjProperty(PropertyKey, PropertyValue, ObjId) values(@PropertyKey, @PropertyValue, @ObjId)";

            using (IDbConnection conn = _connectionFactory.getDBConnection())
            {
                conn.Open();
                IDbTransaction sqlTransaction = conn.BeginTransaction();
                try
                {
                    var id = conn.Query<int>(commandOfInsertObj, baseObj, sqlTransaction).FirstOrDefault();

                    baseObj.Id = id;

                    var objPropertyToInset = baseObj.GetObjProperties().Select(prop => new { PropertyKey = prop.PropertyKey, PropertyValue = prop.PropertyValue, ObjId = id });

                    conn.Execute(commandOfInsertObjProperty, objPropertyToInset, sqlTransaction);


                    sqlTransaction.Commit();

                    return baseObj;
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public List<Obj> GetObjs(List<int> ids)
        {
            //_db.Query<Users>("SELECT * FROM dbo.Users s WHERE s.id IN @ids ", new { ids = IDs.ToArray() }).ToList();
            string commandOfGetObjById = "select * from Obj left join ObjProperty on Obj.Id = ObjProperty.ObjId where Obj.Id IN @ids ";

            Dictionary<int, Obj> dic = new Dictionary<int, Obj>();

            using (IDbConnection conn = _connectionFactory.getDBConnection())
            {
                try
                {
                    var data = conn.Query<Obj, ObjProperty, Obj>(commandOfGetObjById, (obj, properties) =>
                    {
                        Obj temResult;

                        if (dic.TryGetValue(obj.Id, out temResult))
                        {
                            temResult.AddObjProperty(properties);
                        }

                        else
                        {
                            obj.AddObjProperty(properties);
                            dic.Add(obj.Id, obj);
                        }
                        return obj;
                    }, new { Ids = ids.ToArray() });

                    return dic.Values.ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public virtual Obj GetObj(int id)
        {
            string commandOfGetObjById = "select * from Obj left join ObjProperty on Obj.Id = ObjProperty.ObjId where Obj.Id = @Id ";
            Obj result = null;

            Dictionary<int, Obj> dic = new Dictionary<int, Obj>();

            using (IDbConnection conn = _connectionFactory.getDBConnection())
            {
                try
                {
                    var data = conn.Query<Obj, ObjProperty, Obj>(commandOfGetObjById, (obj, properties) =>
                    {
                        Obj temResult;

                        if (dic.TryGetValue(obj.Id, out temResult))
                        {
                            temResult.AddObjProperty(properties);
                        }

                        else
                        {
                            obj.AddObjProperty(properties);
                            dic.Add(obj.Id, obj);
                        }
                        return obj;
                    }, new { Id = id });

                    return dic.Values.ToList().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;

        }

        public Obj GetObjTT(int id)
        {
            string commandOfGetObjById = "select Id, Name from Obj where Id = @Id ";
            string commandOfGetPropertyByBaseId = "select Id, PropertyKey, PropertyValue, ObjId from ObjProperty  where ObjId = @ObjId";
            Obj result = null;

            using (IDbConnection conn = _connectionFactory.getDBConnection())
            {
                try
                {
                    result = conn.Query<Obj>(commandOfGetObjById, new { Id = id }).FirstOrDefault();
                    if (result != null)
                    {
                        var properties = conn.Query<ObjProperty>(commandOfGetPropertyByBaseId, new { ObjId = result.Id });

                        result.SetObjProperties(properties);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;

        }
    }
}
