using System.Web.Mvc;
using Demo.Repositories;
using DemoFE.ViewModels;

namespace DemoFE.Controllers
{
    /// <summary>
    /// 品牌頻道
    /// </summary>
    public class BrandController : Controller
    {
        IBrandTypeRepository _brandTypeRepository;
        IBrandSubTypeRepository _brandSubTypeRepository;
        IBrandRepository _brandRepository;
        IProductRepository _productRepository;

        public BrandController()
        {
            _brandTypeRepository = new BrandTypeRepository();
            _brandSubTypeRepository = new BrandSubTypeRepository();
            _brandRepository = new BrandRepository();
            _productRepository = new ProductRepository();
        }

        /// <summary>
        /// 品牌列表頁 (首頁)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            BrandViewModel viewModel = new BrandViewModel();

            // 產品總數
            viewModel.productCounter = _productRepository.RetrieveCount(true);

            // 產品類型
            viewModel.brandTypeList = _brandTypeRepository.GetList(false);
            
            // 品牌列表
            viewModel.brandList = _brandRepository.GetList(0, 0, false);

            return View(viewModel);
        }

        /// <summary>
        /// 篩選產品類型
        /// </summary>
        /// <param name="id">產品類型 ID</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BrandType(long id)
        {
            BrandTypeViewModel viewModel = new BrandTypeViewModel();

            // 次分類列表
            viewModel.brandSubTypeList = _brandSubTypeRepository.GetList(id, false);

            // 品牌列表
            viewModel.brandList = _brandRepository.GetList(id, 0, false);

            return PartialView(viewModel);
        }

        /// <summary>
        /// 搜尋品牌 + 產品
        /// </summary>
        /// <param name="keyword">關鍵字(模糊比對)</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(string keyword)
        {
            SearchResultViewModel viewModel = new SearchResultViewModel();

            viewModel.brandList = _brandRepository.GetList(0, 0, keyword, false);
            viewModel.productList = _productRepository.GetList(0, keyword, false);

            return PartialView(viewModel);
        }

        /// <summary>
        /// 品牌首頁
        /// </summary>
        /// <param name="id">品牌 ID</param>
        /// <returns></returns>
        public ActionResult Intro(long id)
        {
            BrandIntroViewModel viewModel = new BrandIntroViewModel();

            // 抓單一筆品牌資料
            viewModel.brand = _brandRepository.Retrieve(id, false);

            // 列出該品牌所有產品
            viewModel.productList = _productRepository.GetList(id, "", false);

            return View(viewModel);
        }
        
        /// <summary>
        /// 產品細節
        /// </summary>
        /// <param name="ProductID">產品 ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Product(long ProductID)
        {
            // 抓單一產品資料
            var data = _productRepository.Retrieve(ProductID, false);

            return PartialView(data);
        }
    }
}
