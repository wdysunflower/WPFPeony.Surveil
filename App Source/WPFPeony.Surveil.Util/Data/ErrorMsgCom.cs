using System;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 异常/错误消息设置及获取
    /// </summary>
    public static class ErrorMsgCom
    {
        /// <summary>
        /// 错误字符串
        /// </summary>
        private static string _errorStr;

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="str">错误的描述信息</param>
        public static void SetErrorStr(string str)
        {
            _errorStr = str;
        }

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="key">错误信息对应的Key</param>
        public static void SetErrorStrByKey(string key)
        {
            _errorStr = ResourceCom.GetString(key);
        }

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="type">错误信息对应的枚举类型</param>
        /// <param name="value">错误信息对应的枚举值</param>
        public static void SetErrorStrByEnum(Type type, int value)
        {
            string enumName = Enum.GetName(type, value);
            if (String.IsNullOrEmpty(enumName))
                _errorStr = ResourceCom.GetString("NoDefineError") + value;
            else
                _errorStr = ResourceCom.GetString(enumName);
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        public static string GetErrorResourceStr()
        {
            if (!String.IsNullOrEmpty(_errorStr))
            {
                string error = _errorStr;
                _errorStr = null;
                return error;
            }
            return ResourceCom.GetString("无具体错误消息");
        }
    }
}