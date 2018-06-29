using System.Configuration;

namespace Tools.Config
{
    public class SiteOption: ConfigurationElement
    {
        /// <summary>
        /// 站点名称 eg:dzsc.com
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
        }

        /// <summary>
        /// 上传域 eg:http://upload.dzsc.com
        /// </summary>
        [ConfigurationProperty("uploaddomain", IsRequired = true)]
        public string UploadDomain
        {
            get
            {
                return this["uploaddomain"] as string;
            }
        }


        /// <summary>
        /// 获取配置的路径
        /// </summary>
        [ConfigurationProperty("settingurl", IsRequired = true)]
        public string SettingUrl
        {
            get
            {
                return this["settingurl"] as string;
            }
        }
    }
}
