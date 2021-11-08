﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace BasicFacebookFeatures
{
    public partial class SettingsForm : Form
    {
        private LogManager k_LogManager;
        private MyFacebookService k_MyFacebookService;

        public SettingsForm(MyFacebookService i_MyFacebookService)
        {
            k_LogManager = i_MyFacebookService.LogManager;
            k_MyFacebookService = i_MyFacebookService;
            
            //k_LogManager.ActionsList.Add(new FaceBookAction(FaceBookAction.ActionType.SETTINGS_CLICKED));
          
            InitializeComponent();
            myInitComponents();
        }
         
        //TODO: condition to if was today show time and write today, otherwise write date.
        private void myInitComponents()
        {
            tableLayoutRecentActions.RowCount = k_LogManager.ActionsList.Count;
            foreach (FaceBookAction fbAction in k_LogManager.ActionsList)
            {
                Label labelTime = new Label();
                labelTime.Text = fbAction.Time.TimeOfDay.ToString();

                Label labelType = new Label();
                labelType.Text = fbAction.Type.ToString();

                Label labelErrorStatus = new Label();
                labelErrorStatus.Text = fbAction.ErrorStatus.ToString();

                tableLayoutRecentActions.Controls.Add(labelTime);
                tableLayoutRecentActions.Controls.Add(labelType);
                tableLayoutRecentActions.Controls.Add(labelErrorStatus);

            }

        }

        private void friendsTab_Selected(object sender, TabControlEventArgs e)
        {
            loadPotentialFriends();
        }

        private void loadPotentialFriends()
        {
            int currentX = 40;
            int currentY = 50;

            this.Size = new Size(1200, 700);
            this.AutoScroll = true;
            foreach (User suggestion in k_MyFacebookService.GetFriendSuggestions().Take(10))
            {
                Button pictureBox = new Button();

                pictureBox.Image = suggestion.ImageNormal;
               // pictureBox.Image. = PictureBoxSizeMode.Zoom;
                pictureBox.Size = new Size(300, 300);
                pictureBox.Text = suggestion.Name;
                pictureBox.Location = new Point(currentX, currentY);

                if (currentX + 300 > this.Width)
                {
                    currentX = 40;
                    currentY += 300;
                }
                else
                {
                    currentX += 300;
                }
                this.Controls.Add(pictureBox);
            }
           
        }

        private void statisticsTab_Selected(object sender, TabControlEventArgs e)
        {
            loadStatistics();
        }

        private void loadStatistics()
        {
            foreach(FaceBookAction.ActionType type in Enum.GetValues(typeof(FaceBookAction.ActionType)))
            {
                this.actionTypeChart.Series["Activity Type"].Points.AddXY(type.ToString(), k_LogManager.GetActivityCountByType(type));
            }
        }
    }
}
