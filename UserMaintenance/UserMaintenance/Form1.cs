﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            labelLastname.Text = Resource1.LastName;
            labelFirstname.Text = Resource1.FirstName;
            btnAdd.Text = Resource1.Add;
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                LastName = textBox1.Text,
                FirstName=textBox2.Text
            };
            users.Add(u);
        }
    }
}
