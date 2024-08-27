using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.Paging;
using System.Drawing;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IimageRepository : IBaseRepository<DATN.Core.Model.Image>
	{
        ImagePaging GetImagePaging(ImagePaging request);
    }
}
