using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            BOTAO.Clicked += BuscarCEP;

		}

        private void BuscarCEP(object sender, EventArgs args)
        {
            //TODO - Validações
            string cep = CEP.Text.Trim();
            if (isValidCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCep(cep);
                    if (end != null)
                    {
                        RESULTADO.Text = $"Endereço: {end.uf}-{end.localidade}, {end.bairro} - {end.logradouro} ";
                    }
                    else
                        DisplayAlert("ERRO", $"Endereço não encontrado para o cep {cep}", "Ok");
                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "Ok");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;
            if(cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "Ok");
                valido = false;
            }
            int NovoCep = 0;
            if(!int.TryParse(cep, out NovoCep))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter apenas números.", "Ok");
                valido = false;
            }
            return valido;
        }

	}
}
