using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int carImageId);
        IDataResult<List<CarImage>> GetCarImagesByCarId(int carId);
        IResult Add(IFormFile formfile, CarImage carImage);
        IResult Delete(CarImage carImage);
        IResult Update(IFormFile formfile, CarImage carImage);
    }
}
