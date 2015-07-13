using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using DataAccess.Repositories;

namespace TaskListRefactoring.Services
{
    public class BasicService<T> where T : class
    {
        private AbstractRepository<T> _repository;

        public BasicService(AbstractRepository<T> repositry)
        {
            _repository = repositry;
        }

        public virtual ServiceResult GetAllData()
        {
            try
            {
                var entities = _repository.GetAll();

                return new ServiceResult { Errors = "", Success = entities };
            }
            catch (Exception exception)
            {
                return new ServiceResult{Errors = exception.Message, Success = null};
            }
        }

        public ServiceResult GetEntityById(int id)
        {
            try
            {
                var entity = _repository.Get(id);

                return new ServiceResult {Errors = "", Success = entity};
            }
            catch (Exception exception)
            {
                return new ServiceResult { Errors = exception.Message, Success = null };
            }
        }

        public ServiceResult AddEntity(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                _repository.Add(entity);

                return new ServiceResult {Errors = "", Success = entity};
            }
            catch (Exception exception)
            {
                return new ServiceResult {Errors = exception.Message, Success = null};
            }
        }

        public ServiceResult UpdateEntity(params T[] entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new Exception("Invalid type of parameter");
                }

                foreach (var entity in entities)
                {
                    _repository.Update(entity);
                }

                return new ServiceResult {Errors = "", Success = entities};
            }
            catch (Exception exception)
            {
                return new ServiceResult {Errors = exception.Message, Success = null};
            }
        }

        public ServiceResult DeleteEntity(params int[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    _repository.Delete(id);
                }

                return new ServiceResult {Errors = "", Success = new object()};
            }
            catch (Exception exception)
            {
                return new ServiceResult {Errors = exception.Message, Success = null};
            }
        }
    }
}