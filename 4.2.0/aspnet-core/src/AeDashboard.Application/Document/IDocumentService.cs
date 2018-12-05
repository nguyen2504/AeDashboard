using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;
using AeDashboard.Document.Dto;

namespace AeDashboard.Document
{
    public interface IDocumentService : IDomainService
    {
        IList<Document> GetAll();
        Task<Document> GetId(int id);
        Task<bool> CreateOrUpdate(DocumentDto entity);
        Task<bool> Delete(int id);
        IList<Document> Search(int skip, int take, string name);

    }
}
