using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;

namespace CTOnboarding
{
    public partial class EZView : Form
    {
        private TreeNode selectedNode;
        bool changesMade = false;
        //private TreeNode oldSelectedNode;
        DataTable parsedData = new DataTable();

        private void SaveData()
        {
            XmlWriter writer = null;

            try
            {
                // Create an XmlWriterSettings object with the correct options.
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;

                // Create the XmlWriter object and write some content.
                writer = XmlWriter.Create("treestructure.xml", settings);
                //writer.WriteStartElement("Default");
                SaveNodes(treeView1.Nodes, writer);
                //writer.WriteEndAttribute();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

            parsedData.TableName = "ADGroups";
            parsedData.WriteXml("DataXML.xml", XmlWriteMode.IgnoreSchema);
            parsedData.WriteXmlSchema("DataSchema.xml");
            changesMade = false;
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (!changesMade) return;

            DialogResult exitResult = MessageBox.Show("Would you like to save?", "Exiting...", MessageBoxButtons.YesNoCancel);
            if (exitResult == DialogResult.Yes)
            {
                SaveData();
            }
            else if(exitResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        public EZView()
        {
            InitializeComponent();
            LoadTreeViewFromXmlFile();

            if (!File.Exists("DataSchema.xml")) { 
                File.WriteAllText("DataSchema.xml", Properties.Resources.DataSchemaDefault);
            }
            parsedData.ReadXmlSchema("DataSchema.xml");


            if (!File.Exists("DataXML.xml"))
            {
                File.WriteAllText("DataXML.xml", Properties.Resources.DataXMLDefault);
            }
            parsedData.ReadXml("DataXML.xml");


            dataGridView1.DataSource = parsedData;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Visible = false;
            }
        }

        #region Hierarchy interactions

        protected void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode curNode = treeView1.GetNodeAt(e.X, e.Y);

            if (e.Button == MouseButtons.Left && curNode == selectedNode)
            {
                curNode.BeginEdit();
            }

            treeView1.SelectedNode = curNode;


            if (e.Button != MouseButtons.Right) return;


            contextMenuStrip1.Show(treeView1, e.X, e.Y);
        }

        protected void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || treeView1.SelectedNode == treeView1.Nodes[0]) return;

            TreeNode nodeToDelete = treeView1.SelectedNode;
            TreeNode nextNode = treeView1.SelectedNode.PrevNode != null ? treeView1.SelectedNode.PrevNode : treeView1.SelectedNode.Parent;

            treeView1.SelectedNode = nextNode;

            if (parsedData.Columns[nodeToDelete.Text] != null)
            {
                parsedData.Columns.Remove(nodeToDelete.Text);
            }
            treeView1.Nodes.Remove(nodeToDelete);
            changesMade = true;
        }

        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null && dataGridView1.Columns[treeView1.SelectedNode.Text] != null)
            {
                if (selectedNode != null && dataGridView1.Columns[selectedNode.Text] != null)
                    dataGridView1.Columns[selectedNode.Text].Visible = false;

                dataGridView1.Columns[treeView1.SelectedNode.Text].Visible = true;
            }

            if (dataGridView1.Columns[treeView1.SelectedNode.Text] != null)
                selectedNode = treeView1.SelectedNode;
        }

        private static bool StringIsValid(string str)
        {
            char[] cArray = str.ToCharArray();

            foreach(char c in cArray)
            {
                if(!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') ||
                (c >= '0' && c <= '9') || (c == '-')))
                {
                    return false;
                }
            }
            return true;
        }

        private void treeView1_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            if (e.Label != null && StringIsValid(e.Label) && parsedData.Columns[e.Label] == null && parsedData.Columns[e.Label] == null)
            {
                parsedData.Columns[e.Node.Text].ColumnName = e.Label;
                changesMade = true;
            }
            else
            {
                e.CancelEdit = true;
            }

        }

        private void AddChild(object sender, EventArgs e)
        {
            string newName = "New";
            int failTimes = 0;

            while(parsedData.Columns[newName] != null)
            {
                newName = "New" + (failTimes).ToString();
                failTimes++;
            }

            TreeNode newChild = treeView1.SelectedNode.Nodes.Add(newName);
            treeView1.SelectedNode.Expand();
            newChild.BeginEdit();
            

            parsedData.Columns.Add(newName);
            treeView1.SelectedNode = newChild;
            changesMade = true;
        }

        // Load a TreeView control from an XML file.
        private void LoadTreeViewFromXmlFile()
        {
            // Load the XML document.
            XmlDocument xml_doc = new XmlDocument();
            if (!File.Exists("treestructure.xml"))
            {
                File.WriteAllText("treestructure.xml", Properties.Resources.TreeStructureDefault);
            }
            xml_doc.Load("treestructure.xml");

            // Add the root node's children to the TreeView.
            treeView1.Nodes.Clear();
            //treeView1.Nodes.Add(new
            //    TreeNode(xml_doc.DocumentElement.Name));
            TreeNode parent = treeView1.Nodes.Add(xml_doc.FirstChild.Name);
            AddTreeViewChildNodes(parent.Nodes, xml_doc.DocumentElement);

            treeView1.Sort();
        }

        private void SaveNodes(TreeNodeCollection nodesCollection, XmlWriter textWriter)
        {
            foreach (var node in nodesCollection.OfType<TreeNode>())
            {
                textWriter.WriteStartElement(node.Text);
                SaveNodes(node.Nodes, textWriter);
                textWriter.WriteEndElement();
            }
        }

        private void AddTreeViewChildNodes(
            TreeNodeCollection parent_nodes, XmlNode xml_node)
        {
            foreach (XmlNode child_node in xml_node.ChildNodes)
            {
                // Make the new TreeView node.
                TreeNode new_node = parent_nodes.Add(child_node.Name);

                // Recursively make this node's descendants.
                AddTreeViewChildNodes(new_node.Nodes, child_node);

                // If this is a leaf node, make sure it's visible.
                if (new_node.Nodes.Count == 0) new_node.EnsureVisible();
            }
        }

        #endregion

        #region Datatable interactions

        public void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    cell.Value = "";
                }

                parsedData.Rows.Cast<DataRow>().ToList().FindAll(
                    row => string.IsNullOrEmpty(string.Join("", row.ItemArray))
                    ).ForEach(row => { parsedData.Rows.Remove(row); });

                changesMade = true;
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            dataGridView1.ClearSelection();
            DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
            if(hit.RowIndex != -1 && hit.ColumnIndex != -1)
            {
                dataGridView1.Rows[hit.RowIndex].Cells[hit.ColumnIndex].Selected = true;
                contextMenuStrip2.Show(dataGridView1, e.X, e.Y);
            }

        }

        private void Paste(object sender, EventArgs e)
        {
            string clipData = Clipboard.GetText();
            string[] clipdatas = clipData.Split('\n');
            if (clipdatas[clipdatas.Length - 1] == "")
            {
                clipdatas = clipdatas.Take(clipdatas.Count() - 1).ToArray();
            }

            int cellRow = dataGridView1.SelectedCells[0].RowIndex - 1;
            int cellColumn = dataGridView1.SelectedCells[0].ColumnIndex;

            foreach (string clip in clipdatas)
            {
                if (cellRow + 1 >= parsedData.Rows.Count)
                {
                    DataRow row = parsedData.NewRow();
                    parsedData.Rows.Add(row);
                }
                cellRow = cellRow + 1;

                parsedData.Rows[cellRow][cellColumn] = clip;
            }

            parsedData.AcceptChanges();

            changesMade = true;
        }

        #endregion

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            changesMade = true;
        }
    }
}
