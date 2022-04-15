using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LK.Common
{

    public static class PredicateBuilder
    {
        #region 用法实例
        //var predicate = PredicateBuilder.False<BaseCfgDictMaterielCode>();
        //foreach (var item in listMaterielType3)
        //{
        //string nTemp = item.InsiteCode;
        //predicate = predicate.Or(p => p.TypeInsiteCode == nTemp);
        //}
        //var predicate1 = PredicateBuilder.True<BaseCfgDictMaterielCode>();
        //if(!temp.IsNullOrEmpty())
        //predicate1 = predicate1.And(c => c.Name.Contains(temp)); 
        #endregion

        /// <summary>
        /// 构造函数使用True时：单个AND有效，多个AND有效；单个OR无效，多个OR无效；混合时写在AND后的OR有效
        /// 构造函数使用False时：单个AND无效，多个AND无效；单个OR有效，多个OR有效；混合时写在OR后面的AND有效
        /// </summary>
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Predicate<T> ConvertToPredicate<T>(this Func<T, bool> func)
        {
            return new Predicate<T>(func);
        }
    }
}
