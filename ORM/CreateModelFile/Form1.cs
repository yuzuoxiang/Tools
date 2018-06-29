using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using SqlSugarTool;

namespace CreateModelFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            string exportPath = ConfigurationManager.AppSettings["path"];
            string nameSpace = ConfigurationManager.AppSettings["nameSpace"];

            txtExportPath.Text = exportPath;
            txtNameSpace.Text = nameSpace;
        }

        private void btnBuilder_Click(object sender, EventArgs e)
        {
            var models = txtModelNames.Text;
            var nameSpace = txtNameSpace.Text;
            var dbName = txtDBName.Text;
            var path = txtExportPath.Text;

            try
            {
                if (Validate(models, nameSpace, dbName, path) != "")
                {
                    MessageBox.Show("参数不能为空！");
                    return;
                }

                var db = SugarDao.GetInstance(dbName);
                string[] tabs = models.Split(',');

                db.DbFirst.Where(tabs).CreateClassFile(path, nameSpace);
                MessageBox.Show("生成成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 是否为空验证
        /// </summary>
        /// <param name="models"></param>
        /// <param name="nameSpace"></param>
        /// <param name="dbName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private string Validate(string models, string nameSpace, string dbName, string path)
        {
            string msg = "";

            if (string.IsNullOrWhiteSpace(models))
                msg = "模型名称不能为空";
            if (string.IsNullOrWhiteSpace(nameSpace))
                msg = "命名空间名称不能为空";
            if (string.IsNullOrWhiteSpace(dbName))
                msg = "数据库名称不能为空";
            if (string.IsNullOrWhiteSpace(path))
                msg = "导出路径不能为空";

            return msg;
        }
    }
}
