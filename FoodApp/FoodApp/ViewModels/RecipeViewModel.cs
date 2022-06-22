using FoodApp.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FoodApp.ViewModels
{
	[QueryProperty(name: "IdReceta", queryId: "id")]
	internal class RecipeViewModel : BaseViewModel
	{
		public Command OnHeartClickedCommand { get; set; }

		private string idReceta;
		private Receta receta;
		private string corazon;
		private int corazones;
		List<Step> steps;

		public string IdReceta
		{
			get => idReceta;
			set
			{
				SetProperty(ref idReceta, value);
				Receta = App.Recetas[int.Parse(value) - 1];
			}
		}
		public Receta Receta
		{
			get => receta;
			set
			{
				SetProperty(ref receta, value);
				Corazon = value.corazon;
				Corazones = value.corazones;
				Steps = FormatSteps();
			}
		}

		public string Corazon
		{
			get => corazon;
			set => SetProperty(ref corazon, value);
		}

		public int Corazones
		{
			get => corazones;
			set => SetProperty(ref corazones, value);
		}

		public List<Step> Steps
		{
			get => steps;
			set => SetProperty(ref steps, value);
		}

		public RecipeViewModel()
		{
			OnHeartClickedCommand = new Command(OnHeartClicked);
		}

		private async void OnHeartClicked(object obj)
		{
			string empty = "icon_heart.png";
			string filled = "icon_heart_filled.png";

			if (String.Equals(Receta.corazon, empty))
			{
				if (await App.restService.CreateFavorito(new Favorito() { idUsuario = App.loggedUser, idReceta = Receta.id }))
				{
					Receta.corazones++;
					Receta.corazon = filled;
					App.Recetas[Receta.id - 1] = Receta;
					Receta = App.Recetas[int.Parse(IdReceta) - 1];
				}
			}
			else
			{
				if (await App.restService.DeleteFavorito(new Favorito() { idUsuario = App.loggedUser, idReceta = Receta.id }))
				{
					Receta.corazones--;
					Receta.corazon = empty;
					App.Recetas[Receta.id - 1] = Receta;
					Receta = App.Recetas[int.Parse(IdReceta) - 1];
				}
			}
		}

		private List<Step> FormatSteps()
		{
			List<Step> list = new List<Step>();

			int i = 0;
			foreach (var step in Receta.pasos.Split('\n'))
			{
				list.Add(new Step(i++, step));
			}

			return list;
		}

	}

	public class Step
	{
		const string CircledNumbers = "❶❷❸❹❺❻❼❽❾❿";
		public char Number { get; set; }
		public string Text { get; set; }

		public Step(int number, string text)
		{
			Number = CircledNumbers[number];
			Text = text;
		}
	}
}
