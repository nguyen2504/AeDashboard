﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using AeDashboard.Calendar;
using AeDashboard.Document.Dto;
using AeDashboard.Fn;
using AeDashboard.GetUser;

namespace AeDashboard.Document
{
    public class DocumentService : DomainService, IDocumentService
    {
        private readonly IGetUserService _getUserService;
        private readonly IRepository<Document> _repository;
        private readonly IFn _fn;

        public DocumentService(IGetUserService getUserService, IRepository<Document> repository, IFn fn)
        {
            _getUserService = getUserService;
            _repository = repository;
            _fn = fn;
        }
        public IList<Document> GetAll()
        {
            try
            {
                var orderByDescending = _repository.GetAllList().OrderByDescending(j => j.CreateDate).ToList();
                return orderByDescending;
            }
            catch (Exception e)
            {
                return new List<Document>();
            }
        }

        public  async  Task<Document> GetId(int id)
        {
            return await _repository.FirstOrDefaultAsync(j => j.Id.Equals(id));
        }

        public async Task<bool> CreateOrUpdate(DocumentDto entity)
        {
            try
            {
                var item = entity.MapTo<Document>();
                await _repository.InsertOrUpdateAsync(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }

        public IList<Document> Search(int skip, int take, string name)
        {
            try
            {
                IList<Document> l;
                if (!string.IsNullOrEmpty(name))
                {
                    l = _repository.GetAll().Where(j => j.Author.StartsWith(name)
                                                        || j.Content.StartsWith(name)).OrderByDescending(j => j.CreateDate).Skip(skip).Take(take).ToList();
                }
                else
                {
                    l = _repository.GetAll().OrderByDescending(j => j.CreateDate).Skip(skip).Take(take).ToList();
                }
                foreach (var q in l)
                {
                    q.Number = _fn.ConvertDaysOrHour(q.CreateDate);
                }
                return l;
            }
            catch (Exception e)
            {
                return new List<Document>();
            }

        }

        public List<string> LoadCatalogue()
        {
            List<string> data = _repository.GetAll().Select(j => j.Notifications).Distinct().ToList();
            try
            {

            }
            catch (Exception e)
            {
               data = new List<string>();
            }
            return data.ToList();
        }

        public async Task<bool> Update(Document entity)
        {
            try
            {
                await _repository.UpdateAsync(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
