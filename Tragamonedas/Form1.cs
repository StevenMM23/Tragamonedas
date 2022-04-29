using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using Tragamonedas.Properties;

namespace Tragamonedas
{
    public partial class Form1 : Form
    {
        private readonly List<PictureBox> GrupoDeFichasUno = new();

        private readonly List<PictureBox> GrupoDeFichasDos = new();

        private readonly List<PictureBox> GrupoDeFichasTres = new();

        private readonly List<PictureBox> GrupoDeFichasCuatro = new();

        private readonly List<PictureBox> GrupoDeFichasCinco = new();

        private int MovimientoY, PosicionSiguiente;
        private readonly int posicionEjeX = 3;
        private bool indicador1;
        private bool indicador2;
        private bool indicador3;
        private bool indicador4;
        private bool indicador5;
        private string nombreFicha1 = "";
        private string nombreFicha2 = "";
        private string nombreFicha3 = "";
        private string nombreFicha4 = "";
        private string nombreFicha5 = "";

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public List<PictureBox> CrearImagenes(List<PictureBox> Lista, Panel panel, string NombreTag)
        {
            var random = new Random();
            var LongitudFicha = 100;
            for (var i = 0; i < 20; i++)
            {
                var pb = new PictureBox();
                pb.Image = (Bitmap) Resources.ResourceManager.GetObject("icons" + random.Next(1,8), Resources.Culture);
                pb.Size = new Size(LongitudFicha, LongitudFicha);
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.Name = string.Format("{0}", i + NombreTag);
                pb.Tag = NombreTag + "_" + i;
                var Mov = pb.Location.Y;
                pb.Location = new Point(posicionEjeX, i * LongitudFicha);
                Lista.Add(pb);
                panel.Controls.Add(Lista[i]);
            }

            return Lista;
        }


        public Form1()
        {
            InitializeComponent();
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
            timer5.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            nombreFicha2 = GirarFichas(GrupoDeFichasDos, panel2, timer2, indicador2);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            nombreFicha3 = GirarFichas(GrupoDeFichasTres, panel3, timer3, indicador3);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            nombreFicha4 = GirarFichas(GrupoDeFichasCuatro, panel4, timer4, indicador4);
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            nombreFicha5 = GirarFichas(GrupoDeFichasCinco, panel5, timer5, indicador5);

            if (nombreFicha5 != "")
            {
                if (NumeroFicha(nombreFicha1) == NumeroFicha(nombreFicha2) &&
                    NumeroFicha(nombreFicha2) == NumeroFicha(nombreFicha3) &&
                    NumeroFicha(nombreFicha3) == NumeroFicha(nombreFicha4) &&
                    NumeroFicha(nombreFicha4) == NumeroFicha(nombreFicha5))
                {
                    var form2 = new Form2();
                    form2.Show();
                    this.Visible = false;
                }
                else
                {
                    var form3 = new GameOver();
                    form3.Show();
                    this.Visible = false;
                }
            }
            

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            if (timer2.Enabled)
            {
                indicador2 = true;
                timer3.Start();
                pictureBox2.Image = Resources._1200px_Button_Icon_Green_svg;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            if (timer3.Enabled)
            {
                indicador3 = true;
                timer4.Start();
                pictureBox3.Image = Resources._1200px_Button_Icon_Green_svg;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            
            if (timer4.Enabled)
            {
                indicador4 = true;
                timer5.Start();
                pictureBox4.Image = Resources._1200px_Button_Icon_Green_svg;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
            if (timer5.Enabled)
            {
                indicador5 = true;
                pictureBox5.Image = Resources._1200px_Button_Icon_Green_svg;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            if (timer1.Enabled)
            {
                pictureBox7.Enabled = false;
                indicador1 = true;
                timer2.Start();
                pictureBox1.Image = Resources._1200px_Button_Icon_Green_svg;
                
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            indicador1 = false;
            indicador2 = false;
            indicador3 = false;
            indicador4 = false;
            indicador5 = false;
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetImages();
        }

        public void ResetImages()
        {
            CrearImagenes(GrupoDeFichasUno, panel1, "Uno");
            CrearImagenes(GrupoDeFichasDos, panel2, "Dos");
            CrearImagenes(GrupoDeFichasTres, panel3, "Tres");
            CrearImagenes(GrupoDeFichasCuatro, panel4, "Cuatro");
            CrearImagenes(GrupoDeFichasCinco, panel5, "Cinco");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nombreFicha1 = GirarFichas(GrupoDeFichasUno, panel1, timer1, indicador1);
        }


        public string GirarFichas(List<PictureBox> Lista, Panel panel, Timer timer, bool bandera)
        {
            var FichaSeleccionada = "";
            for (var i = 0; i < Lista.Count; i++)
            {
                MovimientoY = Lista[i].Location.Y;
                Lista[i].Location = new Point(posicionEjeX, MovimientoY - 10);
                if (Lista[i].Location.Y <= -100)
                {
                    PosicionSiguiente = Lista[Lista.Count - 1].Location.Y;
                    Lista[i].Location = new Point(posicionEjeX, PosicionSiguiente + 100);
                    Lista.Add(Lista[i]);
                    panel.Controls.Add(Lista[i]);
                    Lista.RemoveAt(i);
                }

                if (bandera)
                {
                    if (Lista[i].Location.Y == -10)
                    {
                        timer.Stop();
                        FichaSeleccionada = Lista[i].Tag.ToString();
                    }
                }
                else
                {
                    FichaSeleccionada = "";
                }
            }

            return FichaSeleccionada;
        }

        public int NumeroFicha(string Nombre)
        {
            var Posicion = Nombre.Split("_".ToCharArray());
            return Convert.ToInt32(Posicion[1]);
        }

        

    }
}