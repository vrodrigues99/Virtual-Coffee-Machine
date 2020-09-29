using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualCoffeMachine
{
    public partial class Menu : Form
    {
        //VARIAVEIS GLOBAIS
        private double SaldoTotal = 0;
        private enum TiposCafe { Café, Cappuccino, Mocha, CaféLeite }
        private DialogResult message;
        public Menu()
        {
            //INICIALIZA COMPONENTES DO MENU
            InitializeComponent();

            LblSaldo.Text = "Saldo: " + SaldoTotal.ToString("C");

            //DEFICINICAO DE ACTION PARA BOTOES MOEDA
            btn1centavo.Click += btnsMoeda_Click;
            btn5centavos.Click += btnsMoeda_Click;
            btn10centavos.Click += btnsMoeda_Click;
            btn25centavos.Click += btnsMoeda_Click;
            btn50centavos.Click += btnsMoeda_Click;
            btn1real.Click += btnsMoeda_Click;

            //DEFINICAO DE ACTION PARA BOTOES CAFE
            btnCafe.Click += btnsCafe_Click;
            btnCappuccino.Click += btnsCafe_Click;
            btnMocha.Click += btnsCafe_Click;
            btnCafeLeite.Click += btnsCafe_Click;
        }

        //METODO ACTION BOTOES CAFES
        private void btnsCafe_Click(object sender, EventArgs e)
        {
            //CONVERTE O SENDER PARA BUTTON CAFE
            Button btnsCafe = sender as Button;

            //PERCORE OS TIPOS DE BOTOES DE CAFE
            switch (btnsCafe.Name)
            {
                case "btnCafe": ComprarCafe(TiposCafe.Café); break;
                case "btnCappuccino": ComprarCafe(TiposCafe.Cappuccino); break;
                case "btnMocha": ComprarCafe(TiposCafe.Mocha); break;
                case "btnCafeLeite": ComprarCafe(TiposCafe.CaféLeite); break;
            }
        }

        //METODO ACTION BOTOES MOEDA
        private void btnsMoeda_Click(object sender, EventArgs e)
        {
            //CONVERTE O SENDER PARA BUTTON MOEDA
            Button btnsMoeda = sender as Button;

            //PERCORE OS TIPOS DE BOTOES DE MOEDAS
            switch (btnsMoeda.Name)
            {
                //MOEDA 1 CENTAVO
                case "btn1centavo": AdicionarSaldo(0.01); break;
                
                //MOEDA 5 CENTAVOS
                case "btn5centavos": AdicionarSaldo(0.05); break;

                //MOEDA 10 CENTAVOS
                case "btn10centavos": AdicionarSaldo(0.1); break;

                //MOEDA 25 CENTAVOS
                case "btn25centavos": AdicionarSaldo(0.25); break;

                //MOEDA 50 CENTAVOS
                case "btn50centavos": AdicionarSaldo(Math.Round(0.5, 2)); break;

                //MOEDA 1 REAL
                case "btn1real": AdicionarSaldo(1); break;
            }
            
        }

        //METODO PARA COMPRAR CAFE NA MAQUINA
        private void ComprarCafe(TiposCafe tipo)
        {
            //PERCORRE OS TIPOS DE CAFE
            switch (tipo) 
            {
                //CAFE
                case TiposCafe.Café: ChecarSaldo(2); break;

                //CAPPUCCINO
                case TiposCafe.Cappuccino: ChecarSaldo(3.5); break;

                //MOCHA
                case TiposCafe.Mocha: ChecarSaldo(4); break;

                //CAFELEITE
                case TiposCafe.CaféLeite: ChecarSaldo(3); break;
            }
        }

        //METODO PARA CHECAR O SALDO DO CLIENTE
        private void ChecarSaldo(double preco)
        {
            //CHECA SE HA SALDO SUFICIENTE
            if(preco <= SaldoTotal)
            {
                //CASO HAJA TROCO
                if (!(preco == Math.Round(SaldoTotal, 2)))
                {
                    message = MessageBox.Show("Compra Confirmada! Retire o Troco: " + (SaldoTotal - preco).ToString("C"), "Compra Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZerarSaldo();
                }
                //CASO NAO HAJA TROCO
                else
                {
                    message = MessageBox.Show("Compra Confirmada! Retire seu café.", "Compra Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ZerarSaldo();
                }
                    
            }
            //CASO NAO HAJA SALDO SUFICIENTE
            else
            {
                message = MessageBox.Show("Seu saldo não é suficiente. Favor inserir " + Math.Abs(SaldoTotal-preco).ToString("C"), "Compra Rejeitada", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //METODO PARA ADICIONAR SALDO NA MAQUINA
        private void AdicionarSaldo(double valor)
        {
            //CHECA CASO SEJA MOEDA DE 1C OU 5C E REJEITA
            if(valor.Equals(0.01) || valor.Equals(0.05))
            {
                message = MessageBox.Show("Moeda Rejeitada!", "Moeda Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //ADICIONA MOEDA AO VALOR TOTAL
            SaldoTotal += valor;
            Math.Round(SaldoTotal, 2);
            LblSaldo.Text = "Saldo: " + SaldoTotal.ToString("C");
        }

        //METODO PARA O BOTAO CANCELAR
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            message = MessageBox.Show("Retire seu dinheiro!", "Compra Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.Exit();
        }

        private void ZerarSaldo()
        {
            SaldoTotal = 0;
            LblSaldo.Text = "Saldo: " + SaldoTotal.ToString("C");
        }
    }
}
