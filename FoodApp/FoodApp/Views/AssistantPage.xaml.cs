using FoodApp.Models;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssistantPage : ContentPage
	{
		public static int subtitleSize;
		private Receta currentReceta;
		public Receta CurrentReceta
		{
			get => currentReceta;
			set
			{
				currentReceta = value;
				steps = CurrentReceta.pasos.Split('\n');
				currentStep = 0;
			}
		}
		private string[] steps;
		private int currentStep;

		private const string favoriteAdded = "%1 ha sido añadido a favoritos. La receta cuenta ahora con %2 favoritos.";
		private const string favoriteCantAdd = "No pudo realizar la acción porque la receta ya se encuentra en favoritos.";
		private const string favoriteCantRemove = "No pudo realizar la acción porque la receta no se encuentra en favoritos.";
		private const string favoriteEmpty = "Actualmente no tiene ninguna receta en favoritos.";
		private const string favoriteRemoved = "%1 ha sido eliminado de favoritos. La receta cuenta ahora con %2 favoritos.";
		private const string notRecognized = "Lo siento, no he podido entenderte.";
		private const string recipeFound = "He encontrado la receta de %s.\n\n¿Qué quiere hacer con ella?";
		private const string recipeMultipleResults = "He encontrado varias recetas que se ajustan a la búsqueda:\n\n%s";
		private const string recipeNotFound = "Lo siento, no he podido encontrar la receta que buscas, vuelve a intentarlo.";
		private const string recipeNotSelected = "No hay ningúna receta seleccionada, pruebe a buscar una.";
		private const string stepEnd = "Enhorabuena, ya ha completado el último paso y ha acabado la receta.\nBuen provecho.";
		private const string stepNotValid = "Lo siento, el paso número %1 no es válido. Esta receta solo tiene %2 pasos.";
		private const string stepRead = "Paso %1: %2";
		private const string stepStart = "No hay ningún paso anterior, ¿Quiere acaso salir del asistente?";
		private const string timeRead = "La receta requiere de aproximadamente %s.";
		private const string emptyHeart = "icon_heart.png";
		private const string filledHeart = "icon_heart_filled.png";

		private readonly StringCollection blackCollection;
		private readonly StringCollection searchCollection;
		private readonly StringCollection nextCollection;
		private readonly StringCollection previousCollection;
		private readonly StringCollection repeatCollection;
		private readonly StringCollection favoriteCollection;
		private readonly StringCollection descriptionCollection;
		private readonly StringCollection timeCollection;
		private readonly StringCollection ingredientsCollection;
		private readonly StringCollection stepCollection;
		private readonly StringCollection exitCollection;
		private readonly StringCollection addCollection;
		private readonly StringCollection removeCollection;
		private readonly StringCollection tipCollection;

		public AssistantPage()
		{
			InitializeComponent();

			(blackCollection = new StringCollection()).AddRange(new string[] { "a", "al", "con", "cosa", "cómo", "de", "debe", "deber", "debes", "debo", "decir",
				"decirme", "del", "digas", "dime", "dirigir", "dirígete", "el", "esa", "esas", "eso", "esos", "la", "lo", "mi", "mía", "mías", "mío", "míos",
				"no", "o", "otra", "otro", "poder", "por", "puede", "puedes", "puedo", "querer", "quiero", "se", "si", "sí", "un", "una", "ve", "vete" });

			(searchCollection = new StringCollection()).AddRange(new string[] { "busca", "buscame", "buscar", "cocina", "cocinar", "cocino", "comenzar",
				"comienza", "empezar", "empieza", "empiezo", "encontrar", "encuentra", "encuentrame", "enseña", "enseñame", "enseñar", "hace", "hacer",
				"hacerla", "hacerlo", "hago", "haría", "mostrar", "muestra", "muestrame", "receta" });

			(nextCollection = new StringCollection()).AddRange(new string[] { "adelante", "continuación", "continuar", "continúa", "pasa", "pasar", "sigue", "siguiente" });
			(previousCollection = new StringCollection()).AddRange(new string[] { "anterior", "atrás", "previo", "volver", "vuelve" });
			(repeatCollection = new StringCollection()).AddRange(new string[] { "repetir", "repetirme", "repite", "repíteme", "repítemela", "repítemelo" });
			(favoriteCollection = new StringCollection()).AddRange(new string[] { "favorito", "favoritos", "gusta", "gustas", "like", "likes" });
			(descriptionCollection = new StringCollection()).AddRange(new string[] { "descripción", "detalles" });
			(timeCollection = new StringCollection()).AddRange(new string[] { "aproxima", "aproximadamente", "aproximar", "aproximas", "cuánto", "exacta",
				"exactamente", "exacto", "tarda", "tardar", "tardaría", "tardas", "tardo", "tiempo" });
			(ingredientsCollection = new StringCollection()).AddRange(new string[] { "que", "cuál", "cuáles", "ingrediente", "ingredientes" });
			(stepCollection = new StringCollection()).AddRange(new string[] { "número", "paso", "pasos" });
			(exitCollection = new StringCollection()).AddRange(new string[] { "acabar", "salir", "terminar" });
			(addCollection = new StringCollection()).AddRange(new string[] { "añade", "añademelo", "añadir", "añado", "dar", "dale", "doy" });
			(removeCollection = new StringCollection()).AddRange(new string[] { "quita", "quitamelo", "quitar", "quitale" });
			(tipCollection = new StringCollection()).AddRange(new string[] { "aconseja", "aconsejame", "aconsejar", "consejo", "consejos", "diaria", "diario", "día",
				"hoy", "tip", "truco", "trucos" });

			machineLabel.FontSize = subtitleSize;
			machineLabel.Text = "Hola, ¿cómo puedo ayudarte?";
			humanLabel.Text = "Puedes probar a decir:\n«¿Cómo hago un Pastel de Calabaza?»";
			Shell.SetTabBarIsVisible(this, false);
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			try
			{
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync<MicrophonePermission>();
				if (status != PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Microphone))
						await DisplayAlert("Solicitud Micrófono", "El asistente necesita acceso al micrófono para funcionar correctamente.", "Aceptar");

					status = await CrossPermissions.Current.RequestPermissionAsync<MicrophonePermission>();
				}

				if ((status != PermissionStatus.Granted) && (status != PermissionStatus.Unknown))
				{
					await DisplayAlert("Acceso Denegado", "Para acceder al asistente, por favor, habilite el acceso al micrófono.", "Aceptar");
					await Navigation.PopAsync();
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e);
			}
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			recordButton.IsEnabled = false;
			micLabel.Text = "";
			background.Opacity = 1;

			var result = await App.speechService.SpeechToText();
			humanLabel.Text = result.Text;
			await Interpret(result.Text);

			micLabel.Text = "";
			background.Opacity = 0;
			recordButton.IsEnabled = true;
			recordButton.BackgroundColor = Color.Red;
		}

		private async Task Interpret(string text)
		{
			var words = text.ToLower().Split(new char[] { ' ', '.', ',', ':', ';', '¿', '?', '¡', '!' }, StringSplitOptions.RemoveEmptyEntries);
			List<String> remainder = new List<string>();
			string response;

			bool searchBool = false, nextBool = false, previousBool = false, repeatBool = false, favoriteBool = false,
				descriptionBool = false, timeBool = false, ingredientsBool = false, stepBool = false, exitBool = false, tipBool = false, addBool = false, removeBool = false;

			foreach (var word in words)
			{
				if (blackCollection.Contains(word))
					continue;

				if (searchCollection.Contains(word)) searchBool = true;
				else if (nextCollection.Contains(word)) nextBool = true;
				else if (previousCollection.Contains(word)) previousBool = true;
				else if (repeatCollection.Contains(word)) repeatBool = true;
				else if (favoriteCollection.Contains(word)) favoriteBool = true;
				else if (descriptionCollection.Contains(word)) descriptionBool = true;
				else if (timeCollection.Contains(word)) timeBool = true;
				else if (ingredientsCollection.Contains(word)) ingredientsBool = true;
				else if (stepCollection.Contains(word)) stepBool = true;
				else if (exitCollection.Contains(word)) exitBool = true;
				else if (tipCollection.Contains(word)) tipBool = true;
				else if (addCollection.Contains(word)) addBool = true;
				else if (removeCollection.Contains(word)) removeBool = true;
				else remainder.Add(word);
			}


			if (searchBool)
				response = SearchReceta(remainder);
			else if (tipBool)
				response = (await App.restService.GetConsejo()).texto;
			else if (favoriteBool)
				response = ListFavorites();
			else if (exitBool)
				response = await ExitAssistant();
			else if (remainder.Count > 0)
				response = SearchReceta(remainder);
			else
				response = recipeNotSelected;


			if (currentReceta != null)
			{
				if (timeBool)
					response = timeRead.Replace("%s", currentReceta.tiempo);
				else if (nextBool)
				{
					if (currentStep == steps.Length - 1)
					{
						response = stepEnd;
						currentStep = -1;
					}
					else
					{
						currentStep++;
						response = StepRead();
					}
				}
				else if (previousBool)
				{
					if (currentStep == 0)
						response = stepStart;
					else
					{
						currentStep--;
						response = StepRead();
					}
				}
				else if (repeatBool)
				{
					if (currentStep == -1)
						currentStep = 0;
					response = StepRead();
				}
				else if (favoriteBool)
				{
					if (addBool)
						response = await AddFavorite();
					else if (removeBool)
						response = await RemoveFavorite();
					else
						response = ListFavorites();
				}
				else if (descriptionBool)
					response = CurrentReceta.descripcion;
				else if (ingredientsBool)
					response = CurrentReceta.ingredientes;
				else if (stepBool)
				{
					int newStep = StringToInt(remainder[0]);

					if ((--newStep >= 0) && (newStep < steps.Length))
					{
						currentStep = newStep;
						response = StepRead();
					}
					else
						response = stepNotValid.Replace("%1", (++newStep).ToString()).Replace("%2", steps.Length.ToString());
				}
				else if (remainder.Count > 0)
					response = SearchReceta(remainder);
				else
					response = notRecognized;
			}

			micLabel.Text = "";
			machineLabel.Text = response;
			await App.speechService.TextToSpeech(response);
		}

		private string SearchReceta(List<String> text)
		{
			if (CurrentReceta == null)
			{
				if (text.Count > 0)
				{
					var filtered = new List<Receta>();
					var query = from receta in App.Recetas
								let w = receta.nombre.ToLower().Split(' ')
								where w.Intersect(text).Count() == text.Count()
								select receta;

					foreach (var receta in query)
						filtered.Add(receta);

					switch (filtered.Count)
					{
						case 0:
							return recipeNotFound;
						case 1:
							CurrentReceta = filtered[0];
							return recipeFound.Replace("%s", filtered[0].ToString());
						default:
							return recipeMultipleResults.Replace("%s", String.Join("\n", filtered));
					}
				}
				else
					return notRecognized;
			}
			else
			{
				currentStep = 0;
				return StepRead();
			}
		}

		private async Task<string> ExitAssistant()
		{
			await Navigation.PopAsync();
			return String.Empty;
		}

		private async Task<string> AddFavorite()
		{
			if (string.Equals(CurrentReceta.corazon, emptyHeart))
			{
				await App.restService.CreateFavorito(new Favorito() { idUsuario = App.loggedUser, idReceta = CurrentReceta.id });
				CurrentReceta.corazones++;
				CurrentReceta.corazon = filledHeart;
				App.Recetas[CurrentReceta.id - 1] = CurrentReceta;
				return favoriteAdded.Replace("%1", CurrentReceta.nombre).Replace("%2", CurrentReceta.corazones.ToString());
			}
			else
				return favoriteCantAdd;
		}

		private async Task<string> RemoveFavorite()
		{
			if (string.Equals(CurrentReceta.corazon, filledHeart))
			{
				await App.restService.DeleteFavorito(new Favorito() { idUsuario = App.loggedUser, idReceta = CurrentReceta.id });
				CurrentReceta.corazones--;
				CurrentReceta.corazon = emptyHeart;
				App.Recetas[CurrentReceta.id - 1] = CurrentReceta;
				return favoriteRemoved.Replace("%1", CurrentReceta.nombre).Replace("%2", CurrentReceta.corazones.ToString());
			}
			else
				return favoriteCantRemove;
		}
		private string ListFavorites()
		{
			var favoritos = HomePage.GetFavoritos();

			if (favoritos.Count == 0)
				return favoriteEmpty;
			else
				return String.Join("\n", favoritos);
		}

		private string StepRead()
		{
			return stepRead.Replace("%2", steps[currentStep]).Replace("%1", (currentStep + 1).ToString());
		}

		private int StringToInt(string input)
		{
			if (int.TryParse(input, out int result))
				return result;

			switch (input)
			{
				case "uno":
					return 1;
				case "dos":
					return 2;
				case "tres":
					return 3;
				case "cuatro":
					return 4;
				case "cinco":
					return 5;
				case "seis":
					return 6;
				case "siete":
					return 7;
				case "ocho":
					return 8;
				case "nueve":
					return 9;
				default:
					return 0;
			}
		}

	}
}
