using Base.Aspects.Autofac.Validation;
using Base.DependencyResolvers.Ninject;
using Base.Utilities.Results.Abstract;
using Base.Utilities.Results.Concrete;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductRepository productRepository;
        public ProductManager()
        {
            productRepository = InstanceFactory.GetInstance<IProductRepository>();
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product Entity)
        {
            try
            {
                productRepository.Add(Entity);
                return new SuccessResult("Ürün ekleme işlemi başarılı");
            }
            catch (System.Exception)
            {

                return new ErrorResult("Ürün ekleme sırasında bir hata oluştu");
            }
        }

        public IResult Delete(int id)
        {
            try
            {
                var result = this.GetById(id).Result;
                productRepository.Delete(result);
                return new SuccessResult("Ürün silme işlemi başarıyla gerçekleşti");
            }
            catch (System.Exception)
            {
                return new ErrorResult("Ürün silme işlemi sırasında bir problem oluştu");
            }
        }

        public IDataResult<List<Product>> GetAll()
        {
            try
            {
                var result = productRepository.GetAll();
                return new SuccessDataResult<List<Product>>(result, "Ürünler listelendi");
            }
            catch (System.Exception)
            {

                return new ErrorDataResult<List<Product>>("Ürünler listelenirken bir hata oluştu");
            }
        }

        public IDataResult<Product> GetById(int id)
        {
            try
            {
                var result = productRepository.Get(c => c.ProductID == id);
                return new SuccessDataResult<Product>(result, "Ürün getirildi.");
            }
            catch (System.Exception)
            {

                return new ErrorDataResult<Product>("Idye ait ürün bulunamadı.");
            }
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product Entity)
        {
            try
            {
                var result = this.GetById(Entity.ProductID);
                result.Result.ProductName = Entity.ProductName;
                result.Result.UnitPrice = Entity.UnitPrice;
                result.Result.CategoryID = Entity.CategoryID;
                result.Result.UnitsInStock = Entity.UnitsInStock;
                productRepository.Update(result.Result);
                return new SuccessResult("Ürün güncelleme işlemi başarıyla gerçekleşti.");
            }
            catch (System.Exception)
            {
                return new ErrorResult("Güncelleme sırasında bir hata oluştu.");
            }
        }
    }
}
