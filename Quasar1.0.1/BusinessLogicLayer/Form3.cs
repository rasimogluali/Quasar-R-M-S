﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace Quasar1._0._1.BusinessLogicLayer
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'quasarDataSet5.masa3' table. You can move, or remove it, as needed.
            this.masa3TableAdapter.Fill(this.quasarDataSet5.masa3);
            oc = new OleDbCommand("select qida_id, qida_adi,qiymet from qida_elave_et where alt_qida='true'", con);
            con.Open();
            odr = oc.ExecuteReader();
            while (odr.Read())
            {
                comboBox1.Items.Add(odr.GetValue(0) + ")" + odr.GetValue(1) + "_" + odr.GetValue(2));
            }

            oc = new OleDbCommand("select * from masa3", con);
            oc.CommandType = CommandType.Text;
            oda = new OleDbDataAdapter(oc);
            ocb = new OleDbCommandBuilder(oda);
            ds = new DataSet();
            oda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
         
        }
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ferhan\Desktop\quasar.accdb");
        OleDbCommand oc;
        OleDbDataReader odr;
        OleDbDataAdapter oda;
        DataTable dt;
        DataSet ds;
        OleDbCommandBuilder ocb;
        int a;

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                string b1 = comboBox1.Text.Substring(0, comboBox1.Text.IndexOf(")"));
                string b2 = comboBox1.Text.Substring(comboBox1.Text.IndexOf(")") + 1, comboBox1.Text.IndexOf("_") - 3);
                string b3 = comboBox1.Text.Substring(comboBox1.Text.IndexOf("_") + 1);
                a++;
                label1.Text = a.ToString();
                oc = new OleDbCommand("insert into masa3 (m_id,qida_id,qida_adi,qiymet)  values (" + Int32.Parse(label1.Text) + " , " + Int32.Parse(b1)
                   + " , '" + b2 + "'," + double.Parse(b3) + " )", con);
                con.Open();
                oc.ExecuteNonQuery();
                con.Close();

                con.Open();
                oc = new OleDbCommand("select * from masa3", con);
                oc.CommandType = CommandType.Text;
                oda = new OleDbDataAdapter(oc);
                ocb = new OleDbCommandBuilder(oda);
                ds = new DataSet();
                oda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                con.Close();
                con.Close();
            }
            catch (Exception ex) { label1.Text = ex.Message; }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                string a = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                oc = new OleDbCommand("delete from masa3 where m_id=" + a, con);
                con.Open();
                oc.ExecuteNonQuery();
                con.Close();

                foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }


            }
            catch (Exception ex) { label1.Text = ex.Message; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                oc = new OleDbCommand("insert into satislar (qida_id,qida_adi,qiymet,tarix)  select qida_id,qida_adi,qiymet,tarix from masa3", con);
                con.Open();
                oc.ExecuteNonQuery();
                con.Close();
                oc = new OleDbCommand("delete * from masa3", con);
                con.Open();
                oc.ExecuteNonQuery();
                con.Close();
                this.Close();
            }
            catch (Exception ex) { label1.Text = ex.Message; }
        }
    }
}
