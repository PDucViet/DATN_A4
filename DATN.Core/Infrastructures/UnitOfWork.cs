using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.Repositories.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DATN.Core.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DATNDbContext _context;

        //Them repo

        private IContactRepository _contactRepository;
        private IUserRepository _userRepository;


        private ICategoryRepository _categoryRepository;
        private ICategoryProductRepository _categoryProductRepository;
        private ICategoryTranslationRepository _categoryTranslationRepository;
        private ILanguageRepository _languageRepository;
        private IPromotionRepository _promotionRepository;

        private IAuthenRepository _authenRepository;
        private IRoleRepository _roleRepository;
        private IAddressRepository _addressRepository;
        //Product
        private IProductRepository _productRepository;
        private IAttributeRepository _attributeRepository;
        private IAtributeValueRepository _atributeValueRepository;
        private IProductAtributeRepository _productAtributeRepository;
        private IimageRepository _imageReponsiroty;
        private IProductAddressRepository _productAddressRepository;
        private IProductPromotionRepository _productPromotionRepository;
        private IInterestedRepository _interestedRepository;

        private INewFeedRepository _newFeedRepository;
        private IInvoiceDetailRepository _invoiceDetailRepository;

        private INotificationRepository _notificationRepository;
        private IOriginRepositoty _originRepositoty;
        private IMagazineRepository _magazineRepository;

        private IBrandRepository _brandRepository;
        private IVoucherUserRepository _voucherUserRepository;
        private IInvoiceRepository _invoiceRepository;
        private IPaymentInfoRepository _paymentInfoRepository;

        private ICommentRepository _commentRepository;
        private ITimeRangeRepository _timeRangeRepository;
        private IShippingOrderRepository _shippingOrderRepository;
        private ICategoryTimeRangeRepository _CateRangeRepository;
        private IImageTypeRepository _imageTypeRepository;
        private IVoucherRepository _voucherRepository;
        private IStatisticRepository _statisticRepository;

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UnitOfWork(DATNDbContext context, RoleManager<IdentityRole<Guid>> roleManager, IConfiguration configuration, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        //Thêm tương tự
        public IContactRepository DonateRepository => _contactRepository ?? (_contactRepository = new ContactRepository(_context, _mapper));
        public ICategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context, _mapper));
        public ICategoryProductRepository CategoryProductRepository => _categoryProductRepository ?? (_categoryProductRepository = new CategoryProductRepository(_context, _mapper));
        public ICategoryTranslationRepository CategoryTranslationRepository => _categoryTranslationRepository ?? (_categoryTranslationRepository = new CategoryTranslationRepository(_context, _mapper));
        public ILanguageRepository LanguageRepository => _languageRepository ?? (_languageRepository = new LanguageRepository(_context, _mapper));
        public IPromotionRepository PromotionRepository => _promotionRepository ?? (_promotionRepository = new PromotionRepository(_context, _mapper));
        public IRoleRepository RoleRepository => _roleRepository ?? (_roleRepository = new RoleRepository(_context));
        //Product
        public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(_context, _mapper));
        public IAddressRepository AddressRepository => _addressRepository ?? (_addressRepository = new AddressRepository(_context, _mapper));
        public IInvoiceDetailRepository InvoiceDetailRepository => _invoiceDetailRepository ?? (_invoiceDetailRepository = new InvoiceDetailRepository(_context, _mapper));

        public IAttributeRepository AttributeRepository => _attributeRepository ?? (_attributeRepository = new AttributeRepository(_context, _mapper));

        public IAtributeValueRepository AtributeValueRepository => _atributeValueRepository ?? (_atributeValueRepository = new AttributeValueRepository(_context, _mapper));

        public IProductAtributeRepository ProductAtributeRepository => _productAtributeRepository ?? (_productAtributeRepository = new ProductAttributeRepository(_context, _mapper));

        public IimageRepository imageReponsiroty => _imageReponsiroty ?? (_imageReponsiroty = new ImageReponsiroty(_context, _mapper));
        public IProductAddressRepository productAddressRepository => _productAddressRepository ?? (_productAddressRepository = new ProductAddressRepository(_context, _mapper));
        public IProductPromotionRepository productPromotionRepository => _productPromotionRepository ?? (_productPromotionRepository = new ProductPromotionRepository(_context, _mapper));
        public IInterestedRepository interestedRepository => _interestedRepository ?? (_interestedRepository = new InterestedRepository(_context, _mapper));

        public INotificationRepository notificationRepository => _notificationRepository ?? (_notificationRepository = new NotificationRepository(_context, _mapper));
        public IOriginRepositoty originRepositoty => _originRepositoty ?? (_originRepositoty = new OriginRepository(_context, _mapper));


        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context, _mapper));
        public DATNDbContext AppDbContext => _context;

        public IAuthenRepository AuthenRepository => _authenRepository ?? (_authenRepository = new AuthenRepository(_userManager, _roleManager, _context, _configuration));

        public INewFeedRepository newFeedRepository => _newFeedRepository ?? (_newFeedRepository = new NewFeedRepository(_context, _mapper));
        public IBrandRepository brandRepository => _brandRepository ?? (_brandRepository = new BrandRepository(_context, _mapper));
        public ICommentRepository commentRepository => _commentRepository ?? (_commentRepository = new CommentRepository(_context, _mapper));

        public IVoucherUserRepository voucherUserRepository => _voucherUserRepository ?? (_voucherUserRepository = new VoucherUserRepository(_context, _mapper));
        public IInvoiceRepository InvoiceRepository => _invoiceRepository ?? (_invoiceRepository = new InvoiceRepository(_context, _mapper));
        public IMagazineRepository MagazineRepository=> _magazineRepository??(_magazineRepository=new MagazineRepository(_context, _mapper));

        public IPaymentInfoRepository PaymentInfoRepository => _paymentInfoRepository ?? (_paymentInfoRepository = new PaymentInfoRepository(_context, _mapper));

        public ITimeRangeRepository TimeRangeRepository => _timeRangeRepository ?? (_timeRangeRepository = new TimeRangeRepository(_context, _mapper));

        public IImageTypeRepository ImageTypeRepository => _imageTypeRepository ?? (_imageTypeRepository = new ImageTypeRepository(_context, _mapper));

        public ICategoryTimeRangeRepository CategoryTimeRange => _CateRangeRepository ?? (_CateRangeRepository = new CategoryTimerangeRepository(_context, _mapper));

        public IShippingOrderRepository ShippingOrderRepository => _shippingOrderRepository ?? (_shippingOrderRepository = new ShippingOrderRepository(_context, _mapper));
        public IVoucherRepository VoucherRepository => _voucherRepository ??(_voucherRepository = new  VoucherRepository(_context, _mapper));

        public IStatisticRepository StatisticRepository => _statisticRepository ?? (_statisticRepository = new StatisticRepository(_context));

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

       
    }
}
