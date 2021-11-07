﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace BasicFacebookFeatures
{
    public partial class LoginForm : Form
    {
        private int k_CollectionLimit = 50;
        private LoggedUserForm m_MainForm;
        private readonly MyFacebookService k_MyFBServices;
        public LoginForm()
        {
           // initTablePanel(3);
            InitializeComponent();

           
            myInitComponent();

            k_MyFBServices = new MyFacebookService();
            FacebookWrapper.FacebookService.s_CollectionLimit = k_CollectionLimit;

       //     HorizontalBox box = new HorizontalBox(1);
         //   this.Controls.Add(box);
        }

        private void initTablePanel(int i_ColumnCount)
        {
            // this.hBox.Anchor = AnchorStyles.Right;
            // this.hBox.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            // this.hBox.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
            // this.hBox.AutoSize = true;
            // this.hBox.Location = new Point(400, 400);


            // hBox.RowCount = 1;
            // hBox.ColumnCount = i_ColumnCount;

            HorizontalBox hbox = new HorizontalBox(3);
            hbox.AllowDrop = true;
            //hbox.AutoScroll = true;
            hbox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            hbox.AutoSize = true;
          //  hbox.MaximumSize = new Size(1000, 1000);

            Label labelTest1 = new Label();
            labelTest1.Text = "test1";

            Label labelTest2 = new Label();
            labelTest2.Text = "test2";

            Label labelTest3 = new Label();
            labelTest3.Text = "test3";

            // hBox.SetRow(labelTest1,0);
            // hBox.SetRow(labelTest2, 1);
            // hBox.SetRow(labelTest3, 2);
            
            hbox.Controls.Add(labelTest1);
            hbox.Controls.Add(labelTest3);
            hbox.Controls.Add(labelTest2);

            this.Controls.Add(hbox);
        }

        private void myInitComponent()
        {
            this.Size = new Size(1200, 700);
            
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("design.patterns20cc"); /// the current password for Desig Patter
            k_MyFBServices.LogManager.ActionsList.Add(new FaceBookAction(DateTime.Now, false, FaceBookAction.ActionType.LOGIN_CLICKED));

            FacebookWrapper.LoginResult loginResult = FacebookService.Login(
                    "162373509365377",
                    "email",
                    "public_profile",
                    "user_age_range",
                    "user_birthday",
                    "user_events",
                    "user_friends",
                    "user_gender",
                    "user_hometown",
                    "user_likes",
                    "user_link",
                    "user_location",
                    "user_photos",
                    "user_posts",
                    "user_videos",

                    "pages_manage_posts", 
                    "pages_read_engagement", 
                    "pages_manage_posts",

                    "publish_to_groups"
                  //  "user_messenger_contact"
                    // "publish_pages"- doesn't work
                    //  "can_post" taken from developers.facebook.com - should work but doesn't.
                    );

            if (!string.IsNullOrEmpty(loginResult.AccessToken))
            {
                k_MyFBServices.Init(loginResult);
                m_MainForm = new LoggedUserForm(k_MyFBServices);
                this.Hide();
                m_MainForm.Closed += mainForm_Closed;
                m_MainForm.Show();
            }
            else
            {
                MessageBox.Show(loginResult.ErrorMessage, "Login Failed");
            }
        }

        private void mainForm_Closed(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}