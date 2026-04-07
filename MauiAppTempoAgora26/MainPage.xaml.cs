using MauiAppTempoAgora26.Models;
using MauiAppTempoAgora26.Services;

namespace MauiAppTempoAgora26
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Previsao(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Localização:\n" +
                                         $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n\n" +
                                         $"Clima:\n" +
                                         $"{t.main}  - {t.description} \n\n" +
                                         $"Temperatura:\n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n\n" +
                                         $"Visibilidade:\n" +
                                         $"{t.visibility} metros \n\n" +
                                         $"Sol:\n" +
                                         $"Nascer do Sol: {t.sunrise} \n" +
                                         $"Por do Sol: {t.sunset} \n";
                                         

                        lbl_res.Text = dados_previsao;

                          
                    }
                    else
                    {

                        lbl_res.Text = "Sem dados de Previsão";
                    }

                }
                else
                {
                    lbl_res.Text = "Preencha a cidade.";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        
    }
}

