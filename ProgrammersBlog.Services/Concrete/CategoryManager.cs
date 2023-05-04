using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string CreatedByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = CreatedByName;
            category.ModifiedByName = CreatedByName;
            var addedCategory=await _unityOfWork.Categories.AddAsync(category);
            await _unityOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adli kategori basariyla eklenmsitir",new CategoryDto
            {
                Category=addedCategory,
                ResultStatus=ResultStatus.Success,
                Message=$"{categoryAddDto.Name} adli kategori basariyla eklenmsitir",
            });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unityOfWork.Categories.UpdateAsync(category);
                await _unityOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{deletedCategory.Name} adli kategory basariyla silinmistir.", new CategoryDto
                {
                    Category=deletedCategory,
                    ResultStatus=ResultStatus.Success,
                    Message=$"{deletedCategory.Name} adli kategory basariyla silinmistir."
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, $"boyle bir kategory bulunamadi", new CategoryDto
            {
                Category=null,
                ResultStatus=ResultStatus.Error,
                Message=$"boyle bir kategory bulunamadi."
            });
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Boyle bir kategory bulunamadi. ", new CategoryDto
            {
                Category=null,
                ResultStatus=ResultStatus.Error,
                Message= "Boyle bir kategory bulunamadi. ",
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll() 
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hic bir kategori bulunamadi.", new CategoryListDto
            {
                Categories=null,
                ResultStatus=ResultStatus.Error,
                Message= "Hic bir kategori bulunamadi.",
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hic bir kategori bulunamadi.", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hic bir kategori bulunamadi.",
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hic bir kategori bulunamadi", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unityOfWork.Categories.DeleteAsync(category);
                await _unityOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} adli kategori veritabanindan basari ile silinmistir.");
            }
            return new Result(ResultStatus.Error, "boyle bir kategori bulunamamistir.");
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            var updatedCategory=await _unityOfWork.Categories.UpdateAsync(category);
            await _unityOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adli kategori basariyla guncellendi.",new CategoryDto
            {
                Category=updatedCategory,
                ResultStatus=ResultStatus.Success,
                Message=$"{categoryUpdateDto.Name} adli kategori basariyla guncellendi.",
            });
    }
}
}
