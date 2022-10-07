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
using WMPLib;

namespace LessMusicPlayer
{
    public partial class Form1 : Form
    {
        List<string> urlList = new List<string>();
        double max, min;
        bool list = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            of.Title = "请选择需要播放的文件";
            of.Filter = "（*.mp3）|*.mp3|（*.wav）|*.wav|（*.flac）|*.flac|（*.ogg）|*.ogg|（所有文件）|*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                string[] nameList = of.FileNames;
                foreach(string url in nameList)
                {                    
                    listBoxMusic.Items.Add(Path.GetFileNameWithoutExtension(url));
                    urlList.Add(url);
                }
            };
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxMusic.SelectedIndex;
            if(selectedIndex < 0)
            {
                selectedIndex = selectedIndex < 0 ? 0 : selectedIndex; ;
                //selectedIndex = selectedIndex < 0 ? listBoxMusic.Items.Count - 1 : selectedIndex;
                listBoxMusic.SelectedIndex = selectedIndex;

                axWindowsMediaPlayer1.URL = urlList[selectedIndex];
                lblMusicName.Text = listBoxMusic.SelectedItem.ToString();
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            timer1.Enabled = true;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            timer1.Enabled = false;
        }

        private void btnPrior_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxMusic.SelectedIndex - 1;
            selectedIndex = selectedIndex < 0 ? listBoxMusic.Items.Count - 1 : selectedIndex;
            listBoxMusic.SelectedIndex = selectedIndex;
            axWindowsMediaPlayer1.URL = urlList[selectedIndex];
            lblMusicName.Text = listBoxMusic.SelectedItem.ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBoxMusic.SelectedIndex + 1;
            selectedIndex = selectedIndex == listBoxMusic.Items.Count ? 0 : selectedIndex;
            listBoxMusic.SelectedIndex = selectedIndex;
            axWindowsMediaPlayer1.URL = urlList[selectedIndex];
            lblMusicName.Text = listBoxMusic.SelectedItem.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            max = axWindowsMediaPlayer1.currentMedia.duration;
            min = axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

            trackBar1.Maximum = (int)(max);
            trackBar1.Value = (int)(min);

            if(axWindowsMediaPlayer1.playState == WMPPlayState.wmppsStopped)
            {
                list = true;
                int seletedIdex = listBoxMusic.SelectedIndex + 1;
                seletedIdex = seletedIdex == listBoxMusic.Items.Count ? 0 : seletedIdex;
                axWindowsMediaPlayer1.URL = urlList[seletedIdex];
                listBoxMusic.SelectedIndex = seletedIdex;
                lblMusicName.Text = listBoxMusic.SelectedItem.ToString();
                trackBar1.Value = 0;
                timer1.Enabled = true;
                
            }
            list = false;
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            axWindowsMediaPlayer1.Ctlcontrols.pause();
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            double doValue = trackBar1.Value;
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = doValue;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            timer1.Enabled = true;
        }

        private void listBoxMusic_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBoxMusic.SelectedIndex;
            axWindowsMediaPlayer1.URL = urlList[selectedIndex];
            lblMusicName.Text = listBoxMusic.SelectedItem.ToString();
            if(list == true)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            if(list == false)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
            
        }
    }
}
