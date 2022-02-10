using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string m_strFolder = string.Empty;
        private DataTable m_lst = new DataTable();

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.ShowDialog();
            m_strFolder = fbd.SelectedPath;
            textBox1.Text = m_strFolder;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(m_strFolder))
            {
                if (!radioButton1.Checked) {
                    MessageBox.Show("You have to select a RadioBox");
                }
                else
                {
                    ReadPackagesInfo(m_strFolder);
                }
                dataGridView1.DataSource = m_lst;
                dataGridView1.Refresh();
            }
            else{
                MessageBox.Show("You have to select a Source Folder");
            }
        }

        private void ReadPackagesInfo(string strDirectory)
        {
            m_lst.Clear();
            m_lst.Columns.Add("PackageFileName", typeof(string));
            m_lst.Columns.Add("PackageFormatVersion", typeof(string));
            m_lst.Columns.Add("CreationDate", typeof(string));
            m_lst.Columns.Add("CreationName", typeof(string));
            m_lst.Columns.Add("CreatorComputerName", typeof(string));
            m_lst.Columns.Add("DTSID", typeof(string));
            m_lst.Columns.Add("ExecutableType", typeof(string));
            m_lst.Columns.Add("LastModifiedProductVersion", typeof(string));
            m_lst.Columns.Add("LocaleID", typeof(string));
            m_lst.Columns.Add("ObjectName", typeof(string));
            m_lst.Columns.Add("PackageType", typeof(string));
            m_lst.Columns.Add("VersionBuild", typeof(string));
            m_lst.Columns.Add("VersionGUID", typeof(string));

            foreach (var strFile in System.IO.Directory.GetFiles(strDirectory, "*.dtsx", System.IO.SearchOption.AllDirectories))
            {
                string strContent = "";

                StreamReader sr = new StreamReader(strFile);
                strContent = sr.ReadToEnd();
                sr.Close();

                string strPackageFormatVersion = Regex.Match(strContent, @"(?<=""PackageFormatVersion"">)(.*)(?=</DTS:Property>)", RegexOptions.Singleline).Value;
                string strCreationDate = Regex.Match(strContent, @"(?<=DTS:CreationDate="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strCreationName = Regex.Match(strContent, @"(?<=DTS:CreationName="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strCreatorComputerName = Regex.Match(strContent, @"(?<=DTS:CreatorComputerName="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strCreatorName = Regex.Match(strContent, @"(?<=DTS:CreatorName"">)(.*)(?=</DTS:Property>)", RegexOptions.Singleline).Value;
                string strDTSID = Regex.Match(strContent, @"(?<=DTS:DTSID="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strExecutableType = Regex.Match(strContent, @"(?<=DTS:ExecutableType="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strLastModifiedProductVersion = Regex.Match(strContent, @"(?<=DTS:LastModifiedProductVersion="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strLocaleID = Regex.Match(strContent, @"(?<=DTS:LocaleID="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strObjectName = Regex.Match(strContent, @"(?<=DTS:ObjectName="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strPackageType = Regex.Match(strContent, @"(?<=DTS:PackageType="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strVersionBuild = Regex.Match(strContent, @"(?<=DTS:VersionBuild="")(.*?)(?="")", RegexOptions.Singleline).Value;
                string strVersionGUID = Regex.Match(strContent, @"(?<=DTS:VersionGUID="")(.*?)(?="")", RegexOptions.Singleline).Value;



                m_lst.Rows.Add(strFile,
                    strPackageFormatVersion, strCreationDate, strCreationName,
                    strCreatorComputerName, strDTSID,
                    strExecutableType, strLastModifiedProductVersion, strLocaleID,
                    strObjectName, strPackageType, strVersionBuild, strVersionGUID
                    );

            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
