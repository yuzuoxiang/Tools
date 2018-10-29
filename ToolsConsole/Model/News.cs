using SqlSugar;
using System;
using System.Linq;
using System.Text;

namespace Models
{
    ///<summary>
    ///
    ///</summary>
    public partial class News
    {
        public News()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:标题
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NewsTitle { get; set; }

        /// <summary>
        /// Desc:新闻内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Txt { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// Desc:咨询类型，1：活动；；2：公告；3：资讯
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? NewsType { get; set; }

        /// <summary>
        /// Desc:Pc端图片地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NewsPcPic { get; set; }

        /// <summary>
        /// Desc:移动端图片链接
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NewsMobliePic { get; set; }

        /// <summary>
        /// Desc:是否显示，1：显示；2：隐藏
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public int? Display { get; set; }

        /// <summary>
        /// Desc:标题颜色
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FontColor { get; set; }

        /// <summary>
        /// Desc:编辑人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? EditorId { get; set; }

        /// <summary>
        /// Desc:用户点击数量
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public int? ClickNum { get; set; }

        /// <summary>
        /// Desc:新闻有效起始时间
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public DateTime? ValidStartTime { get; set; }

        /// <summary>
        /// Desc:新闻有效结束时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? ValidEndTime { get; set; }

        /// <summary>
        /// Desc:是否可点击；1：可点击，2不可点击
        /// Default:1
        /// Nullable:True
        /// </summary>           
        public int? ValidClick { get; set; }

        /// <summary>
        /// Desc:权重；降序
        /// Default:0
        /// Nullable:True
        /// </summary>           
        public int? IsTop { get; set; }

        /// <summary>
        /// Desc:跳转目标类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? TargetType { get; set; }

        /// <summary>
        /// Desc:跳转地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string Href { get; set; }

    }
}
