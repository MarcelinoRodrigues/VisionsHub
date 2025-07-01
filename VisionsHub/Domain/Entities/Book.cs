using VisionsHub.Domain.Common;

namespace VisionsHub.Domain.Entities
{
    public class Book : BaseEntity
    {
        /// <summary>
        /// Titulo do livro
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Autor do livro
        /// </summary>
        public required string Author { get; set; }
        /// <summary>
        /// identificador do livro
        /// </summary>
        public required string ISBN { get; set; }
        /// <summary>
        /// Ano da publicação
        /// </summary>
        public int YearOfPublication { get; set; }
        /// <summary>
        /// Categoria
        /// </summary>
        public required string Category { get; set; }
        /// <summary>
        /// Quantidade total
        /// </summary>
        public int TotalQuantity { get; set; }
        /// <summary>
        /// Quantidade disponivel
        /// </summary>
        public int AvailableQuantity { get; set; }
    }
}
