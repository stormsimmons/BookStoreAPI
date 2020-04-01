using System;

namespace BookStore.ServiceModel.Dtos
{
    public class BookDto
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        public string ContentText { get; set; }
        public string FileName { get; set; }
        public long Views { get; set; }

    }
}
