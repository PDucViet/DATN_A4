

using DATN.Core.Repositories.IRepositories;
using DATN.Core.Repositories.Repositories;

namespace DATN.Core.Infrastructures
{
    public interface IUnitOfWork : IDisposable
    {
        public IContactRepository DonateRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICategoryProductRepository CategoryProductRepository { get; }
        public ICategoryTranslationRepository CategoryTranslationRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public IUserRepository UserRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IAddressRepository AddressRepository { get; }
        public IAttributeRepository AttributeRepository { get; }
        public IAtributeValueRepository AtributeValueRepository { get; }
        public IProductAtributeRepository ProductAtributeRepository { get; }

        public IimageRepository imageReponsiroty { get; }
        public IProductAddressRepository productAddressRepository { get; }
        public IProductPromotionRepository productPromotionRepository { get; }
        public IInterestedRepository  interestedRepository { get; }
        public INewFeedRepository newFeedRepository { get; }


        public INotificationRepository notificationRepository { get; }
        public IOriginRepositoty originRepositoty { get; }
        public IPromotionRepository PromotionRepository { get; }
        public IInvoiceDetailRepository InvoiceDetailRepository { get; }
        public IBrandRepository brandRepository { get; }
        public ICommentRepository commentRepository { get; }
        public IVoucherUserRepository voucherUserRepository { get; }
        public IMagazineRepository MagazineRepository { get; }

        public IInvoiceRepository InvoiceRepository { get; }
        public IPaymentInfoRepository PaymentInfoRepository { get; }
        public IAuthenRepository AuthenRepository { get; }
        public ITimeRangeRepository TimeRangeRepository { get; }

        public IImageTypeRepository ImageTypeRepository { get; }
        public ICategoryTimeRangeRepository CategoryTimeRange { get; }
        public IShippingOrderRepository ShippingOrderRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IVoucherRepository VoucherRepository { get; }
        public IStatisticRepository StatisticRepository { get; }
        //public ICategoryRepository CategoryRepository { get; } // read only
        //public IProjectRepository ProjectRepository { get; }
        //public IPartnerRepository PartnerRepository { get; }
        //public IProjectImageRepository ProjectImageRepository { get; }
        //public MomoDbContext AppDbContext { get; }
        //public IAuthenRepository AuthenRepository { get; }
        //public IRoleRepository RoleRepository { get; }
        //public IUserRepository UserRepository { get; }
        //public IUserFollowRepository UserFollowRepository { get; }

        int SaveChanges();
    }
}
