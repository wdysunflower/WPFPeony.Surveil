using System;
using System.Diagnostics;
using System.Reflection;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 反射创建对象
    /// </summary>  
    public static class CreatorCom
    {
        #region 反射创建对象

        #region 常规

        /// <summary>
        /// 根据程序集、类及命名空间反射创建对象
        /// </summary>
        /// <param name="assemlyName">程序集名称</param>
        /// <param name="namespaceName">命名空间</param>
        /// <param name="className">类名称</param>
        /// <returns>对象</returns>
        public static object CreateObject(string assemlyName, string namespaceName, string className)
        {
            if (String.IsNullOrEmpty(assemlyName) || String.IsNullOrEmpty(className))
                return null;

            string objname = string.Format("{0}.{1}", assemlyName == namespaceName ? assemlyName : namespaceName, className);

            //利用反射创建操作对象           
            object obj = null;
            try
            {
                var type = Type.GetType(string.Format("{0},{1}", objname, assemlyName));
                if (type != null)
                {
                    Assembly assembly = Assembly.GetAssembly(type);
                    obj = assembly.CreateInstance(objname);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return obj;
        }

        /// <summary>
        /// 根据程序集、类反射创建对象(默认程序集名称与命名空间名称相同)
        /// </summary>
        /// <param name="assemlyName">程序集名称</param>
        /// <param name="className">类名称</param>
        /// <returns>对象</returns>
        public static object CreateObject(string assemlyName, string className)
        {
            return CreateObject(assemlyName, assemlyName, className);
        }

        #endregion

        #region 带参数

        /// <summary>
        /// 根据程序集、类、命名空间及所需参数反射创建对象
        /// </summary>
        /// <param name="assemlyName">程序集名称</param>
        /// <param name="namespaceName">命名空间</param>
        /// <param name="className">类名称</param>
        /// <param name="parameters">对象所需参数</param>
        /// <returns>对象</returns>
        public static object CreateObjectByParam(string assemlyName, string namespaceName, string className,
                                                 object[] parameters)
        {
            if (String.IsNullOrEmpty(assemlyName) || String.IsNullOrEmpty(className))
                return null;

            string objname = string.Format("{0}.{1}", assemlyName == namespaceName ? assemlyName : namespaceName, className);

            //利用反射创建操作对象           
            object obj = null;
            try
            {
                Type type = Type.GetType(string.Format("{0},{1}", objname, assemlyName));
                if (type != null)
                {
                    Assembly assembly = Assembly.GetAssembly(type);
                    obj = assembly.CreateInstance(objname, true, BindingFlags.Default, null, parameters, null, null);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return obj;
        }

        /// <summary>
        /// 根据程序集、类、命名空间及所需参数反射创建对象(默认程序集名称与命名空间名称相同)
        /// </summary>
        /// <param name="assemlyName">程序集名称</param>
        /// <param name="className">类名称</param>
        /// <param name="parameters">对象所需参数</param>
        /// <returns>对象</returns>
        public static object CreateObjectByParam(string assemlyName, string className, object[] parameters)
        {
            return CreateObjectByParam(assemlyName, assemlyName, className, parameters);
        }

        #endregion

        #endregion

        #region 反射获取类型

        /// <summary>
        /// 获取指定信息对应的类型
        /// </summary>
        /// <param name="assemlyName">程序集名称</param>
        /// <param name="namespaceName">命名空间</param>
        /// <param name="className">类名称</param>
        /// <returns></returns>
        public static Type GetObjType(string assemlyName, string namespaceName, string className)
        {
            var obj = CreateObject(assemlyName, namespaceName, className);
            return obj != null ? obj.GetType() : null;
        }

        /// <summary>
        /// 获取指定信息对应的类型(默认程序集名称与命名空间名称相同)
        /// </summary>
        /// <param name="assemlyName">程序集名称</param>
        /// <param name="className">类名称</param>
        /// <returns></returns>
        public static Type GetObjType(string assemlyName, string className)
        {
            return GetObjType(assemlyName, assemlyName, className);
        }

        #endregion
    }
}