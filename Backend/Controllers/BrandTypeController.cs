using System;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using Demo.Core.ActionFilter;
using Demo.Models;
using Demo.Repositories;

namespace Demo.Controllers
{
    /// <summary>
    /// 產品類型 (第一層)
    /// </summary>
    public class BrandTypeController : Controller
    {
        IBrandTypeRepository _brandTypeRepository;

        public BrandTypeController()
        {
            _brandTypeRepository = new BrandTypeRepository();
        }

        /// <summary>
        /// 後台 類別 列表
        /// </summary>
        /// <param name="page">目前所在頁數</param>
        /// <param name="PageSize">每頁筆數</param>
        /// <param name="sCaption">名稱</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult BackendList(int page = 1, int PageSize = 10, string sCaption = "")
        {
            var query = _brandTypeRepository.GetPagedList(page, PageSize, sCaption, true);
            int totalOrders = Convert.ToInt32(query.TotalItems);
            var data = query.Items;
            PagedList<DataModel_tbBrandType> pagedNews = new PagedList<DataModel_tbBrandType>(data, page, PageSize, totalOrders);

            ViewBag.TotalCounts = totalOrders;
            ViewBag.page = page;
            ViewBag.PageSize = PageSize;
            ViewBag.sCaption = sCaption;

            ViewBag.AlertMessage = TempData["AlertMessage"];

            return View(pagedNews);
        }

        /// <summary>
        /// 分類 切換顯示＼隱藏
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [PreserveQueryStringAttribute]
        public ActionResult ToggleVisible(long id)
        {
            if (_brandTypeRepository.ToggleVisible(id))
            {
                return RedirectToAction("BackendList");
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        /// <summary>
        /// 分類 上移
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [PreserveQueryStringAttribute]
        public ActionResult BackendMoveUp(long id)
        {
            _brandTypeRepository.MoveTypeUpDown(id, "u");

            return RedirectToAction("BackendList");
        }

        /// <summary>
        /// 分類 下移
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [PreserveQueryStringAttribute]
        public ActionResult BackendMoveDown(long id)
        {
            _brandTypeRepository.MoveTypeUpDown(id, "d");

            return RedirectToAction("BackendList");
        }

        /// <summary>
        /// 後台 分類 新增（檢視）
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [PreserveQueryStringAttribute]
        [HttpGet]
        public ActionResult BackendAdd()
        {
            return View();
        }

        /// <summary>
        /// 後台 分類 新增（存入）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [ValidateInput(false)]
        [PreserveQueryStringAttribute]
        [HttpPost]
        public ActionResult BackendAdd([Bind(Exclude = "sID")]DataModel_tbBrandType model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string ErrorMessage;

            if (_brandTypeRepository.InsertData(model, out ErrorMessage))
            {
                return RedirectToAction("BackendList");
            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// 後台 分類 編輯（檢視）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [PreserveQueryStringAttribute]
        public ActionResult BackendEdit(long id)
        {
            DataModel_tbBrandType data = _brandTypeRepository.Retrieve(id, true);

            if (data == null)
            {
                return Content("找不到此筆資料");
            }

            return View(data);
        }

        /// <summary>
        /// 後台 分類 編輯（存入）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        [PreserveQueryStringAttribute]
        public ActionResult BackendEdit(DataModel_tbBrandType model, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string ErrorMessage = "";
            if (_brandTypeRepository.UpdateData(model, out ErrorMessage))
            {

                return RedirectToAction("BackendList", new
                {
                    sCaption = (form["queryCaption"] != null) ? form["queryCaption"] : String.Empty,
                    page = (form["queryPage"] != null) ? form["queryPage"] : String.Empty,
                    PageSize = (form["queryPageSize"] != null) ? form["queryPageSize"] : String.Empty,
                });

            }
            else
            {
                return View(model);
            }
        }

        /// <summary>
        /// 後台 分類 刪除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [PreserveQueryStringAttribute]
        public ActionResult BackendDelete(long id)
        {
            // 先檢查該分類下是否仍有子分類、子分類下是否仍有品牌、品牌下是否仍有產品
            long Count = _brandTypeRepository.RetrieveSubTypeCountByType(id);
            long brandCount = _brandTypeRepository.RetrieveBrandCountByType(id);
            long productCount = _brandTypeRepository.RetrieveProductCountByType(id);

            long totalCount = Count + brandCount + productCount;
            if (totalCount > 0)
            {
                TempData["AlertMessage"] = "此分類下仍有資料，無法刪除。";

                return RedirectToAction("BackendList");
            }
            else
            {
                // 執行刪除
                _brandTypeRepository.Delete(id);

                TempData["AlertMessage"] = "刪除成功。";

                return RedirectToAction("BackendList");
            }
        }
    }
}
