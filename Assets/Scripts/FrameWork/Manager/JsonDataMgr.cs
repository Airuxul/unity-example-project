using System;
using System.IO;
using LitJson;
using UnityEngine;

namespace FrameWork.Manager
{
    /// <summary>
    /// 通过litJson使用jsondata
    /// </summary>
    public class JsonDataMgr : BaseManager
    {
        //固定读取JsonData文件内的Json文件
        private static readonly string rootFolderPath =Application.dataPath + "/JsonData/";

        /// <summary>
        /// 保存T类对象为fileName名字的文件
        /// </summary>
        /// <param name="t">转换对象</param>
        /// <param name="subPath">子分支路径</param>
        /// <param name="fileName">文件名字</param>
        /// <typeparam name="T">类的类型</typeparam>
        public void SaveToJson<T>(T t,string subPath,string fileName)
        {
            //所处文件夹路径
            string folderPath = rootFolderPath + subPath;
            //文件路径
            string filePath = folderPath +"/"+ fileName;
        
            //没有文件夹则创建该文件夹
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (!Directory.Exists(folderPath))
            {
                Debug.LogError("本地文件夹创建失败");
            }
        
            string jsonStr = JsonMapper.ToJson(t);
        
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8)) 
            {
                sw.WriteLine(jsonStr);
            }
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        /// <summary>
        ///  读取fileName名字的Json文件并返回T类型对象
        /// </summary>
        /// <param name="subPath">子分支路径</param>
        /// <param name="fileName">文件名字</param>
        /// <typeparam name="T">类的类型</typeparam>
        /// <returns>返回类的对象</returns>
        public T LoadFromJson<T>(string subPath,string fileName)
        {
            try
            {
                //所处文件夹路径
                string folderPath = rootFolderPath + subPath;
                //文件路径
                string filePath = folderPath +"/"+ fileName;
        
                //没有文件夹则创建该文件夹
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                    Debug.Log("Json文件内容为空");
                    return default;
                }

                T t = JsonMapper.ToObject<T>(File.ReadAllText(filePath));

                if (t != null)
                    return t;
                Debug.Log("Json文件内容为空");
                return default;
            }
            catch(Exception e)
            {
                Debug.LogError(e);
                return default;
            }
        }
    }
}
