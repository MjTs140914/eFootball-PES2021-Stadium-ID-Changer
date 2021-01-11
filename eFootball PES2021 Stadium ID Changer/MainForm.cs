using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using GzsTool.Core.Common;
using GzsTool.Core.Common.Interfaces;
using GzsTool.Core.Fpk;
using GzsTool.Core.Pftxs;
using GzsTool.Core.Qar;
using GzsTool.Core.Sbp;
using System.Xml.Serialization;
using System.Text;

namespace eFootball_PES2021_Stadium_ID_Changer
{
    public partial class MainForm : Form
    {
        private bool InstanceFieldsInitialized = false;

        public MainForm()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }

            InitializeComponent();
        }
        private void InitializeInstanceFields()
        {
            processStartInfo = process1.StartInfo;
        }
        private string old_id;
        private string new_id;
        private string f_dir;
        private const string temPath = "C:\\ProgramData\\PES Tools\\temp";
        private Process process1 = new Process();
        private ProcessStartInfo processStartInfo;
        private string[] str;
        private List<string> fik_fpkd = new List<string>();
        private List<string> fox2 = new List<string>();
        private List<string> xml_list = new List<string>();
        private List<string> fmdl_list = new List<string>();
        private ListBox lb_old_file_etc = new ListBox();
        private ListBox lb_new_file_etc = new ListBox();
        private List<string> dir_list_del = new List<string>();
        private List<string> xml_fox2 = new List<string>();
        private List<string> xml_fpk = new List<string>();

        private void tx_old_TextChanged(object sender, EventArgs e)
        {
            old_id = tx_old.Text.ToLower();
        }

        private void tx_new_TextChanged(object sender, EventArgs e)
        {
            new_id = tx_new.Text.ToLower();
        }
        public void SaveFileFromResources(object fileInResources, string saveFilePath)
        {
            byte[] myfile = (byte[])fileInResources;
            Microsoft.VisualBasic.FileIO.FileSystem.WriteAllBytes(saveFilePath, myfile, true);
        }

        private void wait(int seconds)
        {
            int tempVar = seconds * 100;
            for (int i = 0; i <= tempVar; i++)
            {
                System.Threading.Thread.Sleep(10);
                Application.DoEvents();
            }
        }

        private void read(string fullPath)
        {

            if (!Path.IsPathRooted(fullPath))
            {
                fullPath = Path.GetFullPath(fullPath);
            }
            if (File.Exists(fullPath))
            {
                string extension = Path.GetExtension(fullPath);
                string str = extension;
                if (extension != null)
                {
                    if (str == ".xml")
                    {
                        WriteArchive(fullPath);
                        return;
                    }
                }
            }
            else if (Directory.Exists(fullPath))
            {

                return;
            }
        }

        private void WriteArchive(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                ArchiveFile archiveFile = (ArchiveFile)ArchiveSerializer.Deserialize(fileStream) as ArchiveFile;
                if (archiveFile != null)
                {
                    WriteArchive(archiveFile, directoryName);
                }
            }
        }

        private void WriteArchive(ArchiveFile archiveFile, string workingDirectory)
        {
            string str = Path.Combine(workingDirectory, archiveFile.Name);
            string str1 = string.Format("{0}\\{1}_{2}", workingDirectory, Path.GetFileNameWithoutExtension(archiveFile.Name), Path.GetExtension(archiveFile.Name).Replace(".", ""));
            IDirectory fileSystemDirectory = new FileSystemDirectory(str1);
            using (FileStream fileStream = new FileStream(str, FileMode.Create))
            {
                archiveFile.Write(fileStream, fileSystemDirectory);
            }

        }
        private readonly XmlSerializer ArchiveSerializer = new XmlSerializer(typeof(ArchiveFile),
        new Type[] { typeof(FpkFile), typeof(PftxsFile), typeof(QarFile), typeof(SbpFile) });

        private void change_filename()
        {
            DirectoryInfo folder = new DirectoryInfo(f_dir);
            foreach (FileInfo file in folder.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.ToString().Contains(old_id))
                {
                    lb_old_file_etc.Items.Add(file.FullName);
                }
            }
            foreach (Object ritem in lb_old_file_etc.Items)
            {
                lb_new_file_etc.Items.Add(Path.GetFileName(Convert.ToString(ritem)));
                for (var i = 0; i < lb_new_file_etc.Items.Count; i++)
                {
                    if (lb_new_file_etc.Items[i].ToString().Contains(old_id))
                    {
                        lb_new_file_etc.Items[i] = lb_new_file_etc.Items[i].ToString().Replace(old_id, new_id);
                        if (File.Exists(Convert.ToString(ritem)))
                        {
                            Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(Convert.ToString(ritem), Convert.ToString(lb_new_file_etc.Items[i]));
                        }
                    }
                }
            }
        }

        private void change_dirname(string dir_name)
        {
            lb_old_file_etc.Items.Clear();
            lb_new_file_etc.Items.Clear();
            if (Directory.Exists(dir_name))
            {
                foreach (string Dir in Directory.GetDirectories(dir_name))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                    lb_old_file_etc.Items.Add(dirInfo.FullName);
                }
                foreach (object ritem in lb_old_file_etc.Items)
                {
                    lb_new_file_etc.Items.Add(Path.GetFileName(Convert.ToString(ritem)));
                    for (var i = 0; i < lb_new_file_etc.Items.Count; i++)
                    {
                        if (lb_new_file_etc.Items[i].ToString().Contains(old_id))
                        {
                            lb_new_file_etc.Items[i] = lb_new_file_etc.Items[i].ToString().Replace(old_id, new_id);
                            Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(Convert.ToString(ritem), Convert.ToString(lb_new_file_etc.Items[i]));
                        }
                    }
                }
            }
        }

        private void delete_dirname(string dir_name)
        {
            if (Directory.Exists(dir_name))
            {
                foreach (string Dir in Directory.GetDirectories(dir_name))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                    dir_list_del.Add(dirInfo.FullName);
                    Directory.Delete(Dir, true);
                }
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            lbl_info.Text = null;
            if (!Directory.Exists(temPath))
            {
                Directory.CreateDirectory(temPath);
            }
            if (!File.Exists(temPath + "\\FoxTool.exe"))
            {
                SaveFileFromResources(Properties.Resources.FoxTool, temPath + "\\FoxTool.exe");
            }
            if (!File.Exists(temPath + "\\FCityHash.dll"))
            {
                SaveFileFromResources(Properties.Resources.CityHash, temPath + "\\CityHash.dll");
            }
            if (!File.Exists(temPath + "\\GzsTool.exe"))
            {
                SaveFileFromResources(Properties.Resources.GzsTool, temPath + "\\GzsTool.exe");
            }
            if (!File.Exists(temPath + "\\fox_dictionary.txt"))
            {
                const string noth = "/Assets/tpp/common_source/environ/guantanamo/cm_gntn_shtt001/sourceimages/cm_gntn_shtt001_srm.ftex";
                File.WriteAllText(temPath + "\\fox_dictionary.txt", noth);
            }
        }

        private void btn_selectfolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog { Description = "Select folder contains 'Assets' and 'common.'" };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                f_dir = fbd.SelectedPath;
                List<string> list_string = new List<string>();
                list_string.Clear();
                foreach (string Dir in Directory.GetDirectories(f_dir))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                    list_string.Add(dirInfo.Name);
                }
                if (list_string.Contains("Asset") && list_string.Contains("common"))
                {
                    lbl_info.ForeColor = Color.Green;
                    lbl_info.Text = "Selected directories ok.";
                    btn_fpk.Enabled = true;
                }
                else
                {
                    lbl_info.ForeColor = Color.Red;
                    lbl_info.Text = "Selected directories error or not contains 'Asset' and 'common' directories.";
                    btn_fpk.Enabled = false;
                    return;
                }
                try
                {
                    foreach (string Dir in Directory.GetDirectories(f_dir + "\\Asset\\model\\bg"))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Dir);
                        if (dirInfo.Name.Contains("st"))
                        {
                            tx_old.Text = dirInfo.Name;
                            tx_new.Text = "st000";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }

            }
        }

        private void btn_fpk_Click(object sender, EventArgs e)
        {
            if (!(tx_new.Text.Count() == 5))
            {
                MessageBox.Show("Text Length must be 5", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tx_old.Text == tx_new.Text)
            {
                MessageBox.Show("Do not use same ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tx_old.Enabled = false;
            tx_new.Enabled = false;
            btn_selectfolder.Enabled = false;
            btn_fpk.Enabled = false;
            lbl_info.Text = "Extracting FPK/FPKD and Decompiling FOX2, Please wait a moment.";
            lbl_info.ForeColor = Color.OrangeRed;
            wait(1);
            if (!Directory.Exists(f_dir + "_Backup"))
            {
                Directory.CreateDirectory(f_dir + "_Backup");
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(f_dir, f_dir + "_Backup");
            }
            fik_fpkd.Clear();
            DirectoryInfo folder = new DirectoryInfo(f_dir);
            foreach (FileInfo file in folder.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.Extension == ".fpk" || file.Extension == ".fpkd")
                {
                    fik_fpkd.Add(file.FullName);
                }
            }
            foreach (var file2 in fik_fpkd)
            {
                process1.StartInfo.FileName = temPath + "\\GzsTool.exe";
                str = new string[] { string.Format("\"{0}\"", file2) };
                processStartInfo.Arguments = string.Concat(str);
                processStartInfo.WorkingDirectory = temPath;
                process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.CreateNoWindow = true;
                process1.Start();
                process1.WaitForExit();
                File.Delete(file2);
            }
            DirectoryInfo folder2 = new DirectoryInfo(f_dir);
            foreach (FileInfo file in folder2.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.Extension == ".fox2")
                {
                    fox2.Add(file.FullName);
                }
            }
            foreach (var file3 in fox2)
            {
                process1.StartInfo.FileName = temPath + "\\FoxTool.exe";
                str = new string[] { string.Format(" -d \"{0}\"", file3) };
                processStartInfo.Arguments = string.Concat(str);
                processStartInfo.WorkingDirectory = temPath;
                process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.CreateNoWindow = true;
                process1.Start();
                process1.WaitForExit();
                File.Delete(file3);
            }
            btn_change_id.Enabled = true;
            lbl_info.ForeColor = Color.Green;
            lbl_info.Text = "Extracting FPK/FPKD and Decompiling FOX2 done.";
        }

        private void btn_change_id_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_info.ForeColor = Color.OrangeRed;
                lbl_info.Text = "Change ID in progress.";
                btn_change_id.Enabled = false;
                wait(1);
                DirectoryInfo folder = new DirectoryInfo(f_dir);
                foreach (FileInfo file in folder.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (file.Extension == ".xml")
                    {
                        xml_list.Add(file.FullName);
                    }
                }
                foreach (var Xmls in xml_list)
                {
                    string xml_contents = File.ReadAllText(Xmls);
                    xml_contents = xml_contents.Replace(old_id, new_id);
                    File.WriteAllText(Xmls, xml_contents);
                }

                foreach (FileInfo file2 in folder.GetFiles("*.*", SearchOption.AllDirectories))
                {
                    if (file2.Extension == ".fmdl")
                    {
                        fmdl_list.Add(file2.FullName);
                    }
                }
                foreach (var fmdl in fmdl_list)
                {
                    byte[] xml_contents = File.ReadAllBytes(fmdl);
                    xml_contents = ByteToReplace.ArrayReplace(xml_contents, old_id, new_id, Encoding.UTF8);
                    File.WriteAllBytes(fmdl, xml_contents);
                }
                change_filename();
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\audi\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\big_flag\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\cheer\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\light\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\pitch\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\scarecrow\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\staff\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\standsFlag\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\tv\\#Win", f_dir, old_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\common\\ad\\#Win", f_dir));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\common\\tv", f_dir));
                change_dirname(string.Format("{0}\\common\\render\\model\\bg\\hit\\stadium", f_dir));
                change_dirname(string.Format("{0}\\common\\bg\\model\\bg\\draw_parameter", f_dir));
                change_dirname(string.Format("{0}\\common\\bg\\model\\bg\\tv", f_dir));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_df_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_df_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_dr_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_dr_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_nf_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_nf_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_nr_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win\\{2}_nr_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\audi\\#Win\\audiarea_{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\audi\\#Win\\audiarea_{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\big_flag\\#Win\\home_big_flag_{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\big_flag\\#Win\\home_big_flag_{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\cheer\\#Win\\cheer_{2}_h_a1_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\cheer\\#Win\\cheer_{2}_h_a1_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\cheer\\#Win\\cheer_{2}_h_a2_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\cheer\\#Win\\cheer_{2}_h_a2_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_df_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_df_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_dr_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_dr_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_nf_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_nf_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_nr_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win\\effect_{2}_nr_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\light\\#Win\\light_{2}_af_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\light\\#Win\\light_{2}_af_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\light\\#Win\\light_{2}_ar_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\light\\#Win\\light_{2}_ar_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\pitch\\#Win\\pitch_{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\pitch\\#Win\\pitch_{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\scarecrow\\#Win\\scarecrow_{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\scarecrow\\#Win\\scarecrow_{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\staff\\#Win\\staff_{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\staff\\#Win\\staff_{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\standsFlag\\#Win\\flagarea_{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\standsFlag\\#Win\\flagarea_{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\tv\\#Win\\tv_{2}_fpk\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\tv\\#Win\\tv_{2}_fpkd\\Assets\\pes16\\model\\bg", f_dir, old_id, new_id));
                change_dirname(string.Format("{0}\\Asset\\model\\bg", f_dir));
                btn_pack.Enabled = true;
                lbl_info.ForeColor = Color.Green;
                lbl_info.Text = string.Format("Change ID done from: {0} to: {1}.", old_id, new_id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void btn_pack_Click(object sender, EventArgs e)
        {
            lbl_info.ForeColor = Color.OrangeRed;
            lbl_info.Text = "Packing FPK/FPKD and Compiling FOX2, Please wait a moment.";
            btn_pack.Enabled = false;
            DirectoryInfo folder2 = new DirectoryInfo(f_dir);
            foreach (FileInfo file in folder2.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.Extension == ".xml")
                {
                    if (file.ToString().Contains("fox2.xml"))
                    {
                        xml_fox2.Add(file.FullName);
                    }
                }
            }
            foreach (var file3 in xml_fox2)
            {
                process1.StartInfo.FileName = temPath + "\\FoxTool.exe";
                str = new string[] { string.Format(" -c \"{0}\"", file3) };
                processStartInfo.Arguments = string.Concat(str);
                processStartInfo.WorkingDirectory = temPath;
                process1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process1.StartInfo.UseShellExecute = false;
                process1.StartInfo.CreateNoWindow = true;
                process1.Start();
                process1.WaitForExit();
                File.Delete(file3);
            }
            DirectoryInfo folder = new DirectoryInfo(f_dir + "\\Asset\\model\\bg");
            foreach (FileInfo file2 in folder.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file2.Extension == ".xml")
                {
                    if (file2.ToString().Contains("fpk.xml") | file2.ToString().Contains("fpkd.xml"))
                    {
                        try
                        {
                            WriteArchive(file2.FullName);
                            read(file2.FullName);
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                        }
                        xml_fpk.Add(file2.FullName);
                    }
                }
            }
            foreach (var file2 in xml_fpk)
            {
                if (File.Exists(file2))
                {
                    File.Delete(file2);
                }
            }
            btn_finish.Enabled = true;
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\audi\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\big_flag\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\cheer\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\effect\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\light\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\pitch\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\scarecrow\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\staff\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\standsFlag\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\{1}\\tv\\#Win", f_dir, new_id));
            delete_dirname(string.Format("{0}\\Asset\\model\\bg\\common\\ad\\#Win", f_dir));
            lbl_info.ForeColor = Color.Green;
            lbl_info.Text = "Packing FPK/FPKD and Compiling FOX2 done.";
        }

        private void btn_finish_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
