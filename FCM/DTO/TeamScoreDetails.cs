using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Shapes;
using FCM.DAO;
using FCM.DTO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace FCM.DTO
{
    class TeamScoreDetails
    {
        public int rankTeam { get; set; }
        public string nameTeam { get; set; }
        public int m { get; set; }
        public int w { get; set; }
        public int d { get; set; }
        public int l { get; set; }
        public int f { get; set; }
        public int a { get; set; }
        public int pts { get; set; }
        public int gD { get; set; }
        public string fLM { get; set; }
        public BitmapImage logo { get; set; }
        public BitmapImage imageFLM { get; set; }

        public TeamScoreDetails(string teamName, BitmapImage lg)
        {
            this.rankTeam = 0;
            this.nameTeam = teamName;
            this.logo = lg;
            this.m = 0;
            this.w = 0;
            this.d = 0;
            this.l = 0;
            this.f = 0;
            this.a = 0;
            this.gD = 0;
            this.pts = 0;
            this.fLM = "";
        }
        public void CalcDetails(int gf, int ga)
        {
            
            this.m++;
            this.f += gf;
            this.a += ga;
            this.gD = this.gD + gf - ga;

            if (this.fLM.Length > 4)
            {
                this.fLM = this.fLM.Remove(0, 1);
            }

            if (gf > ga)
            {
                this.w++;
                this.fLM += 'V';
            }

            if (gf == ga)
            {
                this.d++;
                this.fLM += '-';
            }

            if (gf < ga)
            {
                this.l++;
                this.fLM += 'X';
            }
        }

    }
}
