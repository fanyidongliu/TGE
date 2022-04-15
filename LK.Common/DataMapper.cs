using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LK.Common
{
    public static class DataMapper
    {
        /// <summary>
        /// 对象映射函数
        /// </summary>
        /// <typeparam name="S">源类</typeparam>
        /// <typeparam name="T">目的类</typeparam>
        /// <param name="s">源类对象</param>
        /// <returns></returns>
        public static T Mapper<S, T>(S s)
            where T : class, new()
            where S : class, new()
        {
            if (s == null)
                return null;
            MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.CreateMap<S, T>());
            var mapper = configuration.CreateMapper();
            return mapper.Map<T>(s);
        }
    }
}
