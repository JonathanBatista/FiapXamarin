﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF.Recursos
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        #region Controles
        private async void btnEditor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Controles.EditorView());
        }
        private async void btnPicker_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Controles.ListPickerView());
        }
        private async void btnData_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Controles.PickerView());
        }
        private async void btnProgresso_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Controles.ProgressoView());
        }
        private async void btnStepper_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Controles.StepperView());
        }
        #endregion

        #region Estilo
        private async void btnSimples_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Estilo.SimplesView());
        }
        private async void btnGlobal_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Estilo.GeralView());
        }
        private async void btnDinamico_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Estilo.DinamicoView());
        }
        private async void btnTriggers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Estilo.TriggersView());
        }
        #endregion

        #region Lista
        private async void btnListSimples_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista.SimplesView());
        }
        private async void btnListClasse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista.ClasseView());
        }
        private async void btnProduto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista.ProdutoView());
        }
        private async void btnListaCards_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista.ListaCards());
        }
        private async void btnEtiqueta_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Lista.ListaEtiquetaView());
        }
        #endregion

        #region Menu
        private async void btnMestre_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu.MainPage());
        }

        #endregion

        #region Theme
        private async void btnTemplate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Theme.ThemeView());
        }
        #endregion

        #region PassParameter
        private async void btnHome_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PassParameter.HomeView(DateTime.Now.ToString("u")));
        }


        private async void btnMC_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PassParameter.MCHomeView());
        }
        #endregion

        #region Custom Renderer

        private async void btnCustom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomControl.CustomView());
        }

        #endregion

        #region AppResources
        private async void btnLigar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new API.PhoneView());
        }

        private async void btnGps_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GPS.CoordenadaView());
        }
        #endregion


        
    }
}
