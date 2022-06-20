using FoodApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodApp.Services
{
	public class RestService
	{
		private const string RestUrl = "https://foodapp-api.azurewebsites.net/api/";

		HttpClient client;
		JsonSerializerOptions serializerOptions;

		public RestService()
		{
			client = new HttpClient();
			serializerOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = null,
				PropertyNameCaseInsensitive = true,
				WriteIndented = true
			};
		}


		public async Task<bool> CreateUsuario(Usuario usuario)
		{
			Uri uri = new Uri(string.Concat(RestUrl, "usuario/"));
			try
			{
				string json = JsonSerializer.Serialize(usuario, serializerOptions);
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await client.PostAsync(uri, content);
				return response.IsSuccessStatusCode;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				return false;
			}
		}

		public async Task<int> Login(Usuario usuario)
		{
			Uri uri = new Uri(string.Concat(RestUrl, "login/"));
			try
			{
				string json = JsonSerializer.Serialize(usuario, serializerOptions);
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await client.PostAsync(uri, content);
				if (response.IsSuccessStatusCode)
				{
					string responseJson = await response.Content.ReadAsStringAsync();
					List<Usuario> temp = JsonSerializer.Deserialize<List<Usuario>>(responseJson, serializerOptions);
					return temp[0].id;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
			return -1;
		}

		public async Task<bool> UpdateUsuario(Usuario usuario)
		{
			Uri uri = new Uri(string.Concat(RestUrl, "usuario/", usuario.id));
			try
			{
				string json = JsonSerializer.Serialize(usuario, serializerOptions);
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
				HttpResponseMessage response = await client.PutAsync(uri, content);
				return response.IsSuccessStatusCode;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
			return false;
		}

		public async Task<bool> DeleteUsuario(Usuario usuario)
		{
			Uri uri = new Uri(string.Concat(RestUrl, "usuario/", usuario.id));
			try
			{
				HttpResponseMessage response = await client.DeleteAsync(uri);
				return response.IsSuccessStatusCode;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				return false;
			}
		}

		public async Task<bool> GetRecetasAsUsuario(Usuario usuario)
		{
			Uri uri = new Uri(string.Concat(RestUrl, "recetas/", usuario.id));
			try
			{
				HttpResponseMessage response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					string content = await response.Content.ReadAsStringAsync();
					App.Recetas = JsonSerializer.Deserialize<List<Receta>>(content, serializerOptions);
					return true;
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
			return false;
		}

		public async Task<bool> CreateFavorito(Favorito favorito)
		{
			Uri uri = new Uri(string.Concat(RestUrl, "favorito/"));
			try
			{
				string json = JsonSerializer.Serialize(favorito, serializerOptions);
				StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await client.PostAsync(uri, content);
				return response.IsSuccessStatusCode;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				return false;
			}
		}

		public async Task<bool> DeleteFavorito(Favorito favorito)
		{
			Uri uri = new Uri(string.Concat(RestUrl, "favorito/", favorito.idUsuario, "/", favorito.idReceta));
			try
			{
				HttpResponseMessage response = await client.DeleteAsync(uri);
				return response.IsSuccessStatusCode;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				return false;
			}
		}

	}
}
