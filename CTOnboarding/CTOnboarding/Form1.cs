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

namespace CTOnboarding
{
    public partial class Form1 : Form
    {
        private TreeNode selectedNode;
        //private TreeNode oldSelectedNode;
        DataTable parsedData = new DataTable();

        protected void treeView1_AfterSelect(object sender,
System.Windows.Forms.TreeViewEventArgs e)
        {
            System.Console.WriteLine(treeView1.SelectedNode.Text);

            if (treeView1.SelectedNode != null && dataGridView1.Columns[treeView1.SelectedNode.Text] != null)
            {
                if(selectedNode!=null && dataGridView1.Columns[selectedNode.Text] != null)
                    dataGridView1.Columns[selectedNode.Text].Visible = false;

                dataGridView1.Columns[treeView1.SelectedNode.Text].Visible = true;
            }
            //oldSelectedNode = selectedNode;

            if(dataGridView1.Columns[treeView1.SelectedNode.Text] != null)
                selectedNode = treeView1.SelectedNode;
        }

        protected void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete) return;

            TreeNode nodeToDelete = treeView1.SelectedNode;
            TreeNode nextNode = treeView1.SelectedNode.PrevNode != null ? treeView1.SelectedNode.PrevNode : treeView1.SelectedNode.Parent;

            treeView1.SelectedNode = nextNode;

            if(parsedData.Columns[nodeToDelete.Text] != null)
            {
                //selectedNode = treeView1.SelectedNode.Parent;
               // dataGridView1.Columns[treeView1.SelectedNode.Text].Visible = false;
                parsedData.Columns.Remove(nodeToDelete.Text);
            }
            treeView1.Nodes.Remove(nodeToDelete);

            //selectedNode = null;
        }
        private void DataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            contextMenuStrip2.Show(dataGridView1, e.X, e.Y);
        }
            protected void treeView1_NodeMouseClick(object sender,
    TreeNodeMouseClickEventArgs e)
        {
            TreeNode curNode = treeView1.GetNodeAt(e.X,e.Y);

            if (e.Button == MouseButtons.Left && curNode == selectedNode)
            {
                curNode.BeginEdit();
            }

            treeView1.SelectedNode = curNode;

            
            if (e.Button != MouseButtons.Right) return;


            contextMenuStrip1.Show(treeView1, e.X,e.Y);
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

        // Load a TreeView control from an XML file.
        private void LoadTreeViewFromXmlFile()
        {
            // Load the XML document.
            XmlDocument xml_doc = new XmlDocument();
            xml_doc.Load("treestructure.xml");

            // Add the root node's children to the TreeView.
            treeView1.Nodes.Clear();
            //treeView1.Nodes.Add(new
            //    TreeNode(xml_doc.DocumentElement.Name));
            TreeNode parent = treeView1.Nodes.Add(xml_doc.FirstChild.Name);
            AddTreeViewChildNodes(parent.Nodes, xml_doc.DocumentElement);

            treeView1.Sort();
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
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
            parsedData.WriteXml("DataXML.xml", XmlWriteMode.WriteSchema);
        }

        public Form1()
        {
            InitializeComponent();
            LoadTreeViewFromXmlFile();
            parsedData.ReadXml("DataXML.xml");
            dataGridView1.DataSource = parsedData;

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Visible = false;
            }
        }

        private void treeView1_AfterLabelEdit(object sender,
         System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            if (e.Label != null && parsedData.Columns[e.Label] == null)
            {
                parsedData.Columns.Add(e.Label);
                dataGridView1.Columns[e.Label].Visible = false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode newChild = treeView1.SelectedNode.Nodes.Add("New");
            treeView1.SelectedNode.Expand();
            newChild.BeginEdit();
        }

        private void newToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string clipData = Clipboard.GetText();
            string[] clipdatas = clipData.Split('\n');
            if(clipdatas[clipdatas.Length-1] == "")
            {
                clipdatas = clipdatas.Take(clipdatas.Count() - 1).ToArray();
            }

            int cellRow = dataGridView1.SelectedCells[0].RowIndex-1;
            int cellColumn = dataGridView1.SelectedCells[0].ColumnIndex;

            foreach (string clip in clipdatas)
            {
                if (cellRow + 1 >= parsedData.Rows.Count)
                {
                    DataRow row = parsedData.NewRow();
                    parsedData.Rows.Add(row);
                    //dataGridView1.Rows.Add(row);
                    //parsedData.AcceptChanges();
                    //dataGridView1.DataSource = parsedData;

                }
                cellRow = cellRow + 1;

                parsedData.Rows[cellRow][cellColumn] = clip;
            }

            parsedData.AcceptChanges();

            //DataGridViewCell cell = dataGridView1.SelectedCells[0];
            //int index = 0;
            //foreach(string clip in clipdatas)
            //{
            //    cell.Value = clip;

            //    if (cell.RowIndex + 1 >= parsedData.Rows.Count)
            //    {
            //        DataRow row = parsedData.NewRow();
            //        parsedData.Rows.Add(row);
            //        //dataGridView1.Rows.Add(row);
            //        //parsedData.AcceptChanges();
            //        //dataGridView1.DataSource = parsedData;
                    
            //    }

            //    cell = dataGridView1.Rows[cell.RowIndex+1].Cells[cell.ColumnIndex];
            //}
        }
    }
}
