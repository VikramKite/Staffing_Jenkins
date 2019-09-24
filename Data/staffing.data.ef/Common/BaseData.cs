using staffing.data.models.Common.Post;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace staffing.data.ef.Common
{
    public abstract class BaseData
    {
        protected abstract string TableName();
        protected string UpdateWhereClause;
        protected bool UpdateLastDateTime;

        protected BaseData()
        {
            UpdateLastDateTime = true;
        }

        protected async Task<bool> ExecUpdate(PatchPostModel data, int userId, DateTime currentDt)
        {
            if (string.IsNullOrEmpty(UpdateWhereClause))
            {
                throw new NotImplementedException();
            }

            if (data == null || data.changed == null)
            {
                return true;
            }

            string upd = $"UPDATE {TableName()} SET";

            int cnt = data.changed.Count;
            if (cnt == 0)
            {
                return true;
            }

            List<object> sqlparams = new List<object>();
            ICollection<string> keys = data.changed.Keys;
            foreach (string key in keys)
            {
                string value = data.changed[key];
                //upd += $" {key} = @{key},";


                try
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        //sqlparams.Add(new SqlParameter($"@{key}", null));

                        upd += $" {key} = null,";
                        continue;
                    }

                    upd += $" {key} = @{key},";

                    if (string.Equals(value, "true", StringComparison.OrdinalIgnoreCase))
                    {
                        sqlparams.Add(new SqlParameter($"@{key}", true));
                        continue;
                    }

                    if (string.Equals(value, "false", StringComparison.OrdinalIgnoreCase))
                    {
                        sqlparams.Add(new SqlParameter($"@{key}", false));
                        continue;
                    }

                    sqlparams.Add(new SqlParameter($"@{key}", value));
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    return false;
                }
            }

            if (UpdateLastDateTime)
            {
                upd += " last_updated_by_user_id = @last_updated_by_user_id, last_updated_datetime = @last_updated_datetime";
                sqlparams.Add(new SqlParameter("@last_updated_by_user_id", userId));
                sqlparams.Add(new SqlParameter("@last_updated_datetime", currentDt));
            }
            else
            {
                upd = upd.Substring(0, upd.Length - 1);
            }

            // await ExecuteQuery($"{upd} WHERE {UpdateWhereClause}", dict);
            using (var db = new InternalSystemEntities())
            {
                try
                {
                    int recCnt = await db.Database.ExecuteSqlCommandAsync($"{upd} WHERE {UpdateWhereClause}", sqlparams.ToArray());
                    return recCnt > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
