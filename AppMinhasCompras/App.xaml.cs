using AppMinhasCompras.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace AppMinhasCompras
{
    public partial class App : Application
    {
        // criação do campo db (onde está gravado e conexão única)
        static SQLiteDatabaseHelper _db;

        // criação da propriedade para ter acesso ao campo, somente leitura
        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // verificação se existe algum objeto no campo db
                if(_db == null)
                {
                     /* Path.combine garante que seja possível o encontro até o arquivo que
                    armazenará os dados em diferentes sistemas operacionais */

                    string path = Path.Combine(

                        // salvamento de dados na pasta
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),

                        // Nome do arquivo do banco de dados SQLite, onde será aberto ou criado
                        "banco_sqlite_compras.db3"
                        );

                    // criação de um objeto e guardar na variável _db
                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }
        }

        // Define a Main Page (página inicial)
        public App()
        {
            InitializeComponent();
       MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}