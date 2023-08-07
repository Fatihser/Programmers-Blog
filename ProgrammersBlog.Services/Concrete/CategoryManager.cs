using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDto">categoryAddDto tipinde eklenecek kategori bilgileri</param>
        /// /// <param name="CreatedByName">String tipinde kullanicinin kullanici adi</param>
        /// <returns>Asenkron bir operasyon ile Task olarak bizlere ekleme isleminin sonucunu DataResult tipinde doner.</returns>
        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string CreatedByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = CreatedByName;
            category.ModifiedByName = CreatedByName;
            var addedCategory=await _unityOfWork.Categories.AddAsync(category);
            await _unityOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Add(addedCategory.Name),new CategoryDto
            {
                Category=addedCategory,
                ResultStatus=ResultStatus.Success,
                Message=Messages.Category.Add(addedCategory.Name),
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await _unityOfWork.Categories.CountAsync();
            if (categoriesCount>-1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karsilasildi.",-1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categoriesCount = await _unityOfWork.Categories.CountAsync(c=>!c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karsilasildi.", -1);
            }
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unityOfWork.Categories.UpdateAsync(category);
                await _unityOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Delete(deletedCategory.Name), new CategoryDto
                {
                    Category=deletedCategory,
                    ResultStatus=ResultStatus.Success,
                    Message=Messages.Category.Delete(deletedCategory.Name)
                });;
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(false), new CategoryDto
            {
                Category=null,
                ResultStatus=ResultStatus.Error,
                Message=Messages.Category.NotFound(false)
            });
        }

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural:false), new CategoryDto
            {
                Category=null,
                ResultStatus=ResultStatus.Error,
                Message= Messages.Category.NotFound(isPlural:false),
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync() 
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(true), new CategoryListDto
            {
                Categories=null,
                ResultStatus=ResultStatus.Error,
                Message= "Hic bir kategori bulunamadi.",
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync()
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(c => !c.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(true), new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = Messages.Category.NotFound(true),
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive);
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

        /// <summary>
        /// Verilen ID parametresine ait kategorinin CategoryUpdateDto temsilini geri doner.
        /// </summary>
        /// <param name="categoryId">0'dan buyuk integer bir ID degeri</param>
        /// <returns>Assenkron bir operasyon ile Task olarak islem sonucunu DataResult tipinde geriye doner.</returns>
        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            var result = await _unityOfWork.Categories.AnyAsync(c=>c.Id==categoryId);
            if (result)
            {
                var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error,Messages.Category.NotFound(false), null); 
            }
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unityOfWork.Categories.DeleteAsync(category);
                await _unityOfWork.SaveAsync();
                return new Result(ResultStatus.Success, Messages.Category.HardDelete(category.Name));
            }
            return new Result(ResultStatus.Error, Messages.Category.NotFound(false));
        }

        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = _mapper.Map<CategoryUpdateDto,Category>(categoryUpdateDto,oldCategory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory=await _unityOfWork.Categories.UpdateAsync(category);
            await _unityOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Update(updatedCategory.Name),new CategoryDto
            {
                Category=updatedCategory,
                ResultStatus=ResultStatus.Success,
                Message=Messages.Category.Update(updatedCategory.Name),
            });
    }
}
}
