using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IRepositories
{
    public interface IBaseRepository<Tkey, T> where T : BaseEntity
    {
        public int InsertMany(List<T> listModel);
        public int InsertOne(T model);

        public T GetOne(object whereProperties);
        public T GetOne(string where);
        public T GetById(Tkey id);
        public List<T> GetMany(object whereProperties);
        public List<T> GetMany(string where);
        public bool Exists(object whereProperties);
        public bool Exists(string where);
        public int DeleteById(T entity);

        public int Delete(object whereProperties);
        public int UpdateById(T entity);
        public int Update(object updateProperty, string where);

        public int DeleteByWithTrans(T entity, IDbConnection _conn, IDbTransaction trans);
        public int UpdateWithTrans(T entity, IDbConnection _conn, IDbTransaction trans);
        public int InsertOneWithTrans(T model, IDbConnection _conn, IDbTransaction trans);
        public int InsertManyWithTrans(List<T> listModel, IDbConnection _conn, IDbTransaction trans);

    }
}
