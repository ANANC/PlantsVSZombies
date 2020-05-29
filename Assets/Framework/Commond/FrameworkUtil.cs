using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FrameworkUtil
{
    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    private static string m_DataPath = string.Empty;

    public static string DataPath
    {
        get
        {
            if (!string.IsNullOrEmpty(m_DataPath))
            {
                return m_DataPath;
            }
            else
            {
                string game = "Game";
                if (Application.isMobilePlatform)
                {
                    m_DataPath = Application.persistentDataPath + "/" + game + "/";
                }
                else
                {
                    m_DataPath = Application.dataPath + "/../c/" + game + "/";
                }

                return m_DataPath;
            }
        }
    }


    /// <summary>
    /// 应用程序内容路径
    /// </summary>
    private static string m_AppContentPath = string.Empty;
    public static string AppContentPath
    {
        get
        {
            if (!string.IsNullOrEmpty(m_AppContentPath))
            {
                return m_AppContentPath;
            }
            else
            {
                m_AppContentPath = Application.streamingAssetsPath + "/";
                return m_AppContentPath;
            }
        }
    }

    public static bool DoublePath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return false;
            }
            return true;
        }
    }

    public static string GetPlatformPath(string filename)
    {
        if (DoublePath)
        {
            string persistentPath = DataPath + filename;
            if (File.Exists(persistentPath))
            {
                return persistentPath;
            }
            else
            {
                return AppContentPath + filename;
            }
        }
        else
        {
            return DataPath + filename;
        }
    }

}

