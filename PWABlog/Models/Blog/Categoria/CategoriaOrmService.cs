using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PWABlog.Models.Blog.Categoria
{
    public class CategoriaOrmService
    {
        private readonly Database databaseContext;

        public CategoriaOrmService(Database databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<CategoriaEntity> ObterCategorias()
        {
            // INÍCIO DOS EXEMPLOS
            
            /**********************************************************************************************************/
            /*** OBTER UM ÚNICO OBJETO                                                                                */
            /**********************************************************************************************************/
            
            // First = Obter a primeira categoria retornada pela consulta
            var primeiraCategoria = databaseContext.Categorias.First();
            
            // FirstOrDefault = Mesmo do First, porém retorna nulo caso não encontre nenhuma
            var primeiraCategoriaOuNulo = databaseContext.Categorias.FirstOrDefault();
            
            // Single = Obter um único registro do banco de dados
            var algumaCategoriaEspecifica = databaseContext.Categorias.Single(c => c.Id == 3);
            
            // SingleOrDefault = Mesmo do Sigle, porém retorna nulo caso não encontre nenhuma
            var algumaCategoriaOuNulo = databaseContext.Categorias.SingleOrDefault(c => c.Id == 3);
            
            // Find = Equivalente ao SingleOrDefault, porém fazendo uma busca por uma propriedade chave
            var encontrarCategoria = databaseContext.Categorias.Find(3);
            
            
            /**********************************************************************************************************/
            /*** OBTER MÚLTIPLOS OBJETOS                                                                              */
            /**********************************************************************************************************/
     
            // ToList
            var todasCategorias = databaseContext.Categorias.ToList();
            
            
            /***********/
            /* FILTROS */
            /***********/

            var categoriasComALetraG = databaseContext.Categorias.Where(c => c.Nome.StartsWith("G")).ToList();
            var categoriasComALetraMouL = databaseContext.Categorias
                .Where(c => c.Nome.StartsWith("M") || c.Nome.StartsWith("L"))
                .ToList();
            
            
         
            /*************/
            /* ORDENAÇÃO */
            /*************/

            var categoriasEmOrdemAlfabetica = databaseContext.Categorias.OrderBy(c => c.Nome).ToList();
            var categoriasEmOrdemAlfabeticaInversa = databaseContext.Categorias.OrderByDescending(c => c.Nome).ToList();
            
            
            /**************************/
            /* ENTIDADES RELACIONADAS */
            /**************************/

            var categoriasESuasEtiquetas = databaseContext.Categorias
                .Include(c => c.Etiquetas)
                .ToList();
                
            var categoriasSemEtiquetas = databaseContext.Categorias
                .Where(c=> c.Etiquetas.Count == 0)
                .ToList();
            
            var categoriasComEtiquetas = databaseContext.Categorias
                .Where(c=> c.Etiquetas.Count > 0)
                .ToList();
            
            // FIM DOS EXEMPLOS
            
            
            
            // Código de fato necessário para o método
            return databaseContext.Categorias
                .Include(c => c.Etiquetas)
                .ToList();
        }

        public CategoriaEntity getById(int idCategoria)
        {
            var categoria = databaseContext.Categorias.Find(idCategoria);

            return categoria;
        }

        public List<CategoriaEntity> getByName(string nomeCategoria)
        {
            return databaseContext.Categorias.Where(c => c.Nome.Contains(nomeCategoria)).ToList();
            
        }


        public CategoriaEntity Create(string nome)
        {
            var novaCategoria = new CategoriaEntity { Nome = nome };
            databaseContext.Categorias.Add(novaCategoria);
            databaseContext.SaveChanges();

            return novaCategoria;
        }

        public CategoriaEntity Edit(int id, string nome)
        {
            var categoria = databaseContext.Categorias.Find(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada!");
            }

            categoria.Nome = nome;
            databaseContext.SaveChanges();

            return categoria;
        }

        public bool Delete(int id)
        {
            var categoria = databaseContext.Categorias.Find(id);

            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada!");
            }

            databaseContext.Categorias.Remove(categoria);
            databaseContext.SaveChanges();

            return true;
        }

    }
}