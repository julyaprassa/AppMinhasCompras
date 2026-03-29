using AppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace AppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	// as alterações acontecem automaticamente na lista
	ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();
	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = Lista;

	}

	// Inicia dados toda vez que a página é aberta
	protected async override void OnAppearing()
	{
		try
		{
			// recarrega lista
			Lista.Clear();

			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => Lista.Add(i));
		}

		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}


	}

	// Navegar para outra página caso clique (botão adicionar)
	private void ToolbarItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
	}

	// Pesquisa conforme o texto é digitado e modificado

	private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
	{
		try
		{
			string q = e.NewTextValue;
			// atualizando
            lst_produtos.IsRefreshing = true;
       

        Lista.Clear();

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => Lista.Add(i));
		}

		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

	// Soma do total dos produtos
	private void ToolbarItem_Clicked_1(object sender, EventArgs e)
	{
		double soma = Lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos produtos", msg, "OK");
	}

	private async void MenuItem_Clicked(object sender, EventArgs e)
	{
		try
		{
			// ao clicar no menu item mostrará o item selecionado
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			// confirmar remoção do produto
			bool confirm = await DisplayAlert("Tem certeza?", $"Deseja remover {p.Descricao}?", "Sim", "Não");

			if (confirm)
			{
				// remoção do banco de dados
				await App.Db.Delete(p.Id);

				// remover da observable collection
				Lista.Remove(p);

			}
		}

		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");

		}
	}

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			// tela editar produto
			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p
			});
		}

		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }

	// regarregar lista
    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
		try
		{
			// recarrega lista
			Lista.Clear();

			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => Lista.Add(i));
		}

		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		// executa de qualquer maneira (para parar o "carregamento")
		finally {
			lst_produtos.IsRefreshing = false;
		}
    }
}