.net core 使用web service
 ` https://lebang2020.cn/details/210110njneqn2f.html `

引入dapper

获取连接字符串DapperFactory,打开连接

```c#
    public class DapperFactory
    {
        public static readonly string connectionString =
            ConfigurationUtil.Configuration["ConnectionStrings:DefaultConnection"].ToString();
             

        public static OracleConnection CrateOracleConnection()
        {
            
           var connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
```



基类操作类

```c#
 public class BaseRepository<Tkey, T> : IBaseRepository<Tkey, T> where T : BaseEntity
    {

        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="listModel"></param>
        public virtual int InsertMany(List<T> listModel)
        {
            if (listModel == null || listModel.Count <= 0)
            {
                throw new Exception("插入数据不可为空");
            }
            T model = listModel.FirstOrDefault();
            var ps = model.GetType().GetProperties();
            List<string> @colms = new List<string>();
            List<string> @params = new List<string>();

            foreach (var p in ps)
            {
                @colms.Add(string.Format("{0}", p.Name));
                @params.Add(string.Format(":{0}", p.Name));
            }
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", typeof(T).Name, string.Join(", ", @colms), string.Join(", ", @params));

            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Execute(sql, listModel, null, null, null);
            }

        }


        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="listModel"></param>
        public virtual int InsertManyWithTrans(List<T> listModel, IDbConnection _conn, IDbTransaction trans)
        {
            if (listModel == null || listModel.Count <= 0)
            {
                throw new Exception("插入数据不可为空");
            }
            T model = listModel.FirstOrDefault();
            var ps = model.GetType().GetProperties();
            List<string> @colms = new List<string>();
            List<string> @params = new List<string>();

            foreach (var p in ps)
            {
                @colms.Add(string.Format("{0}", p.Name));
                @params.Add(string.Format(":{0}", p.Name));
            }
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", typeof(T).Name, string.Join(", ", @colms), string.Join(", ", @params));

            return _conn.Execute(sql, listModel, trans, null, null);

        }

        /// <summary>
        /// 插入一个
        /// </summary>
        /// <param name="model"></param>
        public virtual int InsertOne(T model)
        {

            if (model == null)
            {
                throw new Exception("插入数据不可为空");
            }
            var ps = model.GetType().GetProperties();
            List<string> @colms = new List<string>();
            List<string> @params = new List<string>();

            foreach (var p in ps)
            {
                @colms.Add(string.Format("{0}", p.Name));
                @params.Add(string.Format(":{0}", p.Name));
            }
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", typeof(T).Name, string.Join(", ", @colms), string.Join(", ", @params));
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Execute(sql, model, null, null, null);
            }
        }

        /// <summary>
        /// 插入一个
        /// </summary>
        /// <param name="model"></param>
        public virtual int InsertOneWithTrans(T model, IDbConnection _conn, IDbTransaction trans)
        {

            if (model == null)
            {
                throw new Exception("插入数据不可为空");
            }
            var ps = model.GetType().GetProperties();
            List<string> @colms = new List<string>();
            List<string> @params = new List<string>();

            foreach (var p in ps)
            {
                @colms.Add(string.Format("{0}", p.Name));
                @params.Add(string.Format(":{0}", p.Name));
            }
            var sql = string.Format("INSERT INTO {0} ({1}) VALUES({2})", typeof(T).Name, string.Join(", ", @colms), string.Join(", ", @params));

            return _conn.Execute(sql, model, trans, null, null);
        }
        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name = "whereProperties" ></ param >
        /// < returns ></ returns >
        public virtual T GetOne(object whereProperties)
        {

            string where = "";
            var listPropert = whereProperties.GetType().GetProperties();
            if (listPropert.Length > 0)
            {
                where += " where ";
                listPropert.ToList().ForEach(e =>
                {
                    where += $" {e.Name} = :{e.Name} and";
                });
            }
            where = where.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            //返回单条信息
            string query = $"SELECT * FROM { typeof(T).Name}{where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.QuerySingleOrDefault<T>(query, whereProperties);
            }
        }

        public virtual T GetById(Tkey id)
        {
            string keyName = GetTkey();
            if (string.IsNullOrEmpty(keyName))
            {
                return null;
            }
            var obj = new { Id = id };

            //返回单条信息
            string query = $"SELECT * FROM { typeof(T).Name} where {keyName} = :Id";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.QuerySingleOrDefault<T>(query, obj);
            }

        }



        /// <summary>
        /// 获取对象主键
        /// </summary>
        /// <returns></returns>
        public string GetTkey()
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var customAttrs = property.GetCustomAttributes();
                if (customAttrs != null && customAttrs.Any())
                {
                    foreach (var customAttr in customAttrs)
                    {
                        if (customAttr.GetType().Name == "KeyAttribute")
                        {
                            return property.Name;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 查询一个
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual T GetOne(string where)
        {
            if (!string.IsNullOrEmpty(where))
            {
                where = $" where 1=1 and {where}";
            }
            //返回单条信息
            string query = $"SELECT * FROM { typeof(T).Name}  {where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.QuerySingleOrDefault<T>(query);
            }
        }

        /// <summary>
        /// 查询多个
        /// </summary>
        /// <param name="whereProperties"></param>
        /// <returns></returns>
        public virtual List<T> GetMany(object whereProperties)
        {
            string where = "";
            var listPropert = whereProperties.GetType().GetProperties();
            if (listPropert.Length > 0)
            {
                where += " where ";
                listPropert.ToList().ForEach(e =>
                {
                    where += $" {e.Name} = :{e.Name} and";
                });
            }
            where = where.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            string query = $"SELECT * FROM { typeof(T).Name}{where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Query<T>(query, whereProperties)?.ToList();
            }
        }

        /// <summary>
        /// 查询多个
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual List<T> GetMany(string where)
        {
            if (!string.IsNullOrEmpty(where))
            {
                where = $" where 1=1 and {where}";
            }
            string query = $"SELECT * FROM { typeof(T).Name}  {where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Query<T>(query)?.ToList();
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="whereProperties"></param>
        /// <returns></returns>
        public virtual bool Exists(object whereProperties)
        {
            return GetMany(whereProperties).Any();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual bool Exists(string where)
        {
            return GetMany(where).Any();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int DeleteById(T entity)
        {
            if (entity == null)
            {
                throw new Exception("删除内容不可为空");
            }
            string where = "";
            var listPropert = entity.GetType().GetProperties();
            if (listPropert.Length > 0)
            {
                listPropert.ToList().ForEach(p =>
                {
                    var primaryKey = p.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute));
                    if (primaryKey != null)
                    {
                        where += $" {p.Name} = :{p.Name} and";
                    }
                });
            }

            where = where.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            if (string.IsNullOrEmpty(where))
            {
                throw new Exception("未找到Id");
            }
            string query = $"DELETE FROM { typeof(T).Name} where {where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Execute(query, entity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int DeleteByWithTrans(T entity,IDbConnection _conn, IDbTransaction trans)
        {
            if (entity == null)
            {
                throw new Exception("删除内容不可为空");
            }
            string where = "";
            var listPropert = entity.GetType().GetProperties();
            if (listPropert.Length > 0)
            {
                listPropert.ToList().ForEach(p =>
                {
                    var primaryKey = p.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute));
                    if (primaryKey != null)
                    {
                        where += $" {p.Name} = :{p.Name} and";
                    }
                });
            }

            where = where.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            if (string.IsNullOrEmpty(where))
            {
                throw new Exception("未找到Id");
            }
            string query = $"DELETE FROM { typeof(T).Name} where {where}";
            
            return _conn.Execute(query, entity, trans);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="whereProperties"></param>
        /// <returns></returns>
        public virtual int Delete(object whereProperties)
        {
            string where = "";
            var listPropert = whereProperties.GetType().GetProperties();
            if (listPropert.Length > 0)
            {
                listPropert.ToList().ForEach(e =>
                {
                    where += $"{e.Name} = :{e.Name} and";
                });
            }
            where = where.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            if (string.IsNullOrEmpty(where))
            {
                throw new Exception("条件不可为空");
            }
            string query = $"DELETE FROM { typeof(T).Name} where {where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Execute(query, whereProperties);
            }
        }

        /// <summary>
        /// 根据Id更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int UpdateById(T entity)
        {
            if (entity == null)
            {
                throw new Exception("更新内容不可为空");
            }
            string where = "";
            var listPropert = entity.GetType().GetProperties();
            if (listPropert.Length > 0)
            {
                listPropert.ToList().ForEach(p =>
                {
                    var primaryKey = p.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute));
                    if (primaryKey != null)
                    {
                        where += $" {p.Name} = :{p.Name} and";
                    }
                });
            }

            where = where.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            if (string.IsNullOrEmpty(where))
            {
                throw new Exception("未找到Id");
            }

            string update = "";
            var listPropertUpdate = entity.GetType().GetProperties();
            if (listPropertUpdate.Length > 0)
            {
                update += "";
                listPropertUpdate.ToList().ForEach(e =>
                {
                    if (e.CustomAttributes.All(x => x.AttributeType != typeof(KeyAttribute)))
                    {
                        update += $"{e.Name} = :{e.Name} ,";
                    }
                });
            }
            update = update.TrimEnd(',');
            if (string.IsNullOrEmpty(update))
            {
                throw new Exception("无更新内容");
            }
            string query = $"update { typeof(T).Name} set {update} where {where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Execute(query, entity);
            }

        }


        /// <summary>
        /// 根据Id更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int UpdateWithTrans(T entity, IDbConnection _conn, IDbTransaction trans)
        {
            if (entity == null)
            {
                throw new Exception("更新内容不可为空");
            }
            string where = "";
            var listPropert = entity.GetType().GetProperties();
            if (listPropert.Length > 0)
            {
                listPropert.ToList().ForEach(p =>
                {
                    var primaryKey = p.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(KeyAttribute));
                    if (primaryKey != null)
                    {
                        where += $" {p.Name} = :{p.Name} and";
                    }
                });
            }

            where = where.TrimEnd('d').TrimEnd('n').TrimEnd('a');
            if (string.IsNullOrEmpty(where))
            {
                throw new Exception("未找到Id");
            }

            string update = "";
            var listPropertUpdate = entity.GetType().GetProperties();
            if (listPropertUpdate.Length > 0)
            {
                update += "";
                listPropertUpdate.ToList().ForEach(e =>
                {
                    if (e.CustomAttributes.All(x => x.AttributeType != typeof(KeyAttribute)))
                    {
                        update += $"{e.Name} = :{e.Name} ,";
                    }
                });
            }
            update = update.TrimEnd(',');
            if (string.IsNullOrEmpty(update))
            {
                throw new Exception("无更新内容");
            }
            string query = $"update { typeof(T).Name} set {update} where {where}";

            return _conn.Execute(query, entity,trans);

        }

        /// <summary>
        /// 根据条件更新
        /// </summary>
        /// <param name="updateProperty"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual int Update(object updateProperty, string where)
        {
            if (string.IsNullOrEmpty(where))
            {
                throw new Exception("需输入条件");
            }
            string update = "";
            var listPropertUpdate = updateProperty.GetType().GetProperties();
            if (listPropertUpdate.Length > 0)
            {
                update += "";
                listPropertUpdate.ToList().ForEach(e =>
                {
                    update += $"{e.Name} = :{e.Name} ,";
                });
            }
            update = update.TrimEnd(',');
            if (string.IsNullOrEmpty(update))
            {
                throw new Exception("无更新内容");
            }
            string query = $"update { typeof(T).Name} set {update} where {where}";
            using (var _conn = DapperFactory.CrateOracleConnection())
            {
                return _conn.Execute(query, updateProperty);
            }

        }
    }
```

