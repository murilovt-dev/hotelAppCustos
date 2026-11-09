
using MauiAppMeuCombustivel.Models;
using MauiAppMeuCombustivel.Views;

namespace MauiAppMeuCombustivel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    App PropriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        PropriedadesApp = (App)Application.Current!;

        pck_quarto.ItemsSource = PropriedadesApp.lista_quartos;

        dtpck_checkin.MinimumDate = DateTime.Now;

        dtpck_checkin.MaximumDate = DateTime.Now.AddMonths(1);

        DateTime dataCheckin = dtpck_checkin.Date ?? DateTime.Now;

        dtpck_checkout.MinimumDate = dataCheckin.AddDays(1);
        dtpck_checkout.MaximumDate = dataCheckin.AddMonths(6);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            Hospedagem h = new Hospedagem
            {
                QuartoSelecionado = (Quarto)pck_quarto.SelectedItem,
                QntAdultos = Convert.ToInt32(stp_adultos.Value),
                QntCriancas = Convert.ToInt32(stp_criancas.Value),
                DataCheckIn = dtpck_checkin.Date ?? DateTime.Now,
                DataCheckOut = dtpck_checkout.Date ?? DateTime.Now,
            };

            await Navigation.PushAsync(new HospedagemContratada()
            {
                BindingContext = h
            });
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (sender is DatePicker elemento && elemento.Date.HasValue)
        {
            DateTime dataSelecionadaCheckin = elemento.Date.Value;

            dtpck_checkout.MinimumDate = dataSelecionadaCheckin.AddDays(1);
            dtpck_checkout.MaximumDate = dataSelecionadaCheckin.AddMonths(6);
        }
    }

    private async void BtnSobre_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Sobre());
    }
}
