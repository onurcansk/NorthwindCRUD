using Base.Aspects.Autofac.Validation;
using Base.DependencyResolvers.Ninject;
using Base.Utilities.Results.Abstract;
using Base.Utilities.Results.Concrete;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        ICategoryRepository _categoryRepository;
        public CategoryManager()
        {
            _categoryRepository=InstanceFactory.GetInstance<ICategoryRepository>();
        }
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category Entity)
        {
            _categoryRepository.Add(Entity);
            return new SuccessResult("Kategori ekleme işlemi başarıyla gerçekleşti.");
        }

        public IResult Delete(int id)
        {
            Category Entity = this.GetById(id).Result;
            _categoryRepository.Delete(Entity);
            return new SuccessResult("Kategori silme işlemi başarıyla gerçekleşti.");
        }

        public IDataResult<List<Category>> GetAll()
        {
            var result = _categoryRepository.GetAll();
            return new SuccessDataResult<List<Category>>(result, "Tüm kategoriler başarıyla listelendi");
        }

        public IDataResult<Category> GetById(int id)
        {
            var result = _categoryRepository.Get(x => x.CategoryID == id);
            return new SuccessDataResult<Category>(result, "Gönderilen idye ait kategori gösterildi.");
        }

        public IResult Update(Category Entity)
        {
            var result = this.GetById(Entity.CategoryID);
            result.Result.CategoryName = Entity.CategoryName;
            result.Result.Description = Entity.Description;
            result.Result.Picture = Entity.Picture;
            _categoryRepository.Update(result.Result);
            return new SuccessResult("Kategori güncelleme işlemi başarıyla gerçekleşti.");
        }
    }
}
