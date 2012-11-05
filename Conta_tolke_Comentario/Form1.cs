using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Conta_tolke_Comentario
{
    public partial class Form1 : Form
    {
        string[] Linhas;
        OpenFileDialog open = new OpenFileDialog();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            open.Multiselect = true;
            open.Title = "Selecione o arquivo.";
            //open.InitialDirectory = @"C:\Documents and Settings\luishr\Meus documentos\Visual Studio 2010\Projects\BDAutoPecas2\Camadas";
            open.Filter = "Arquivo de texto (*.txt)|*.txt|Arquivo C# (*.cs)|*.cs|Arquivo Dat (*.dat)|*.dat|Todos os Arquivos (*.*)|*.*";
            open.ShowDialog();
            tb_caminho.Text = open.FileName.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int contaComentario = 0;
            int contaCodigo = 0;
            int contaLinhaVazia = 0;
            rtb_ContLinhas.Clear();
            try
            {
                foreach (String Arquivo in open.FileNames)
                {
                    
                    //rtb_ContLinhas.Clear();

                    Linhas = File.ReadAllLines(Arquivo, Encoding.Default);

                    tb_caminho.Text = open.FileName.ToString();

                    for (int i = 0; i < Linhas.Length; i++)
                    {
                        if (progressBar.Value < 100)
                        {
                            progressBar.Value += 2;
                        }
                        
                        string aux = Linhas[i];
                        int contbarra = 0;
                        int contespaco = 0;
                        for (int j = 0; j < aux.Length; j++)
                        {
                            decimal percent = aux.Length / 100;
                            if (aux[j].Equals('/'))
                                contbarra++;
                            if (aux[j].Equals('*'))
                            {
                                if (aux[j + 1].Equals('/') || aux[j - 1].Equals('/'))
                                    contbarra++;
                            }                            
                            if (Convert.ToInt16(aux[j]) == 32)
                                contespaco++;
                        }

                        if (aux.Length == contespaco)
                            contaLinhaVazia++;
                        else if (contbarra > 1)
                            contaComentario++;
                        else
                            contaCodigo++;
                    }
                    progressBar.Value = 0;
                    rtb_ContLinhas.Text += Arquivo;
                    rtb_ContLinhas.Text += "\nLinhas de Comentarios  =  " + contaComentario;
                    rtb_ContLinhas.Text += "\nLinhas de Códigos        =  " + contaCodigo;
                    rtb_ContLinhas.Text += "\nLinhas Vazias              =  " + contaLinhaVazia;
                    rtb_ContLinhas.Text += "\n===========================";
                }
                rtb_ContLinhas.Text += "\n         TOTAL GERAL";
                rtb_ContLinhas.Text += "\n===========================";
                rtb_ContLinhas.Text += "\nLinhas de Comentarios  =  " + contaComentario;
                rtb_ContLinhas.Text += "\nLinhas de Códigos        =  " + contaCodigo;
                rtb_ContLinhas.Text += "\nLinhas Vazias              =  " + contaLinhaVazia;
                rtb_ContLinhas.Text += "\n===========================";
                progressBar.Value = 100;
                float total = contaCodigo + contaComentario + contaLinhaVazia;
                rtb_ContLinhas.Text += "\nTotal de Linhas  =  " + (total*100);                
                float perCodigos = contaCodigo/total;
                rtb_ContLinhas.Text += "\n% Código          =  " + (perCodigos*100);
                float perComent = contaComentario / total;
                rtb_ContLinhas.Text += "\n% Comentários   =  " + (perComent*100);
                







            }        
            catch (Exception err)
            {

                MessageBox.Show(err.ToString(), "Erro na leitura do arquivo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }
    }
}
