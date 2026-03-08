using AppMinhasCompras.Models;
using SQLite;


namespace AppMinhasCompras.Helpers
{
    // conecta o banco, cria tabela e faz operações
    public class SQLiteDatabaseHelper
    {
        // criação da variável _conn que faz conexão com o banco
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            // cria a tabela produto
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // Insere um produto no banco
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        // atualiza um produto
        public Task<List<Produto>> Update(Produto p)
        {
            // atualiza descrição, quantidade e preço onde o id é igual ao informado
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
        sql, p.Descricao, p.Quantidade, p.Preco, p.Id
        );
        }

        // deleta um prodduto de acordo com o Id que foi informado
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // busca todos os produtos e lista eles
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();

        }

        // busca produtos pela descrição deles
        public Task<List<Produto>> Search(string q)
        { 
            string sql = "SELECT * Produto WHERE Descricao LIKE ?";
            return _conn.QueryAsync<Produto>(sql, "%" + q + "%");
        }
        }
    }
