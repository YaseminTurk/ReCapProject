using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile formFile, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimited(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.Add(formFile);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var oldpath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId).ImagePath;
            IResult result = BusinessRules.Run(FileHelper.Delete(oldpath));

            if (result != null)
            {
                return result;
            }

            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.CarImageId == carImageId));
        }

        public IDataResult<List<CarImage>> GetCarImagesByCarId(int carId)
        {
            IResult result = BusinessRules.Run(NullCarImage(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(ci=>ci.CarId==carId));
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile formFile, CarImage carImage)
        {
            var oldPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\wwwroot")) + _carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId).ImagePath;

            carImage.ImagePath = FileHelper.Update(oldPath, formFile);
            carImage.Date = DateTime.Now;

            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        private IResult CheckImageLimited(int carId)
        {
            var carImageCount = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (carImageCount >= 5)
            {
                return new ErrorResult(Messages.CarCheckImageLimited);
            }
            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> NullCarImage(int id)
        {
            try
            {
                string path = @"\wwwroot\images\default.jpg";
                var result = _carImageDal.GetAll(i => i.CarId == id).Any();
                if (!result)
                {
                    List<CarImage> images = new List<CarImage>();
                    images.Add(new CarImage() { CarId = id, Date = DateTime.Now, ImagePath = path });
                    return new SuccessDataResult<List<CarImage>>(images);
                }

            }
            catch (Exception e)
            {
                return new ErrorDataResult<List<CarImage>>(e.Message);
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(i => i.CarId == id));
        }
    }
}
